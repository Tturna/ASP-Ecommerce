using ASP_Ecommerce.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace ASP_Ecommerce.Services;

public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
    : IdentityDbContext<UserModel, IdentityRole<int>, int>(options)
{
    // Users DbSet is provided by IdentityDbContext
    public DbSet<ProductModel> Products { get; set; } = default!;
    public DbSet<OrderModel> Orders { get; set; } = default!;
    public DbSet<OrderItemModel> OrderItems { get; set; } = default!;
    public DbSet<ShoppingCartModel> ShoppingCarts { get; set; } = default!;
    public DbSet<CartItemModel> CartItems { get; set; } = default!;
}