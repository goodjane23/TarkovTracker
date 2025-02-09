using Microsoft.EntityFrameworkCore;
using TarkovTracker.Data;
using TarkovTracker.Services;

var builder = WebApplication.CreateBuilder(args);

var services = builder.Services;
var configuration = builder.Configuration;

services.AddControllersWithViews();

services.AddDbContextFactory<AppDbContext>(options =>
{
    options.UseNpgsql(configuration.GetConnectionString("TrackerDB"));
});

services.AddSingleton<TarkovApiService>();
services.AddSingleton<TrackerService>();

services.AddHttpClient();

services.AddHostedService<StartupService>();

var app = builder.Build();

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

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Tarkov}/{action=Index}/{id?}");

app.Run();
