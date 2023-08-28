using Microsoft.EntityFrameworkCore;
using WebApplication2.Data.Context;
using WebApplication2.Entity;
using WebApplication2.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<IMDbContext>(options => 
    options.UseSqlServer(builder.Configuration.GetConnectionString("YourConnectionString")));

builder.Services.AddIdentity<IdentityUser, IdentityRole>(options => options.SignIn.RequireConfirmedAccount = true).AddEntityFrameworkStores<IMDbContext>().AddDefaultTokenProviders();
builder.Services.AddScoped<ITypeOfBookRepository, TypeOfBookRepository>(); //ITypeOfBookRepository nesnesinin olu�turulmas�n� sa�lar 
//Dikkat --> sizde mutlaka  yeni bir repository s�n�f olu�turdu�unuzda mutlaka burda servicese eklemelisiniz
//Dependency Injection 

builder.Services.AddScoped<ITypeOfBookRepository, TypeOfBookRepository>();
builder.Services.AddScoped<IBookRepository, BookRepository>();
builder.Services.AddScoped<IHireBookRepository, HireRepository>();
builder.Services.AddRazorPages();
builder.Services.AddScoped<IEmailSender, EmailSender>();
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
app.MapRazorPages();
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
