﻿using KochiStockholm.Entities;
using Microsoft.EntityFrameworkCore;

namespace KochiStockholm.Data
{
    public class StoreContext : DbContext
    {
        public StoreContext( DbContextOptions options) : base(options)
        {
        }

        public DbSet<Product> Products { get; set; }
    }
}
