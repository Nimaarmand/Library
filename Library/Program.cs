
using Application.Features.Definitions.Books;
using Application.Features.Implementations.Books;
using Application.Repositories;
using Hangfire;
using Microsoft.Build.Framework;
using Persistence.Repositories;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddScoped<IReservationBookService, ReservationBookService>();
builder.Services.AddScoped<IGenericRepository, GenericRepository>();
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