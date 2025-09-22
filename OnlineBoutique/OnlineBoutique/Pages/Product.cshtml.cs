using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.RazorPages;
using OnlineBoutiqueCoreLayer.Services;
using OnlineBoutiqueDataLayer.Entities;

namespace OnlineBoutique.Pages
{
    public class ProductModel : PageModel
    {
        private readonly IItemService _itemService;
        private readonly ICartService _cartService;


        public ProductModel(ItemService itemService, CartService cartService)
        {
            _itemService = itemService;
            _cartService = cartService;
        }

        public Item item { get; set; }
        public List<String> itemSizes { get; set; }
        public List<Item> relatedItems { get; set; }
        public async Task OnGetAsync(int itemId)
        {
            item = await _itemService.GetItemByIdAsync(itemId);
            itemSizes = item.Size
                .Split(',')
                .Select(s => s.Trim())
                .Where(s => !string.IsNullOrEmpty(s))
                .ToList();

            relatedItems = (await _itemService.GetItemsByCategoryAsync(item.CategoryId)).ToList();
            if(relatedItems.Contains(item)){
                relatedItems.Remove(item);
            }
        }

        //add to cart 


        [BindProperty]
        public string productSize { get; set; }

        [BindProperty]
        public int productQuantity { get; set; }

        [BindProperty]
        public int productId { get; set; }

        [BindProperty]
        public string productName { get; set; }

        [BindProperty]
        public int unitPrice { get; set; }

        public bool AddToCartSuccess { get; set; } = false;

        public async Task<IActionResult> OnPostAddToCartAsync()
        {
            // Get user id from claims
            var userIdClaim = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;
            if (userIdClaim == null) return RedirectToPage("/Account/Login");
            int userId = int.Parse(userIdClaim);

            // Call your cart service
            _cartService.AddItem(userId, productId, productName, productSize, unitPrice, productQuantity);

            AddToCartSuccess = true;
            return RedirectToPage("Cart");
        }




    }
}
