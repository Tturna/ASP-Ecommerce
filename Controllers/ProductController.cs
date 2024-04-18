using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ASP_Ecommerce.Controllers;

public class ProductController : Controller
{
    public IActionResult Index()
    {
        return View();
    }

    [Authorize, Route("/product/add")]
    public IActionResult AddProduct()
    {
        return View();
    }
}