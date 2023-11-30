using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Trend_Fashion_Strore.Models
{
    public class ProductColorsAndSize
    {
        public int ProductColorsAndSizeId { get; set; }


        [Required]
        [DisplayName("رقم المنتج")]
        public int ProductId { get; set; }
        [ForeignKey(nameof(ProductId))]
        [DisplayName("رقم المنتج")]
        public Product? Product { get; set; }


        [Required]
        [DisplayName("اللون")]
        public int ColorId { get; set; }
        [ForeignKey(nameof(ColorId))]
        [DisplayName("اللون")]
        public Color? Color { get; set; }


        [Required]
        [DisplayName("المقاس")]
        public int SizeId { get; set; }
        [ForeignKey(nameof(SizeId))]
        [DisplayName("المقاس")]
        public Size? Size { get; set; }


        public int Quantity { get; set; }

    }
}
