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
            // بررسی ورودی‌ها
            if (string.IsNullOrWhiteSpace(reservationDto.UserId) || reservationDto.BookId <= 0)
            {
                throw new MyArgumentNullException(ErrorType.InvalidInput);
            }

            // دریافت اطلاعات کاربر
            var user = await _userService.GetUserByUserId(reservationDto.UserId);
            if (user == null)
                return "کاربر موردنظر یافت نشد.";
            reservationDto.UserName = user.UserName;
            // دریافت اطلاعات کتاب
            var book = await _context.Set<Book>().FindAsync(reservationDto.BookId);
            
            if (book == null)
            {
                throw new MyArgumentNullException(ErrorType.BookIdNotFound);
            }

          
            if (book.IsAvailable==true) 
            {
                throw new MyArgumentNullException(ErrorType.IsAvailable);
            }

            var reservation=new Reservation();
            reservation.ReservationDate = reservationDto.ReservationDate;
            reservation.UserName = reservationDto.UserName;
            reservation.UserId=user.Id;
            reservation.ExpirationDate = DateTime.Now.AddDays(3);
            reservation.BookId= reservationDto.BookId;
         
           // book.Reservations.Add(reservation);

          
            book.IsAvailable = true;

           
            using (var transaction = await _context.Database.BeginTransactionAsync())
            {
                try
                {
                    _context.Set<Book>().Update(book);
                    _context.Set<Reservation>().Add(reservation);

                    // ذخیره تغییرات در پایگاه داده
                    await _context.SaveChangesAsync();

                    // تأیید تراکنش
                    await transaction.CommitAsync();
                }
                catch (Exception ex)
                {
                    // در صورت بروز خطا، تراکنش را بازگشت دهید
                    await transaction.RollbackAsync();
                    throw new Exception("خطا در ذخیره رزرو", ex);
                }
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

        public async Task<List<ReservationDto>> GetAllReservations()
        {
            var reservations = await _context.Set<Reservation>()
                .Join(_context.Set<Book>(),
                    reservation => reservation.BookId,
                    book => book.Id,
                    (reservation, book) => new ReservationDto
                    {
                        Id = reservation.Id,
                        BookId = book.Id,
                        BookName = book.Name,
                       UserName=reservation.UserName,
                    })
                .ToListAsync();

            return reservations;
        }
    }

}



