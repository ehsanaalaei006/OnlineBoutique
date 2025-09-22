using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using OnlineBoutiqueCoreLayer.Services;
using OnlineBoutiqueDataLayer.Entities;
using System.Security.Claims;

namespace OnlineBoutique.Pages
{
    public class CartModel : PageModel
    {
        private readonly CartService _cartService;

        public CartModel(CartService cartService)
        {
            _cartService = cartService;
        }

        [BindProperty]
        public Cart Cart { get; set; }

        public void OnGet()
        {
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (userIdClaim == null)
            {
                // redirect to login or show error
                RedirectToPage("/Account/Login");
                return;
            }

            int userId = int.Parse(userIdClaim);
            Cart = _cartService.GetCart(userId) ?? new Cart { Items = new List<CartItem>() };
        }

        public IActionResult OnPostAdd(int productId, string productName, string size, int unitPrice, int amount)
        {
            var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
            _cartService.AddItem(userId, productId, productName, size, unitPrice, amount);
            return RedirectToPage();
        }

        public IActionResult OnPostUpdate(int productId, string size, int amount)
        {
            var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
            _cartService.UpdateItemAmount(userId, productId, size, amount);
            return RedirectToPage();
        }

        public IActionResult OnPostDelete(int productId, string size)
        {
            var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
            _cartService.DeleteItem(userId, productId, size);
            return RedirectToPage();
        }
    }
}
