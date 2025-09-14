using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using OnlineBoutiqueCoreLayer.Services;
using OnlineBoutiqueDataLayer.Entities;

namespace OnlineBoutique.Pages
{
    public class ProductsModel : PageModel
    {
        private readonly IItemService _itemService;
        private readonly ICategoryService _categoryService;

        public ProductsModel(IItemService itemService, ICategoryService categoryService)
        {
            _itemService = itemService;
            _categoryService = categoryService;
        }

        // Public properties for Razor
        public List<Item> PagedItems { get; set; } = new();
        public List<Category> Categories { get; set; } = new();
        public string? CategoryName { get; set; }

        public int TotalPages { get; set; }
        public int CurrentPage { get; set; }
        public int? CurrentCategoryId { get; set; }

        private const int PageSize = 3;

        // Initial page load
        public async Task OnGetAsync(int? categoryId, int pageId = 1)
        {
            CurrentCategoryId = categoryId;
            CurrentPage = pageId;

            var allItems = categoryId.HasValue
                ? await _itemService.GetItemsByCategoryAsync(categoryId.Value)
                : await _itemService.GetAllItemsAsync();

            if (categoryId.HasValue)
            {
                var category = await _categoryService.GetCategoryByIdAsync(categoryId.Value);
                CategoryName = category?.Name;
            }

            TotalPages = (int)Math.Ceiling(allItems.Count() / (double)PageSize);
            PagedItems = allItems
                .Skip((pageId - 1) * PageSize)
                .Take(PageSize)
                .ToList();

            Categories = (await _categoryService.GetAllCategoriesAsync()).ToList();
        }

        // AJAX pagination handler
        public async Task<IActionResult> OnGetProductGridAsync(int? categoryId, int pageId = 1)
        {
            var allItems = categoryId.HasValue
                ? await _itemService.GetItemsByCategoryAsync(categoryId.Value)
                : await _itemService.GetAllItemsAsync();

            var pagedItems = allItems
                .Skip((pageId - 1) * PageSize)
                .Take(PageSize)
                .ToList();

            return Partial("_ProductGridPartial", pagedItems);
        }
    }
}

