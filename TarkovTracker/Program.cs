using Microsoft.Build.Execution;
using Microsoft.EntityFrameworkCore;
using System.Xml.Linq;
using TarkovTracker.Controllers;
using TarkovTracker.Data;
using TarkovTracker.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<AppDbContext>((options) =>
{
    options.UseNpgsql(builder.Configuration.GetConnectionString("TrackerDB"));
} );

builder.Services.AddScoped<TarkovApiService>();
builder.Services.AddScoped<TrackerService>();

builder.Services.AddHostedService()
//builder.Services.AddDbContext<>;
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

app.MapControllerRoute
    (name: "/tarkovtracker",
    pattern: "{controller=Tarkov}/{action=Index}");

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
