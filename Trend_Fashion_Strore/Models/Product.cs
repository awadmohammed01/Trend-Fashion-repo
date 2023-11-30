using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace Trend_Fashion_Strore.Models
{
    public class Product
    {
        [Key]
        public int ProductId { get; set; }

        [Required]
        [DisplayName("اسم المنتج")]
        public string? Name { get; set; }

        [Required]
        [DisplayName("الصنف")]
        public string? Categorize { get; set; }

      

        [Required]
        [DisplayName("التكلفة الموحدة")]

        public double Cost { get; set; }

        [Required]
        [DisplayName("هامش الربح")]
        public double ProfitMargin { get; set; }

       

        [DisplayName("ملاحظات")]
        public string? Note { get; set; }

        public bool? Deleted { get; set; }

        [DisplayName("تاريخ الانشاء")]
        public DateTime? CreatedAt { get; set; }
        [DisplayName("تاريخ التعديل")]

        public DateTime? UpdatedAt { get; set; }
    }
}
