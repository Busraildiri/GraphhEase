using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using GraphEase.Models;

// Web uygulamas� i�in bir yap�land�rma nesnesi olu�tur
var builder = WebApplication.CreateBuilder(args);

// MVC ile birlikte gelen Controller ve View yap�land�rmalar�n� ekleyin
builder.Services.AddControllersWithViews();

// Veritaban� ba�lant�s�n� ekleyin
// �zellikle, AppDbContext'i SQL Server ile kullanmak i�in yap�land�r�yoruz
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Web uygulamas� nesnesini olu�tur
var app = builder.Build();

// Uygulaman�n �al��ma modunu kontrol edin (Geli�tirme modu mu? �retim modu mu?)
if (app.Environment.IsDevelopment())
{
    // Geli�tirme modundayken, detayl� hata sayfalar�n� g�ster
    app.UseDeveloperExceptionPage();
}
else
{
    // �retim modundayken, hata sayfas�na y�nlendir
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts(); // Sadece HTTPS �zerinden eri�ime izin ver
}

// HTTPS y�nlendirmesi ve statik dosyalar i�in middleware'leri ekleyin
app.UseHttpsRedirection();
app.UseStaticFiles();

// Rotalamay� ve yetkilendirmeyi ekleyin
app.UseRouting();
app.UseAuthorization();

// Varsay�lan rota yap�land�rmas�n� ayarlay�n
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

// Uygulamay� ba�lat
app.Run();
