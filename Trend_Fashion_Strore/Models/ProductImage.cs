using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace Trend_Fashion_Strore.Models
{
    public class ProductImage
    {
        [Key]
        public int ProductImageId { get; set; }

        [DisplayName("اسم الصوره")]
        [Required]
        public string? ImageName { get; set; }

        [DisplayName("رقم المنتج ")]
        public int ProductId { get; set; }


        [ForeignKey(nameof(ProductId))]
        public Product? product { get; set; }

        [Required]
        [DisplayName("مسار الصوره ")]
        public string? ImagePath { get; set; }

        [Required]
        [DisplayName("اللون")]
        public string? ProductColor { get; set; }

    }
}
