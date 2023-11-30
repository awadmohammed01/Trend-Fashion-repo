using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace Trend_Fashion_Strore.Models
{
    public class Size
    {
        [Key]
        public int SizeId { get; set; }

        [DisplayName("المقاس")]
        [Required]
        public string? SizeName { get; set; }

       


       



    }
}
