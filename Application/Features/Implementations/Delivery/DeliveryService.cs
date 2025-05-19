using Application.Constants.Commons;
using Application.Dtos.Delivery;
using Application.Features.Definitions.Books;
using Application.Features.Definitions.Contexts;
using Application.Features.Definitions.Delivery;
using Application.Features.Definitions.Identity;
using Application.Features.Definitions.Userprofile;
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
        private readonly IBookService _bookService;
        private readonly IUserProfileService _Userprofile;
        private readonly IMapper _mapper;
        private readonly IUserService _userService;
        private readonly UserManager<ApplicationUser> _userManager;
        public DeliveryService(IMapper mapper,
            IApplicationDbContext context,

            IBookService bookService,
            IUserProfileService userprofile,
            IUserService userService
,
            UserManager<ApplicationUser> userManager)
        {
            _mapper = mapper;
            _Context = context;
            _bookService = bookService;
            _Userprofile = userprofile;
            _userService = userService;
            _userManager = userManager;
        }


        public async Task<string> Add(DeliveryDto deliveryDto)
        {
            try
            {
                if (deliveryDto.BookId != null && deliveryDto.UserId != null)
                {
                    var activeDelivery = await _Context.Set<DeliveryStatus>()
                        .FirstOrDefaultAsync(ds => ds.UserId == deliveryDto.UserId && ds.DeliveryState);

                    if (activeDelivery != null)
                    {
                        return Messages.Error("شما هنوز کتاب قبلی را پس نداده‌اید و نمی‌توانید کتاب جدید دریافت کنید.");
                    }
                    var book = await _bookService.FindAsync(deliveryDto.BookId);
                    deliveryDto.BookName = book.Name;
                    var user = await _Userprofile.GetByIdAsync(deliveryDto.UserId);
                    deliveryDto.UserName = user.FirstName;


                    var deliverys = new Deliverys();
                    deliverys.BookId = deliveryDto.BookId;
                    deliverys.UserId = deliveryDto.UserId;
                    deliverys.DeliveryTime = DateTime.Now;


                    var deliveryStatus = new DeliveryStatus
                    {
                        UserId = deliveryDto.UserId,
                        DeliveryState = true,
                        BookBack = DateTime.Now.AddDays(14),
                        DeliveryId = deliverys.Id
                    };

                    deliverys.DeliveryStatuses.Add(deliveryStatus);
                    await _Context.Set<Deliverys>().AddAsync(deliverys);
                    await _Context.SaveChangesAsync();

                    return Messages.Success("تحویل کتاب با موفقیت انجام شد.");
                }

                return Messages.Error("اطلاعات ورودی نامعتبر است.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"❌ خطای دیتابیس: {ex.Message}");
                Console.WriteLine($"📌 جزئیات خطا: {ex.InnerException?.Message}");
                return Messages.Error("خطای غیرمنتظره‌ای رخ داده است، لطفاً دوباره امتحان کنید.");
            }
        }


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
                //ReservationId = reservation.Id,
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


        public async Task<List<DeliveryDto>> GetAllDelivery()
        {
            try
            {
                // دریافت لیست تحویل‌هایی که وضعیت تحویل دارند
                var deliveries = await _Context.Set<Deliverys>()
                    .Where(d => d.DeliveryStatuses.Any(ds => ds.DeliveryState))
                    .ToListAsync();

                // استخراج شناسه‌های کاربران
                var userIds = deliveries
                    .Select(d => d.UserId)
                    .Distinct()
                    .ToList();

              
                var bookIds = deliveries
                    .Select(d => d.BookId)
                    .Distinct()
                    .ToList();

              
                var profileUsers = await _Context.Set<ProfileUser>()
                    .Where(p => userIds.Contains(p.Id)) 
                    .ToDictionaryAsync(p => p.Id, p => p.NationalCode);

               
                var books = await _Context.Set<Book>()
                    .Where(b => bookIds.Contains(b.Id))
                    .ToDictionaryAsync(b => b.Id, b => b.Name);

               
                var result = deliveries.Select(d => new DeliveryDto
                {
                    Id = d.Id,
                    UserId = d.UserId,
                    UserName = profileUsers.GetValueOrDefault(d.UserId, "کد ملی یافت نشد"), 
                    BookId = d.BookId,
                    BookName = books.GetValueOrDefault(d.BookId, "نام کتاب یافت نشد"),
                    DeliveryTime = d.DeliveryTime,
                    ReservationId = d.ReservationId ?? 0,
                    DeliveryState = d.DeliveryStatuses.FirstOrDefault()?.DeliveryState ?? false
                })
                .Where(d => d.ReservationId == 0) 
                .ToList();

                return result;
            }
            catch (Exception ex)
            {
                Console.WriteLine("❌ خطا در دریافت اطلاعات تحویل‌ها:");
                Console.WriteLine(ex.Message);
                Console.WriteLine(ex.StackTrace);
                return new List<DeliveryDto>();
            }
        }

        public async Task<List<DeliveryDto>> GetAllResrvDelivery()
        {
            try
            {

                var deliveries = await _Context.Set<Deliverys>()
                    .Include(d => d.Reservation)
                        .ThenInclude(r => r.Book)
                    .Include(d => d.DeliveryStatuses)
                    .Where(d => d.DeliveryStatuses.Any(ds => ds.DeliveryState)
                                && d.Reservation != null)
                    .ToListAsync();

                var userIds = deliveries
                    .Select(d => d.UserId.ToString())
                    .Distinct()
                    .ToList();

                var identityUsers = await _userManager.Users
                    .Where(u => userIds.Contains(u.Id))
                    .ToDictionaryAsync(u => u.Id, u => u.UserName);

                var result = deliveries.Select(d => new DeliveryDto
                {
                    Id = d.Id,
                    UserId = d.UserId,
                    UserName = identityUsers.GetValueOrDefault(d.UserId.ToString()),
                    BookId = d.BookId,
                    BookName = d.Reservation?.Book?.Name,
                    DeliveryTime = d.DeliveryTime,
                    ReservationId = d.Reservation?.Id ?? 0,
                    DeliveryState = d.DeliveryStatuses.FirstOrDefault()?.DeliveryState ?? false
                }).Where(d => d.ReservationId != 0).ToList();

                return result;
            }
            catch (Exception ex)
            {
                Console.WriteLine("❌ خطا در دریافت اطلاعات تحویل‌ها:");
                Console.WriteLine(ex.Message);
                Console.WriteLine(ex.StackTrace);
                return new List<DeliveryDto>();
            }
        }

    }
}