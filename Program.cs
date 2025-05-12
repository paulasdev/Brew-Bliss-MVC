using BrewBlissApp.Models;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<BrewBlissDbContext>(options => 

    options.UseSqlServer(builder.Configuration.GetConnectionString("ConsultWithUsDbContext"))); 


var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Index/Error");

    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.MapControllerRoute(
    name: "default1",
    pattern: "{controller=Contact}/{action=Contact}/{id?}");

app.MapControllerRoute(
    name: "default2",
    pattern: "{controller=About}/{action=About}/{id?}");    

app.MapControllerRoute(
    name: "default3",
    pattern: "{controller=Menu}/{action=Menu}/{id?}");

// API routes (for MenuController)
app.MapControllers(); 
app.UseStaticFiles();

app.Run();


