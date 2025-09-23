using Microsoft.EntityFrameworkCore;
using OnlineBoutiqueDataLayer.Context;
using OnlineBoutiqueDataLayer.Entities;
using System.Threading.Tasks;

namespace OnlineBoutiqueCoreLayer.Services
{
    public class CartService : ICartService
    {
        private readonly AppDbContext _context;

        public CartService(AppDbContext context)
        {
            _context = context;
        }

        public Cart GetCart(int userId)
        {
            return _context.Carts
                .Include(c => c.Items)
                .FirstOrDefault(c => c.UserId == userId);
        }

        public void AddItem(int userId, int productId, string productName, string size, int unitPrice, int amount)
        {
            var cart = GetCart(userId) ?? new Cart { UserId = userId, Items = new List<CartItem>() };

            var existingItem = cart.Items.FirstOrDefault(i => i.ProductId == productId && i.Size == size);
            if (existingItem != null)
            {
                existingItem.Amount += amount;
            }
            else
            {
                cart.Items.Add(new CartItem
                {
                    ProductId = productId,
                    ProductName = productName,
                    Size = size,
                    UnitPrice = unitPrice,
                    Amount = amount
                });
            }

            cart.TotalPrice = cart.Items.Sum(i => i.TotalPrice);

            if (cart.CartId == 0)
                _context.Carts.Add(cart);

            _context.SaveChanges();
        }

        public void UpdateItemAmount(int userId, int productId, string size, int newAmount)
        {
            var cart = GetCart(userId);
            var item = cart?.Items.FirstOrDefault(i => i.ProductId == productId && i.Size == size);
            if (item != null)
            {
                item.Amount = newAmount;
                cart.TotalPrice = cart.Items.Sum(i => i.TotalPrice);
                _context.SaveChanges();
            }
        }

        public async void DeleteItem(int userId, int productId, string size)
        {
            var cart = GetCart(userId);
            var item = cart?.Items.FirstOrDefault(i => i.ProductId == productId && i.Size == size);
            if (item != null)
            {
                cart.Items.Remove(item);
                _context.CartItems.Remove(item);
                cart.TotalPrice = cart.Items.Sum(i => i.TotalPrice);
                _context.SaveChanges();
            }
        }
    }
}
