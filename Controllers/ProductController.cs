using ASP_Ecommerce.Models.ViewModels;
using ASP_Ecommerce.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ASP_Ecommerce.Controllers;

[Route("/products")]
public class ProductController(ApplicationDbContext dbContext) : Controller
{
    public async Task<IActionResult> Index()
    {
        var products = await dbContext.Products.ToArrayAsync();
        var temp = HttpContext;

        var productsViewModel = new ProductsViewModel()
        {
            Products = products
        };
        
        return View(productsViewModel);
    }
}