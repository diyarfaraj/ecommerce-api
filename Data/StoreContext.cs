using ecommerceApi.Entities;
using Microsoft.EntityFrameworkCore;

namespace ecommerceApi.Data
{
    public class StoreContext : IdentityDbContext
    {
        public StoreContext( DbContextOptions options) : base(options)
        {
        }

        public DbSet<Product> Products { get; set; }
        public DbSet<Basket> Baskets { get; set; }

    }
}
