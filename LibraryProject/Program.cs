using LibraryProject.Business;
using LibraryProject.Data;
using LibraryProject.Data.Infrastructure;
using LibraryProject.Data.Service;
using log4net.Config;
using log4net;
using Microsoft.EntityFrameworkCore;
using System.Reflection;
using LibraryProject.Models;
using LibraryProject.Utils;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<ApplicationContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddTransient<IBook, BookRepo>();
builder.Services.AddTransient<BookService, BookService>();
builder.Services.AddTransient<IBorrowing, BorrowingRepo>();
builder.Services.AddTransient<BorrowingService, BorrowingService>();
builder.Services.AddTransient<Validator, Validator>();
log4net.Config.XmlConfigurator.Configure();

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
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
