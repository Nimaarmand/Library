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
using Persistence.Contexts;
using Persistence.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddScoped<IReservationBookService, ReservationBookService>();
builder.Services.AddScoped<IBookService, BookService>();
builder.Services.AddScoped<IBookCategories, BookCategoriesService>();
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IDeliveryService, DeliveryService>();
builder.Services.AddScoped<IUserProfileService, UserProfileService>();
builder.Services.AddScoped<IApplicationDbContext, ApplicationDbContext>();

//Automapper
builder.Services.AddAutoMapper(typeof(BookProfile));
builder.Services.AddAutoMapper(typeof(ReservationProfile));
builder.Services.AddAutoMapper(typeof(Userprofile));
builder.Services.AddControllers();

builder.Services.AddIdentity<ApplicationUser, ApplicationRole>(options =>
{
    options.Password.RequireDigit = true;                    // ????? ?? ??? ?????? ???
    options.Password.RequireLowercase = true;                // ????? ?? ??? ???? ?????? ???
    options.Password.RequireUppercase = false;               // ????? ?? ??? ???? ????
    options.Password.RequireNonAlphanumeric = false;         // ????? ?? ??????? ??? ????
    options.Password.RequiredLength = 6;                     // ????? ??? ??? ????

    options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
    options.Lockout.MaxFailedAccessAttempts = 5;
    options.Lockout.AllowedForNewUsers = true;

    options.User.RequireUniqueEmail = true;
})
.AddEntityFrameworkStores<IdentityContext>()
.AddDefaultTokenProviders();
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
