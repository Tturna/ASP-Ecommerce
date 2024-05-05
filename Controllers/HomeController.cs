using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using ASP_Ecommerce.Models;
using ASP_Ecommerce.Models.ViewModels;
using ASP_Ecommerce.Services;
using Microsoft.EntityFrameworkCore;

namespace ASP_Ecommerce.Controllers;

public class HomeController(ILogger<HomeController> logger, ApplicationDbContext dbContext) : Controller
{
    private readonly ILogger<HomeController> _logger = logger;

    public IActionResult Index()
    {
        var maintainers = dbContext.Users
            .Where(u => u.MaintainerAddress != null)
            .AsNoTracking()
            .ToArray();

        if (maintainers.Length > 3)
        {
            maintainers = maintainers.Take(3).ToArray();
        }
        
        var featuredOfficialProducts = dbContext.Products
            .Where(p => p.MaintainerId == null)
            .Take(3)
            .AsNoTracking()
            .ToArray();
        
        var featuredMaintainerProducts = dbContext.Products
            .Where(p => p.MaintainerId != null)
            .Take(3)
            .AsNoTracking()
            .ToArray();

        var homeViewModel = new HomeViewModel()
        {
            FeaturedMaintainers = maintainers,
            FeaturedProducts = featuredOfficialProducts,
            FeaturedMaintainerProducts = featuredMaintainerProducts
        };
        
        return View(homeViewModel);
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}