using System.IO;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OfficeOpenXml;
using GraphEase.Models;

// Bu kod C# dilinde yazıldı
namespace GraphEase.Controllers
{
    public class GraphController : Controller
    {
        private readonly AppDbContext dbContext;

        public GraphController(AppDbContext context)
        {
            dbContext = context;
        }

        [HttpGet]
        public IActionResult UploadFile()
        {
            return View();
        }

        private decimal CleanAndParsePrice(string price)
        {
            string cleanedPrice = price.Replace(" TL", "").Trim();
            return decimal.Parse(cleanedPrice);
        }

        [HttpPost]
        public async Task<IActionResult> Upload(IFormFile excelFile)
        {
            // Excel işlemleri başlamadan önce lisans ayarlamasını yapın.
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

            using (var memoryStream = new MemoryStream())
            {
                await excelFile.CopyToAsync(memoryStream);

                using (var package = new ExcelPackage(memoryStream))
                {
                    var worksheet = package.Workbook.Worksheets[0];
                    var rowCount = worksheet.Dimension.Rows;

                    for (int row = 2; row <= rowCount; row++)
                    {
                        var product = new Product
                        {
                            UrunAdi = worksheet.Cells[row, 1].Text,
                            Kategori = worksheet.Cells[row, 2].Text,
                            Cinsiyet = worksheet.Cells[row, 3].Text,
                            BirimFiyat = CleanAndParsePrice(worksheet.Cells[row, 4].Text),
                            SatisFiyati = CleanAndParsePrice(worksheet.Cells[row, 5].Text),
                            Sehir = worksheet.Cells[row, 6].Text,
                            SatisAdeti = int.Parse(worksheet.Cells[row, 7].Text)
                        };

                        dbContext.Products.Add(product);
                    }

                    await dbContext.SaveChangesAsync();
                }
            }

            // Dosya yüklemesi başarılı bir şekilde tamamlandıktan sonra kullanıcıyı Analysis sayfasına yönlendir.
            return RedirectToAction("Analysis", "Home");
        }

        [HttpGet]
        public IActionResult GetGraphData(string graphType)
        {
            List<Product> products = dbContext.Products.ToList(); // Veritabanından ürünleri al

            // Daha önce yazılan kodları burada tekrar kullanabiliriz.
            if (graphType == "gender_sales")
            {
                var genderSalesData = products.GroupBy(p => p.Cinsiyet)
                                          .Select(g => new { label = g.Key, y = g.Sum(p => p.SatisFiyati) })
                                          .ToList();

                return Json(genderSalesData);
            }
            // Diğer grafik türleri için benzer koşullar ekleyin.

            // Özetle, yukarıdaki kodda sadece Excel dosyasını okuma kısmını veritabanından ürünleri çekmeyle değiştirdik.
            return BadRequest("Unknown graph type");
        }


        // Diğer metodlarınız burada devam edebilir.
    }
}
