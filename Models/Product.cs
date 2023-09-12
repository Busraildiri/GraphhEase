using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GraphEase.Models
{
    public class Product
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        public string? UrunAdi { get; set; }

        [Required]
        public string? Kategori { get; set; }

        [Required]
        public string? Cinsiyet { get; set; }

        [Required]
        public decimal BirimFiyat { get; set; }

        [Required]
        public decimal SatisFiyati { get; set; }

        [Required]
        public string? Sehir { get; set; }

        [Required]
        public int SatisAdeti { get; set; }
    }
}
