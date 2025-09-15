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
        public string? CurrentSearch { get; set; }
        private const int PageSize = 3;

        // Initial page load
        public async Task OnGetAsync(int? categoryId , string? q)
        {


            var allItems = new List<Item>();

            if (categoryId.HasValue)
            {
                var category = await _categoryService.GetCategoryByIdAsync(categoryId.Value);
                CategoryName = category?.Name;
                allItems = (await _itemService.GetItemsByCategoryAsync(categoryId.Value)).ToList();
            }
            else if (q != null)
            {
                allItems= (await _itemService.GetItemsBySearchAsync(q)).ToList();
            }
            else
            {
                allItems = (await _itemService.GetAllItemsAsync()).ToList();
            }

            TotalPages = (int)Math.Ceiling(allItems.Count() / (double)PageSize);
            PagedItems = allItems
                .Take(PageSize)
                .ToList();

            CurrentSearch = q;
            CurrentCategoryId = categoryId;
            Categories = (await _categoryService.GetAllCategoriesAsync()).ToList();
        }

        // AJAX pagination handler
        public async Task<IActionResult> OnGetProductGridAsync(int? categoryId  ,string? q, int pageId = 1)
        {
            var allItems = new List<Item>();

            if (categoryId.HasValue)
            {
                var category = await _categoryService.GetCategoryByIdAsync(categoryId.Value);
                CategoryName = category?.Name;
                allItems = (await _itemService.GetItemsByCategoryAsync(categoryId.Value)).ToList();
            }
            else if (q != null)
            {
                allItems = (await _itemService.GetItemsBySearchAsync(q)).ToList();
            }
            else
            {
                allItems = (await _itemService.GetAllItemsAsync()).ToList();
            }

            var pagedItems = allItems
                .Skip((pageId - 1) * PageSize)
                .Take(PageSize)
                .ToList();

            return Partial("_ProductGridPartial", pagedItems);
        }
    }
}

