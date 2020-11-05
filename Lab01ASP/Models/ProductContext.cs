using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using Lab01ASP.Models;

namespace Lab01ASP.Models
{
    public class ProductContext: DbContext
    {
        public ProductContext(): base("WingtipToys")
        {

        }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<CartItem> ShoppingCartItems { get; set; }
    }
}