using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace Trend_Fashion_Strore.Models
{
    public class Account
    {
        [Key]

        public int AccountId { get; set; }

        [Required]
        [DisplayName("اسم العميل")]
        public string? Name { get; set; }

        [Required]
        [DisplayName("حساب الحيميل")]
        public string? Email { get; set; }

        [Required]
        [DisplayName("البلد")]
        public string? Address { get; set; }

        [DisplayName("ملاحظات")]
        public string? Note { get; set; }

        [Required]
        [MaxLength(12), MinLength(12)]
        [DisplayName("كلمه السر")]
        public string? Password { get; set; }

        public bool? Deleted { get; set; }
        [DisplayName("تاريخ الانشاء")]
        public DateTime? CreatedAt { get; set; }
        [DisplayName("تاريخ التعديل ")]
        public DateTime? UpdatedAt { get; set; }
    }
}
