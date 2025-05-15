using Application.Constants.Commons;
using Application.Dtos.Delivery;
using Application.Features.Definitions.Delivery;
using Application.Repositories;
using AutoMapper;
using Domain.Entities.Books;
using Domain.Entities.Reservations;

namespace Application.Features.Implementations.Delivery
{
    public class DeliveryService : IDeliveryService
    {
        private readonly IGenericRepository _repository;
        private readonly IMapper _mapper;
        public DeliveryService(IGenericRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }
        /// <summary>
        /// تحویل کتاب به کاربر
        /// </summary>
        /// <param name="deliveryDto"></param>
        /// <returns></returns>
        public async Task<string> Add(DeliveryDto deliveryDto)
        {
            if (deliveryDto.BookId != null && deliveryDto.UserId != null)
            {
                // بررسی وضعیت تحویل قبلی کاربر
                var activeDeliveries = await _repository.Find<DeliveryStatus>(ds => ds.UserId == deliveryDto.UserId && ds.DeliveryState == true);

                if (activeDeliveries != null)
                {
                    return Messages.Error("شما هنوز کتاب قبلی را پس نداده‌اید و نمی‌توانید کتاب جدید دریافت کنید.");
                }

                // ثبت تحویل جدید
                var delivery = _mapper.Map<Deliverys>(deliveryDto);
                delivery.DeliveryTime = DateTime.Now;

                await _repository.AddAsync(delivery);
                await _repository.SaveChangesAsync();

                // ثبت وضعیت تحویل
                var deliveryStatus = new DeliveryStatus
                {
                    UserId = deliveryDto.UserId,
                    DeliveryState = true, // کتاب تحویل داده شده است
                    BookBack = DateTime.Now.AddDays(14), // مهلت بازگشت 14 روزه
                    DeliveryId = delivery.Id
                };

                await _repository.AddAsync(deliveryStatus);
                await _repository.SaveChangesAsync();

                return Messages.Success("تحویل کتاب با موفقیت انجام شد و وضعیت تحویل ثبت شد.");
            }

            return Messages.Error("اطلاعات ورودی نامعتبر است.");
        }
        /// <summary>
        /// پس گرفتن کتاب از کاربر
        /// </summary>
        /// <param name="deliveryId"></param>
        /// <returns></returns>
        public async Task<string> ReturnBook(long deliveryId)
        {
            var deliveryStatus = await _repository.Find<DeliveryStatus>(ds => ds.DeliveryId == deliveryId && ds.DeliveryState == true);

            if (deliveryStatus == null)
            {
                return Messages.Error("کتاب برای این کاربر یافت نشد یا قبلاً پس داده شده است.");
            }

            // تنظیم وضعیت تحویل به "False" و ثبت تاریخ بازگشت
            deliveryStatus.DeliveryState = false;
            deliveryStatus.BookBack = DateTime.Now;

            await _repository.UpdateAsync(deliveryStatus);
            await _repository.SaveChangesAsync();

            return Messages.Success("کتاب با موفقیت پس داده شد.");
        }
        /// <summary>
        /// تحویل کتاب رزرو شده به کاربر
        /// </summary>
        /// <returns></returns>

        public async Task<string> ReservationDelivery(long reservId)
        {
            if (reservId > 0)
            {
                // یافتن رزرو مربوطه
                var reservation = await _repository.Find<Reservation>(r => r.Id == reservId);
                if (reservation == null)
                {
                    return Messages.Error("رزرو موردنظر یافت نشد.");
                }

                // یافتن کتاب مرتبط با رزرو
                var book = await _repository.Find<Book>(b => b.Id == reservation.BookId);
                if (book == null || !book.IsAvailable)
                {
                    return Messages.Error("کتاب موجود نیست یا قبلاً تحویل داده شده است.");
                }

                // ثبت تحویل کتاب برای کاربر
                var delivery = new Deliverys
                {
                    UserId = reservation.UserId,
                    BookId = reservation.BookId,
                    DeliveryTime = DateTime.Now,
                    ReservationId = reservation.Id
                };

                await _repository.AddAsync(delivery);
                await _repository.SaveChangesAsync();

                // ثبت وضعیت تحویل
                var deliveryStatus = new DeliveryStatus
                {
                    UserId = reservation.UserId,
                    DeliveryId = delivery.Id,
                    ReservationId = reservation.Id,
                    DeliveryState = true, // تحویل انجام شد
                    BookBack = DateTime.Now.AddDays(14) // مهلت بازگشت ۱۴ روزه
                };

                await _repository.AddAsync(deliveryStatus);
                await _repository.SaveChangesAsync();

                // تغییر وضعیت کتاب به "غیرقابل رزرو"
                book.IsAvailable = false;
                await _repository.UpdateAsync(book);
                await _repository.SaveChangesAsync();

                return Messages.Success("کتاب با موفقیت به کاربر تحویل داده شد.");
            }

            return Messages.Error("شناسه رزرو نامعتبر است.");
        }


    }
}
