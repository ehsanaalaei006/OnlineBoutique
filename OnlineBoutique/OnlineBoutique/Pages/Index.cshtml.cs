using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using OnlineBoutiqueCoreLayer.Services;
using OnlineBoutiqueDataLayer.Entities;
using System.Threading.Tasks;

namespace OnlineBoutique.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private readonly ICategoryService _categoryService;
        private readonly IItemService _itemService;

        public IndexModel(ILogger<IndexModel> logger, CategoryService categoryService, ItemService itemService)
        {
            _logger = logger;
            _categoryService = categoryService;
            _itemService = itemService;
        }

        public List<Category> categories { get; set; }
        public List<Item> newestItems { get; set; }

        public async Task OnGet()
        {
            categories = (await _categoryService.GetAllCategoriesAndChildrenAsync()).Take(3).ToList();
            newestItems = (await _itemService.GetAllItemsAsync()).Take(4).ToList();
        }
    }
}