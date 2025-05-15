
using Application.Features.Definitions.Books;
using Application.Features.Definitions.Contexts;
using Application.Features.Definitions.Identity;
using Application.Features.Definitions.Userprofile;
using Application.Features.Implementations.Books;
using Application.Features.Implementations.Identity;
using Application.Features.Implementations.UserProfile;
using Application.MappingProfile;
using Application.Repositories;
using Domain.Entities.Books;
using Domain.Entities.Users;
using Hangfire;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Persistence.Contexts;
using Persistence.Repositories;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddScoped<IReservationBookService, ReservationBookService>();
builder.Services.AddScoped<IGenericRepository, GenericRepository>();
builder.Services.AddScoped<IApplicationDbContext, ApplicationDbContext>();
builder.Services.AddScoped<IBookService, BookService>();
builder.Services.AddScoped<IBookCategories, BookCategoriesService>();
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IUserProfileService, UserProfileService>();
builder.Services.AddScoped<IidentityContext, IdentityContext>();



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
// اضافه کردن Hangfire
builder.Services.AddHangfire(config => config
    .SetDataCompatibilityLevel(CompatibilityLevel.Version_180)
    .UseSimpleAssemblyNameTypeSerializer()
    .UseRecommendedSerializerSettings()
    .UseSqlServerStorage(builder.Configuration.GetConnectionString("HangDb")));

builder.Services.AddHangfireServer();


builder.Services.AddDbContext<IdentityContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("Library")));

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("Library")));

builder.Services.AddControllersWithViews();

var app = builder.Build();

app.Lifetime.ApplicationStarted.Register(StartRecurringJobOptions);

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseHangfireDashboard("/hangfire");

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();

void StartRecurringJobOptions()
{
    RecurringJob.AddOrUpdate<ReservationBookService>(
     "AutoReleaseReservations",
     x => x.AutoReleaseExpiredReservationsAsync(),
     Cron.Daily,
     new RecurringJobOptions { TimeZone = TimeZoneInfo.Utc });

}