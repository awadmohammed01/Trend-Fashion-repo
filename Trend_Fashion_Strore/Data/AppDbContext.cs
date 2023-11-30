using Microsoft.EntityFrameworkCore;
using Trend_Fashion_Strore.Models;

namespace Trend_Fashion_Strore.Data
{
    public class AppDbContext:DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }

        public DbSet<Account> Accounts { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Sale> Sales { get; set; }
        public DbSet<Basekt> Basekts { get; set; }
        public DbSet<ProductImage> ProductImages { get; set; }
        public DbSet<Size> Sizes { get; set; }
        public DbSet<TempAccountInfo> TempAccountInfos { get; set; }
        public DbSet<Color> Colors { get; set; }
        public DbSet<ProductColorsAndSize> ProductColorsAndSizes { get; set; }


    }
}

