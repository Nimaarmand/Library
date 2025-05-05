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
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;


namespace Application.Features.Implementations.Books
{
    public class ReservationBookService : IReservationBookService
    {
        private readonly IGenericRepository _repository;
        private readonly IMapper _mapper;

        public ReservationBookService(IGenericRepository genericRepository, IMapper mapper)
        {
            _repository = genericRepository;
            _mapper = mapper;
        }

        public async Task<string> AddReservationAsync(ReservationDto reservationDto)
        {

            if (string.IsNullOrWhiteSpace(reservationDto.UserId) || reservationDto.BookId <= 0)
            {
                return "اطلاعات ورودی نامعتبر است.";
            }


            var user = await _repository.GetById<ApplicationUser>(reservationDto.UserId);
            if (user == null)
            {
                return "کاربر موردنظر یافت نشد.";
            }


            var book = await _repository.GetById<Book>(reservationDto.BookId);
            if (book == null)
            {
                return "کتاب موردنظر یافت نشد.";
            }


            if (!book.IsAvailable)
            {
                return "کتاب موردنظر قابل رزرو نیست.";
            }


            var reservation = _mapper.Map<Reservation>(reservationDto);



            reservation.ExpirationDate = DateTime.Now.AddDays(3);


            book.reservations ??= new List<Reservation>();
            book.reservations.Add(reservation);
            book.IsAvailable = false;

            await _repository.UpdateAsync<Book>(book);

            // افزودن رزرو به پایگاه داده
            await _repository.AddAsync<Reservation>(reservation);

            // ذخیره‌سازی تغییرات در پایگاه داده
            await _repository.SaveChangesAsync();

            return Messages.Success("رزرو با موفقیت انجام شد");     
        }


        public async Task<string> Remove(long id)
        {
            if (id > 0)
            {
                var reservation = await _repository.GetById<Reservation>(id);
                if (reservation == null)
                {
                    throw new MyArgumentNullException(ErrorType.ReservationIdNotFound);
                }

                await _repository.RemoveAsync(reservation);
                await _repository.SaveChangesAsync();

                return Messages.Success("رزرو کنسل شد");
            }

            throw new MyArgumentNullException(ErrorType.InvalidInput);
        }

        public async Task<IEnumerable<Reservation>> GetAllBooksAsync(CancellationToken cancellationToken = default)
        {
            return await _repository.GetAll<Reservation>(null, null, true, cancellationToken);


        }
        public async Task AutoReleaseExpiredReservationsAsync()
        {
            var expiredReservations = await _repository.GetAll<Reservation>(
                predicate: r => r.ExpirationDate.AddDays(3) < DateTime.Now,
                orderBy: null,
                cancellationToken: default);

            foreach (var reservation in expiredReservations)
            {
                var book = await _repository.Find<Book>(b => b.Id == reservation.BookId);
                if (book != null)
                {
                    book.IsAvailable = true;
                    await _repository.UpdateAsync(book);
                }

                await _repository.RemoveAsync(reservation);
            }

            await _repository.SaveChangesAsync();
        }
    }

}



