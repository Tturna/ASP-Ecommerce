using ASP_Ecommerce.Models;
using ASP_Ecommerce.Models.DataTransferObjects;
using ASP_Ecommerce.Models.ViewModels;
using ASP_Ecommerce.Services;
using Microsoft.AspNetCore.Mvc;

namespace ASP_Ecommerce.Controllers;

public class ShoppingCartController(ShoppingCartService cartService) : Controller
{
    public IActionResult Index()
    {
        return View();
    }

    [HttpPost]
    public IActionResult GetCart([FromBody] string? localStorageItemDictJson)
    {
        var identity = User.Identity;
        CartItemData[]? cartItems = null;

        if (identity is { Name: not null })
        {
            var cart = cartService.GetCart(identity.Name);
            cartItems = cart?.CartItems.ToArray();
        }
        else if (localStorageItemDictJson != null)
        {
            cartItems = cartService.GetItemsFromLocalStorageJson(localStorageItemDictJson);
        }
        
        var cartOverlayViewModel = new CartOverlayViewModel()
        {
            CartItemDatas = cartItems
        };
        
        return PartialView("_CartOverlay", cartOverlayViewModel);
    }

    [HttpPost]
    public IActionResult Add([FromBody] AddCartItemDto? cartItemData)
    {
        if (cartItemData is not { ProductId: not null })
        {
            return BadRequest();
        }
        
        var identity = User.Identity;
        CartItemData[]? cartItems = null;

        if (identity is { Name: not null })
        {
            var cart = cartService.AddItemToCart(identity.Name, cartItemData.ProductId.Value);
            cartItems = cart?.CartItems.ToArray();
        }
        else if (cartItemData.LocalStorageItemDictJson != null)
        {
            cartItems = cartService.GetItemsFromLocalStorageJson(cartItemData.LocalStorageItemDictJson);
        }
        
        var cartOverlayViewModel = new CartOverlayViewModel()
        {
            CartItemDatas = cartItems
        };
        
        return PartialView("_CartOverlay", cartOverlayViewModel);
    }

    [HttpPost]
    public IActionResult Remove([FromBody] RemoveCartItemDto cartItemData)
    {
        if (cartItemData is not { ProductId: not null })
        {
            return BadRequest();
        }

        var identity = User.Identity;
        CartItemData[]? cartItems = null;
        
        if (identity is { Name: not null })
        {
            var cart = cartService.RemoveItemFromCart(identity.Name, cartItemData.ProductId.Value, cartItemData.Quantity ?? 1);
            cartItems = cart?.CartItems.ToArray();
        }
        else if (cartItemData.LocalStorageItemDictJson != null)
        {
            cartItems = cartService.GetItemsFromLocalStorageJson(cartItemData.LocalStorageItemDictJson);
        }
        
        var cartOverlayViewModel = new CartOverlayViewModel()
        {
            CartItemDatas = cartItems
        };
        
        return PartialView("_CartOverlay", cartOverlayViewModel);
    }

    public IActionResult EmptyCart()
    {
        var identity = User.Identity;
        CartItemData[]? cartItems = null;

        if (identity is { Name: not null })
        {
            var cart = cartService.EmptyCart(identity.Name);
            cartItems = cart?.CartItems.ToArray();
        }
        
        var cartOverlayViewModel = new CartOverlayViewModel()
        {
            CartItemDatas = cartItems
        };
        
        return PartialView("_CartOverlay", cartOverlayViewModel);
    }
}