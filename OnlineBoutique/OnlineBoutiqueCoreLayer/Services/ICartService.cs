using OnlineBoutiqueDataLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineBoutiqueCoreLayer.Services
{
    public interface ICartService
    {
        public Cart GetCart(int userId);
        public void AddItem(int userId, int productId, string productName, string size, int unitPrice, int amount);
        public void UpdateItemAmount(int userId, int productId, string size, int newAmount);
        public void DeleteItem(int userId, int productId, string size);

    }
}
