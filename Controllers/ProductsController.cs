using ASP_Ecommerce.Models;
using ASP_Ecommerce.Models.ViewModels;
using ASP_Ecommerce.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ASP_Ecommerce.Controllers;

public class ProductsController(ApplicationDbContext dbContext) : Controller
{
    public async Task<IActionResult> Index()
    {
        var products = await dbContext
            .Products
            .Where(p => p.Maintainer == null)
            .AsNoTracking()
            .ToArrayAsync();
        
        var categories = await dbContext.Categories
            .Include(c => c.Parent)
            .ThenInclude(c => c.Parent)
            .AsNoTracking()
            .ToArrayAsync();

        var productsViewModel = new ProductsViewModel()
        {
            Products = products,
            Categories = categories
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
        
        // Remove circular reference so this object can be serialized and
        // passed to a Razor component.
        foreach (var product in products)
        {
            product.Maintainer!.MaintainerProducts = [];
        }
        
        var productsViewModel = new ProductsViewModel()
        {
            Products = products
        };
        
        return View(productsViewModel);
    }

    [Route("/products/{id:int}")]
    public IActionResult Product(int id)
    {
        var product = dbContext.Products
            .Include(p => p.Maintainer)
            .AsNoTracking()
            .FirstOrDefault(p => p.Id == id);

        if (product == null)
        {
            return NotFound();
        }
        
        // Remove circular reference so this object can be serialized and
        // passed to a Razor component.
        if (product.Maintainer != null)
        {
            product.Maintainer.MaintainerProducts = [];
        }
        
        return View(product);
    }
    
    // TODO: Validate CSRF token.
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