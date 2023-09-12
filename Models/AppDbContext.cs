using Microsoft.EntityFrameworkCore;
using GraphEase.Models;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<Product> Products { get; set; }

    // Eğer diğer modelleriniz varsa (veritabanında bir karşılığı olanlar), 
    // onları da benzer şekilde ekleyebilirsiniz. Örneğin:
    // public DbSet<AnotherModel> AnotherModels { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Model oluşturma sırasında ek özelleştirmeler yapmak isterseniz bu metodu kullanabilirsiniz.
        // Örneğin, karmaşık ilişkiler, indeksler, varsayılan değerler vb. için.

        // Örnek: Product için bir indeks oluşturma
        // modelBuilder.Entity<Product>().HasIndex(p => p.UrunAdi).IsUnique();
    }
}
