﻿using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace Trend_Fashion_Strore.Models
{
    public class Basekt
    {
        [Key]
        [DisplayName("رقم السلة")]
        public int BasektId { get; set; }

        [Required]
        [DisplayName("رقم الحساب")]
        public int AccountId { get; set; }
        [ForeignKey(nameof(AccountId))]
        [DisplayName("رقم الحساب")]
        public Account? Account { get; set; }

        [DisplayName("الخصم")]
        public double? Discount { get; set; } = 0;

        [DisplayName("اجمالي المبلغ")]
        public double? Total { get; set; } = 0;
        [DisplayName("ملاحظة")]
        public string? Note { get; set; }
        public bool? Deleted { get; set; }

        [DisplayName("تاريخ الانشاء")]

        public DateTime? CreatedAt { get; set; }
        [DisplayName("تاريخ التعديل")]

        public DateTime? UpdatedAt { get; set; }

        [DisplayName("رقم الحواله الماليه")]
        public  double? MoneyTransformNum { get; set; }

        [DisplayName("حاله الحفظ")]
        public bool? SaveStatus { get; set; }

        [DisplayName("التحقق من الدفع")]
        public bool? PaymentVerification { get; set; }

    }
}
