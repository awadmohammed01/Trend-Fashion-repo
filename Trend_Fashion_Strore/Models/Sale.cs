using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace Trend_Fashion_Strore.Models
{
    public class Sale
    {
        [Key]
        public int SaleId { get; set; }


        [DisplayName("اسم المنتج")]
        public int? ProductId { get; set; } // Foreign Key part 1


        [DisplayName("اسم المنتج")]
        [ForeignKey(nameof(ProductId))]
        public Product? Product { get; set; } // Navigation property to Product

        [Required]
        [DisplayName("اللون")]
        public int? ColorId { get; set; } // Foreign Key part 1
        [DisplayName("اللون")]
        [ForeignKey(nameof(ColorId))]
        public Color? Color { get; set; } // Navigation property to Product

        [Required]
        [DisplayName("المقاس")]
        public int? SizeId { get; set; } // Foreign Key part 1
        [DisplayName("المقاس")]
        [ForeignKey(nameof(SizeId))]
        public Size? Size { get; set; } // Navigation property to Product


        [Required]
        [DisplayName("الكميه")]
        public double Quantity { get; set; }


        [Required]
        [DisplayName("السعر الافرادي")]
        public double Price { get; set; }


        [DisplayName("رقم السلة")]
        public int BasektId { get; set; }


        [ForeignKey(nameof(BasektId))]
        public Basekt? Basekt { get; set; }


        [DisplayName("ملاحظة")]
        public string? Note { get; set; }


        public bool? Deleted { get; set; }



        [DisplayName("تاريخ الانشاء")]
        public DateTime? CreatedAt { get; set; }


        [DisplayName("تاريخ التعديل")]
        public DateTime? UpdatedAt { get; set; }
    }
}
