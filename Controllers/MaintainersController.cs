using ASP_Ecommerce.Models;
using ASP_Ecommerce.Models.Enums;
using ASP_Ecommerce.Models.ViewModels;
using ASP_Ecommerce.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ASP_Ecommerce.Controllers;

public class MaintainersController(ApplicationDbContext dbContext,
    UserManager<UserModel> userManager,
    HttpClient httpClient,
    IConfiguration config) : Controller
{
    public IActionResult Index(int? id)
    {
        if (id == null)
        {
            var maintainers = dbContext.Users
                .Where(u => u.MaintainerAddress != null)
                .AsNoTracking()
                .ToArray();
            
            var maintainersViewModel = new MaintainersViewModel()
            {
                Maintainers = maintainers,
                GeocodingUrl = $"https://us1.locationiq.com/v1/search?key={config["LIQ_KEY"]}&q={{address}}&format=json"
            };
            
            return View(maintainersViewModel);
        }
        
        var user = dbContext.Users
            .Include(u => u.MaintainerProducts)
            .AsNoTracking()
            .FirstOrDefault(u => u.Id == id);

        if (user?.MaintainerAddress == null)
        {
            return NotFound();
        }
        
        // Remove circular reference so this object can be serialized and
        // passed to a Razor component.
        user.MaintainerProducts = user.MaintainerProducts.Select(p =>
        {
            p.Maintainer = null;
            return p;
        }).ToList();

        var productsViewModel = new ProductsViewModel()
        {
            Products = user.MaintainerProducts.ToArray()
        };
        
        var individualMaintainerViewModel= new IndividualMaintainerViewModel()
        {
            Maintainer = user,
            ProductsViewModel = productsViewModel
        };

        return View("Individual", individualMaintainerViewModel);
    }

    [Authorize]
    public IActionResult Register()
    {
        return View();
    }

    [HttpPost, Authorize, ValidateAntiForgeryToken]
    public async Task<IActionResult> Register([FromForm] BecomeMaintainerViewModel newMaintainerData)
    {
        if (!ModelState.IsValid)
        {
            return View(newMaintainerData);
        }
        
        // user can't be null because of the [Authorize] attribute
        var user = dbContext.Users.First(u => u.UserName == User.Identity!.Name);

        await userManager.AddToRoleAsync(user, Enum.GetName(UserRole.Maintainer)!);
        user.MaintainerAddress = newMaintainerData.MaintainerAddress;
        user.City = newMaintainerData.City;
        user.PostalCode = newMaintainerData.PostalCode;
        user.Country = newMaintainerData.Country;

        var fulladdress = $"{user.MaintainerAddress}, {user.City}, {user.PostalCode}, {user.Country}";
        var uriAddress = Uri.EscapeDataString(fulladdress);
        var apikey = config["LIQ_KEY"];
        var uri = $"https://us1.locationiq.com/v1/search?key={apikey}&q={uriAddress}&format=json";
        var result = await httpClient.GetAsync(uri);
        var json = await result.Content.ReadAsStringAsync();
        
        var latIndex = json.IndexOf("\"lat\":") + 7;
        var latEndIndex = json.IndexOf(",", latIndex) - 1;
        var lat = json.Substring(latIndex, latEndIndex - latIndex);
        var lonIndex = json.IndexOf("\"lon\":") + 7;
        var lonEndIndex = json.IndexOf(",", lonIndex) - 1;
        var lon = json.Substring(lonIndex, lonEndIndex - lonIndex);

        user.MaintainerLatitude = decimal.Parse(lat);
        user.MaintainerLongitude = decimal.Parse(lon);
            
        await dbContext.SaveChangesAsync();
        
        return RedirectToAction("Index", new { id = user.Id });
    }
}