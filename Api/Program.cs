using Application.Features.Definitions.Books;
using Application.Features.Definitions.Contexts;
using Application.Features.Definitions.Delivery;
using Application.Features.Definitions.Identity;
using Application.Features.Definitions.Userprofile;
using Application.Features.Implementations.Books;
using Application.Features.Implementations.Delivery;
using Application.Features.Implementations.Identity;
using Application.Features.Implementations.UserProfile;
using Application.MappingProfile;
using Application.Repositories;
using Domain.Entities.Users;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Persistence.Contexts;
using Persistence.Repositories;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
// Add services to the container.
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("Library")));
builder.Services.AddDbContext<IdentityContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("Library")));

builder.Services.AddIdentity<ApplicationUser, ApplicationRole>(options =>
{
    options.Password.RequireDigit = true;                    // حداقل یک عدد الزامی است
    options.Password.RequireLowercase = true;                // حداقل یک حرف کوچک الزامی است
    options.Password.RequireUppercase = false;               // نیازی به حرف بزرگ نیست
    options.Password.RequireNonAlphanumeric = false;         // نیازی به کاراکتر خاص نیست
    options.Password.RequiredLength = 6;                     // حداقل طول رمز عبور

    options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
    options.Lockout.MaxFailedAccessAttempts = 5;
    options.Lockout.AllowedForNewUsers = true;

    options.User.RequireUniqueEmail = true;
})
.AddEntityFrameworkStores<IdentityContext>()
.AddDefaultTokenProviders();

// اضافه کردن AutoMapper
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.AddAutoMapper(typeof(BookProfile));
builder.Services.AddAutoMapper(typeof(ReservationProfile));
builder.Services.AddAutoMapper(typeof(Userprofile));

//Sercice

builder.Services.AddScoped<IApplicationDbContext,ApplicationDbContext>();
builder.Services.AddScoped<IReservationBookService, ReservationBookService>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IBookService, BookService>();
builder.Services.AddScoped<IDeliveryService, DeliveryService>();
builder.Services.AddScoped<IUserProfileService, UserProfileService>();


// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
