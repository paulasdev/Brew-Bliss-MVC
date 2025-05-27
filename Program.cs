using BrewBlissApp.Models;
using Microsoft.EntityFrameworkCore;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

// Register DbContext with connection string
builder.Services.AddDbContext<BrewBlissDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("BrewBlissDbContext")));

// Enable session services
builder.Services.AddSession();

// Cookie authentication configuration
builder.Services.AddAuthentication("Cookies")
    .AddCookie("Cookies", options =>
    {
        options.LoginPath = "/Admin/Index"; 
    });

builder.Services.AddAuthorization();

var app = builder.Build();

// Exception and status code handling (for production)
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseStatusCodePagesWithReExecute("/Error/{0}");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

// Add middleware for sessions and authentication
app.UseSession();
app.UseAuthentication();
app.UseAuthorization();

// Set up default MVC route
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

// Optional: map API controllers
app.MapControllers();

app.Run();
