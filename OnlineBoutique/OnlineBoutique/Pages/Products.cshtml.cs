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
        private readonly ICategoryService _categoryService;


        public ProductsModel(ItemService itemService, ICategoryService categoryService)
        {
            _itemService = itemService;
            _categoryService = categoryService;
        }


        public List<Item> Items { get; set; }
        public List<Category> Categories { get; set; }

        public string? CategoryName { get; set; }
        public async Task OnGetAsync(int? id)
        {
            //items
            if (id.HasValue)
            {
                var category = await _categoryService.GetCategoryByIdAsync(id.Value);
                CategoryName = category.Name;
                Items = (await _itemService.GetItemsByCategoryAsync(id.Value)).ToList();
            }
            else
            {
                Items = (await _itemService.GetAllItemsAsync()).ToList();
            }



            //categories
            Categories = (await _categoryService.GetAllCategoriesAsync()).ToList();

        }
    }
}
