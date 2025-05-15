using Application.Constants.Commons;
using Application.Dtos.Books;
using Application.Exceptions.ValidationExceptions;
using Application.Features.Definitions.Books;
using Application.Features.Definitions.Contexts;
using Application.Features.Definitions.Identity;
using Application.Repositories;
using AutoMapper;
using Domain.Entities.Books;
using Domain.Entities.Reservations;
using Domain.Entities.Users;
using Microsoft.EntityFrameworkCore;


namespace Application.Features.Implementations.Books
{
    public class ReservationBookService : IReservationBookService
    {
        private readonly IApplicationDbContext _context;
        private readonly IUserService _userService;
       
        private readonly IMapper _mapper;

        public ReservationBookService(IApplicationDbContext context, IMapper mapper, IUserService userService)
        {
            _context = context;
            _mapper = mapper;
            _userService = userService;
        }


        public async Task<string> AddReservationAsync(ReservationDto reservationDto)
        {
            if (string.IsNullOrWhiteSpace(reservationDto.UserId) || reservationDto.BookId <= 0)
                throw new MyArgumentNullException(ErrorType.InvalidInput);

            
            var user = await _userService.GetUserByUserId(reservationDto.UserId);
            if (user == null)
                return "کاربر موردنظر یافت نشد.";

          
            var book = await _context.Set<Book>()
                .Include(b => b.Reservations) 
                .FirstOrDefaultAsync(b => b.Id == reservationDto.BookId);

            if (book == null)
                throw new MyArgumentNullException(ErrorType.BookIdNotFound);

           
            if (book.IsAvailable==false) 
            {
               
                book.IsAvailable = true; 
            }
            else
            {
                
                throw new MyArgumentNullException(ErrorType.IsAvailable); 
            }

            
            var reservation = _mapper.Map<Reservation>(reservationDto);
            reservation.ExpirationDate = DateTime.Now.AddDays(3);

            
            book.Reservations ??= new List<Reservation>();
            book.Reservations.Add(reservation);

           
            _context.Set<Reservation>().Add(reservation);
            _context.Set<Book>().Update(book);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {

            }

           

            return Messages.Success("رزرو با موفقیت انجام شد");
        }

       
        public async Task<string> Remove(long id)
        {
            if (id <= 0)
                throw new MyArgumentNullException(ErrorType.InvalidInput);

            
            var reservation = await _context.Set<Reservation>().FindAsync(id);
            if (reservation == null)
                throw new MyArgumentNullException(ErrorType.ReservationIdNotFound);

          
            var book = await _context.Set<Book>().FindAsync(reservation.BookId);
            if (book != null)
            {
                
                book.IsAvailable = false;
                _context.Set<Book>().Update(book);
            }

          
            _context.Set<Reservation>().Remove(reservation);
            await _context.SaveChangesAsync();

            return Messages.Success("رزرو کنسل شد");
        }

       
        public async Task<IEnumerable<Reservation>> GetAllBooksAsync(CancellationToken cancellationToken = default)
        {
            return await _context.Set<Reservation>()
                .Include(r => r.Book)                  
                .ToListAsync(cancellationToken);
        }

       
        public async Task AutoReleaseExpiredReservationsAsync()
        {
            var now = DateTime.Now;
            var expiredReservations = await _context.Set<Reservation>()
                .Where(r => r.ExpirationDate < now) 
                .ToListAsync();

            var bookIds = expiredReservations.Select(r => r.BookId).Distinct().ToList();
            var books = await _context.Set<Book>()
                .Where(b => bookIds.Contains(b.Id))
                .ToListAsync();

            foreach (var reservation in expiredReservations)
            {
                var book = books.FirstOrDefault(b => b.Id == reservation.BookId);
                if (book != null)
                {
                  
                    book.IsAvailable = false;  
                    _context.Set<Book>().Update(book);
                }

             
                _context.Set<Reservation>().Remove(reservation);
            }

            await _context.SaveChangesAsync();
        }
    }

}



