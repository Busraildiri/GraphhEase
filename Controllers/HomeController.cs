using GraphEase.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace GraphEase.Controllers
{
    public class HomeController : Controller
    {
        private readonly AppDbContext _context;

        public HomeController(AppDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Analysis()
        {
            var model = new GraphEase.Models.AnalysisResultsViewModel
            {
                GenderSalesData = _context.Products
                    .GroupBy(p => p.Cinsiyet)
                    .Select(g => new GraphEase.Models.SalesByGender
                    {
                        Gender = g.Key,
                        TotalSales = g.Sum(p => p.SatisAdeti)
                    }).ToList(),

                CityProductGenderSalesData = _context.Products
                .GroupBy(p => new { p.Sehir, p.UrunAdi })
                .Select(g => new GraphEase.Models.SalesByCityProductAndGender
                {
                    City = g.Key.Sehir,
                    ProductName = g.Key.UrunAdi,
                    MaleSales = g.Where(p => p.Cinsiyet == "Erkek").Sum(p => p.SatisAdeti),
                    FemaleSales = g.Where(p => p.Cinsiyet == "Kadın").Sum(p => p.SatisAdeti)
                }).ToList(),

                CityGenderSalesData = _context.Products
                    .GroupBy(p => p.Sehir)
                    .Select(g => new GraphEase.Models.SalesByCityAndGender
                    {
                        City = g.Key,
                        MaleSales = g.Where(p => p.Cinsiyet == "Erkek").Sum(p => p.SatisAdeti),
                        FemaleSales = g.Where(p => p.Cinsiyet == "Kadın").Sum(p => p.SatisAdeti),
                        TotalSales = g.Sum(p => p.SatisAdeti)
                    }).ToList(),

                ProductSalesData = _context.Products
                    .GroupBy(p => p.UrunAdi)
                    .Select(g => new GraphEase.Models.SalesByProductName
                    {
                        ProductName = g.Key,
                        TotalSales = g.Sum(p => p.SatisAdeti)
                    }).ToList(),

                CityProductSalesData = _context.Products
                    .GroupBy(p => new { p.Sehir, p.UrunAdi })
                    .Select(g => new GraphEase.Models.SalesByCityAndProduct
                    {
                        City = g.Key.Sehir,
                        ProductName = g.Key.UrunAdi,
                        TotalSales = g.Sum(p => p.SatisAdeti)
                    }).ToList(),

                CityGenderCategorySalesData = _context.Products
                    .GroupBy(p => new { p.Sehir, p.Cinsiyet, p.Kategori })
                    .Select(g => new GraphEase.Models.SalesByCityGenderAndCategory
                    {
                        City = g.Key.Sehir,
                        Gender = g.Key.Cinsiyet,
                        Category = g.Key.Kategori,
                        SalesCount = g.Sum(p => p.SatisAdeti)
                    }).ToList(),
            };

            return View("Analysis", model);
        }
    }
}
