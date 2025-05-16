using Application.Constants.Commons;
using Application.Dtos.Delivery;
using Application.Features.Definitions.Contexts;
using Application.Features.Definitions.Delivery;
using Application.Repositories;
using AutoMapper;
using Domain.Entities.Books;
using Domain.Entities.Reservations;
using Domain.Entities.Users;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.Implementations.Delivery
{
    public class DeliveryService : IDeliveryService
    {
        private readonly IApplicationDbContext _Context;
        private readonly IMapper _mapper;
        private readonly UserManager<ApplicationUser> _userManager;
        public DeliveryService(IMapper mapper, IApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _mapper = mapper;
            _Context = context;
            _userManager = userManager;
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
                var activeDelivery = await _Context.Set<DeliveryStatus>()
                    .FirstOrDefaultAsync(ds => ds.UserId == deliveryDto.UserId && ds.DeliveryState);

                if (activeDelivery != null)
                {
                    return Messages.Error("شما هنوز کتاب قبلی را پس نداده‌اید و نمی‌توانید کتاب جدید دریافت کنید.");
                }

                var delivery = _mapper.Map<Deliverys>(deliveryDto);
                delivery.DeliveryTime = DateTime.Now;

                await _Context.Set<Deliverys>().AddAsync(delivery);
                await _Context.SaveChangesAsync();

                var deliveryStatus = new DeliveryStatus
                {
                    UserId = deliveryDto.UserId,
                    DeliveryState = true,
                    BookBack = DateTime.Now.AddDays(14),
                    DeliveryId = delivery.Id
                };

                await _Context.Set<DeliveryStatus>().AddAsync(deliveryStatus);
                await _Context.SaveChangesAsync();

                return Messages.Success("تحویل کتاب با موفقیت انجام شد.");
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
            var deliveryStatus = await _Context.Set<DeliveryStatus>()
                .FirstOrDefaultAsync(ds => ds.DeliveryId == deliveryId && ds.DeliveryState);

            if (deliveryStatus == null)
            {
                return Messages.Error("کتاب برای این کاربر یافت نشد یا قبلاً پس داده شده است.");
            }

            deliveryStatus.DeliveryState = false;
            deliveryStatus.BookBack = DateTime.Now;

            _Context.Set<DeliveryStatus>().Update(deliveryStatus);
            await _Context.SaveChangesAsync();

            return Messages.Success("کتاب با موفقیت پس داده شد.");
        }

        /// <summary>
        /// تحویل کتاب رزرو شده به کاربر
        /// </summary>
        /// <param name="reservId"></param>
        /// <returns></returns>
        public async Task<string> ReservationDelivery(long reservId)
        {
            if (reservId <= 0)
            {
                return Messages.Error("شناسه رزرو نامعتبر است.");
            }

            var reservation = await _Context.Set<Reservation>().FirstOrDefaultAsync(r => r.Id == reservId);
            if (reservation == null)
            {
                return Messages.Error("رزرو موردنظر یافت نشد.");
            }

            var book = await _Context.Set<Book>().FirstOrDefaultAsync(b => b.Id == reservation.BookId);
            if (book == null || !book.IsAvailable)
            {
                return Messages.Error("کتاب موجود نیست یا قبلاً تحویل داده شده است.");
            }

            var delivery = new Deliverys
            {
                UserId = reservation.UserId,
                BookId = reservation.BookId,
                DeliveryTime = DateTime.Now,
                ReservationId = reservation.Id
            };

            await _Context.Set<Deliverys>().AddAsync(delivery);
            await _Context.SaveChangesAsync();

            var deliveryStatus = new DeliveryStatus
            {
                UserId = reservation.UserId,
                DeliveryId = delivery.Id,
                ReservationId = reservation.Id,
                DeliveryState = true,
                BookBack = DateTime.Now.AddDays(14)
            };

            await _Context.Set<DeliveryStatus>().AddAsync(deliveryStatus);
            await _Context.SaveChangesAsync();

            book.IsAvailable = false;
            _Context.Set<Book>().Update(book);
            await _Context.SaveChangesAsync();

            return Messages.Success("کتاب با موفقیت به کاربر تحویل داده شد.");
        }

        public async Task<List<DeliveryDto>> GetAllResrvDelivery()
        {
            var delivryreserv = await _Context.Set<Deliverys>()
                .Join(_Context.Set<Reservation>(),
                    delivery => delivery.ReservationId,
                    reservation => reservation.Id,
                    (delivery, reservation) => new { delivery, reservation })
                .Join(_Context.Set<DeliveryStatus>(),
                    dr => dr.delivery.Id,
                    status => status.DeliveryId,
                    (dr, status) => new { dr.delivery, dr.reservation, status })
                .ToListAsync();

            var result = new List<DeliveryDto>();

            foreach (var item in delivryreserv)
            {
                var user = await _userManager.FindByIdAsync(item.delivery.UserId.ToString()); // دریافت نام کاربر از Identity

                result.Add(new DeliveryDto
                {
                    Id = item.delivery.Id,
                    UserId = item.delivery.UserId,
                    UserName = user?.UserName, // نام کاربر
                    BookId = item.delivery.BookId,
                    DeliveryTime = item.delivery.DeliveryTime,
                    ReservationId = item.reservation.Id,
                    DeliveryState = item.status.DeliveryState // وضعیت تحویل
                });
            }

            return result;
        }
    }
}
