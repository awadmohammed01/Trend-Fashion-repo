using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace Trend_Fashion_Strore.Models
{
    public class TempAccountInfo
    {
        [Key]
        public int TempId { get; set; }

        
        [DisplayName("رقم الحساب")]
        public int? AccountId { get; set; }
       

        [DisplayName("رقم السلة")]
        public int? BasketId { get; set; }
    }
}
