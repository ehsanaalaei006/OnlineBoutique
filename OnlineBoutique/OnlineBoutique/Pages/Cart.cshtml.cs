using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using OnlineBoutiqueCoreLayer.Services;
using OnlineBoutiqueDataLayer.Entities;
using System.Security.Claims;
using System.Threading.Tasks;

namespace OnlineBoutique.Pages
{
    public class CartModel : PageModel
    {
        private readonly CartService _cartService;
        private readonly ItemService _itemService;


        public CartModel(CartService cartService, ItemService itemService)
        {
            _cartService = cartService;
            _itemService = itemService;
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



        public async Task<IActionResult> OnPostDelete(int productId, string size , int amount)
        {
            var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
            await _itemService.UpdateItemStockAsync(productId, amount);
            _cartService.DeleteItem(userId, productId, size);
            return RedirectToPage();
        }
    }
}
