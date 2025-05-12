
using Application.Features.Definitions.Books;
using Application.Features.Definitions.Contexts;
using Application.Features.Implementations.Books;
using Application.MappingProfile;
using Application.Repositories;
using Domain.Entities.Books;
using Hangfire;
using Microsoft.EntityFrameworkCore;
using Persistence.Contexts;
using Persistence.Repositories;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddScoped<IReservationBookService, ReservationBookService>();
builder.Services.AddScoped<IGenericRepository, GenericRepository>();
builder.Services.AddScoped<IApplicationDbContext, ApplicationDbContext>();
builder.Services.AddScoped<IBookService, BookService>();
builder.Services.AddScoped<IBookCategories, BookCategoriesService>();





// اضافه کردن AutoMapper
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.AddAutoMapper(typeof(BookProfile));
builder.Services.AddAutoMapper(typeof(ReservationProfile));
// اضافه کردن Hangfire
builder.Services.AddHangfire(config => config
    .SetDataCompatibilityLevel(CompatibilityLevel.Version_180)
    .UseSimpleAssemblyNameTypeSerializer()
    .UseRecommendedSerializerSettings()
    .UseSqlServerStorage(builder.Configuration.GetConnectionString("HangDb")));

builder.Services.AddHangfireServer();

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