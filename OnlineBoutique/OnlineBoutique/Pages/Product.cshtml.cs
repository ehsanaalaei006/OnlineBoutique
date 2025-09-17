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

        public ProductModel(ItemService itemService)
        {
            _itemService = itemService;
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


    }
}
