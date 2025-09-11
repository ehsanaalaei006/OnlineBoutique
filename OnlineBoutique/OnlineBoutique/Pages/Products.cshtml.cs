using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using OnlineBoutiqueCoreLayer.Services;
using OnlineBoutiqueDataLayer.Entities;
using System.Threading.Tasks;

namespace OnlineBoutique.Pages
{
    public class ProductsModel : PageModel
    {
        private readonly IItemService _itemService;

        public ProductsModel(ItemService itemService)
        {
            _itemService = itemService;
        }


        public List<Item> Items { get; set; }

        public async Task OnGetAsync(int? id)
        {
            if (id.HasValue)
            {
                Items = (await _itemService.GetItemsByCategoryAsync(id.Value)).ToList();
            }
            else
            {
                Items = (await _itemService.GetAllItemsAsync()).ToList();
            }
        }
    }
}
