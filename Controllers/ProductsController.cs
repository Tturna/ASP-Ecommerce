using ASP_Ecommerce.Models.ViewModels;
using ASP_Ecommerce.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ASP_Ecommerce.Controllers;

public class ProductsController(ApplicationDbContext dbContext) : Controller
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
    
    [Authorize]
    [HttpDelete]
    public IActionResult Delete(int? id)
    {
        if (id == null)
        {
            return BadRequest();
        }
        
        var product = dbContext.Products
            .Include(p => p.Maintainer)
            .AsNoTracking()
            .FirstOrDefault(p => p.Id == id);

        if (product == null)
        {
            return NotFound();
        }
        
        if (product.Maintainer?.UserName != User.Identity!.Name)
        {
            return Unauthorized();
        }

        dbContext.Products.Remove(product);
        dbContext.SaveChanges();

        return Ok();
    }
}