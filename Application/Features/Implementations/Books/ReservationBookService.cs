using Application.Constants.Commons;
using Application.Dtos.Books;
using Application.Exceptions.ValidationExceptions;
using Application.Features.Definitions.Books;
using Application.Features.Definitions.Contexts;
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
        private readonly IidentityContext _Identitycontext;
        private readonly IMapper _mapper;

        public ReservationBookService(IApplicationDbContext context, IMapper mapper, IidentityContext identitycontext)
        {
            _context = context;
            _mapper = mapper;
            _Identitycontext = identitycontext;
        }

        
        public async Task<string> AddReservationAsync(ReservationDto reservationDto)
        {
            if (string.IsNullOrWhiteSpace(reservationDto.UserId) || reservationDto.BookId <= 0)
                throw new MyArgumentNullException(ErrorType.InvalidInput);

            
            var user = await _Identitycontext.Users.FindAsync(reservationDto.UserId);
            if (user == null)
                return "کاربر موردنظر یافت نشد.";

          
            var book = await _context.Books
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

           
            _context.Reservations.Add(reservation);
            _context.Books.Update(book);

            
            await _context.SaveChangesAsync();

            return Messages.Success("رزرو با موفقیت انجام شد");
        }

       
        public async Task<string> Remove(long id)
        {
            if (id <= 0)
                throw new MyArgumentNullException(ErrorType.InvalidInput);

            
            var reservation = await _context.Reservations.FindAsync(id);
            if (reservation == null)
                throw new MyArgumentNullException(ErrorType.ReservationIdNotFound);

          
            var book = await _context.Books.FindAsync(reservation.BookId);
            if (book != null)
            {
                
                book.IsAvailable = false;
                _context.Books.Update(book);
            }

          
            _context.Reservations.Remove(reservation);
            await _context.SaveChangesAsync();

            return Messages.Success("رزرو کنسل شد");
        }

       
        public async Task<IEnumerable<Reservation>> GetAllBooksAsync(CancellationToken cancellationToken = default)
        {
            return await _context.Reservations
                .Include(r => r.Book) 
                .Include(r => r.User)  
                .ToListAsync(cancellationToken);
        }

       
        public async Task AutoReleaseExpiredReservationsAsync()
        {
            var now = DateTime.Now;
            var expiredReservations = await _context.Reservations
                .Where(r => r.ExpirationDate < now) 
                .ToListAsync();

            var bookIds = expiredReservations.Select(r => r.BookId).Distinct().ToList();
            var books = await _context.Books
                .Where(b => bookIds.Contains(b.Id))
                .ToListAsync();

            foreach (var reservation in expiredReservations)
            {
                var book = books.FirstOrDefault(b => b.Id == reservation.BookId);
                if (book != null)
                {
                  
                    book.IsAvailable = false;  
                    _context.Books.Update(book);
                }

             
                _context.Reservations.Remove(reservation);
            }

            await _context.SaveChangesAsync();
        }
    }

}



