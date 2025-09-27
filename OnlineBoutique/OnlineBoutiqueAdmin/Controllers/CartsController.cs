using Microsoft.AspNetCore.Mvc;
using OnlineBoutiqueCoreLayer.Services;

namespace OnlineBoutiqueAdmin.Controllers
{
    public class CartsController : Controller
    {
        private readonly ICartService _cartService;

        public CartsController(CartService cartService)
        {
            _cartService = cartService;
        }
        public IActionResult Index()
        {
            return View(_cartService.GetAllCarts());
        }
        // GET: Carts/Details/{userId}
        public IActionResult Details(int userId)
        {
            var cart = _cartService.GetCart(userId);
            if (cart == null)
                return NotFound();

            return View(cart);
        }
    }
}
