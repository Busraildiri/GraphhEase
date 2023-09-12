using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using GraphEase.Models;

// Web uygulamasý için bir yapýlandýrma nesnesi oluþtur
var builder = WebApplication.CreateBuilder(args);

// MVC ile birlikte gelen Controller ve View yapýlandýrmalarýný ekleyin
builder.Services.AddControllersWithViews();

// Veritabaný baðlantýsýný ekleyin
// Özellikle, AppDbContext'i SQL Server ile kullanmak için yapýlandýrýyoruz
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Web uygulamasý nesnesini oluþtur
var app = builder.Build();

// Uygulamanýn çalýþma modunu kontrol edin (Geliþtirme modu mu? Üretim modu mu?)
if (app.Environment.IsDevelopment())
{
    // Geliþtirme modundayken, detaylý hata sayfalarýný göster
    app.UseDeveloperExceptionPage();
}
else
{
    // Üretim modundayken, hata sayfasýna yönlendir
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts(); // Sadece HTTPS üzerinden eriþime izin ver
}

// HTTPS yönlendirmesi ve statik dosyalar için middleware'leri ekleyin
app.UseHttpsRedirection();
app.UseStaticFiles();

// Rotalamayý ve yetkilendirmeyi ekleyin
app.UseRouting();
app.UseAuthorization();

// Varsayýlan rota yapýlandýrmasýný ayarlayýn
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

// Uygulamayý baþlat
app.Run();
