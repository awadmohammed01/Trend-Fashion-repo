using Microsoft.Build.Framework;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel;

namespace Trend_Fashion_Strore.Models
{
    public class Color
    {
        public int ColorId { get; set; }

        [Required]
        public string? ColorName { get; set; }
    }
}
