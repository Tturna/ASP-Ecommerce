using ASP_Ecommerce.Models;
using ASP_Ecommerce.Models.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace ASP_Ecommerce.Controllers;

public class ProductController(UserManager<UserModel> userManager) : Controller
{
    public IActionResult Index()
    {
        return View();
    }

    [Authorize(Roles = "Admin"), Route("/product/add")]
    public IActionResult AddProduct()
    {
        return View();
    }
}