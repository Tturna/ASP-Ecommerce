using ASP_Ecommerce.Models.ViewModels;
using ASP_Ecommerce.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ASP_Ecommerce.Controllers;

[Route("/products")]
public class ProductController(ApplicationDbContext dbContext) : Controller
{
    [Route("/products")]
    public async Task<IActionResult> OfficialProducts()
    {
        var products = await dbContext
            .Products
            .Where(p => p.Maintainer == null)
            .AsNoTracking()
            .ToArrayAsync();

        var productsViewModel = new ProductsViewModel()
        {
            Products = products
        };
        
        return View(productsViewModel);
    }
    
    [Route("/products/maintainer")]
    public async Task<IActionResult> MaintainerProducts()
    {
        var products = await dbContext
            .Products
            .Where(p => p.Maintainer != null)
            .Include(p => p.Maintainer)
            .AsNoTracking()
            .ToArrayAsync();

        var productsViewModel = new ProductsViewModel()
        {
            Products = products
        };
        
        return View(productsViewModel);
    }
}