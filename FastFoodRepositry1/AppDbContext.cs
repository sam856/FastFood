using FastFoodModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNet.Identity.EntityFramework;

namespace FastFoodRepositry1
{
    public class AppDbContext :IdentityDbContext<ApplicationUser>
    { 
        DbSet<ApplicationUser> ApplicationUser { get; set; }
        DbSet<Cart> Cart { get; set; }
        DbSet<Category> Category { get; set; }
        DbSet<Item> Item { get; set; }
        DbSet<Coupon> Coupon { get; set; }
        DbSet <OrderDetails> OrderDetails { get; set; }
        DbSet<OrderHeader> OrderHeader { get; set; }
        DbSet<SubCategory> SubCategories { get; set; }
  
        protected 
}
}
