using DatabaseMastery.HotCoffeePostgreSql.Dtos.ProductDtos;
using DatabaseMastery.HotCoffeePostgreSql.Services.ActivityLogServices;
using DatabaseMastery.HotCoffeePostgreSql.Services.CategoryServices;
using DatabaseMastery.HotCoffeePostgreSql.Services.ProductServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Threading.Tasks;

namespace DatabaseMastery.HotCoffeePostgreSql.Controllers
{
    [Authorize]
    public class ProductController : Controller
    {
        private readonly IProductService _productService;
        private readonly ICategoryService _categoryService;
        private readonly IActivityLogService _activityLogService;

        public ProductController(IProductService ProductService, ICategoryService categoryService, IActivityLogService activityLogService)
        {
            _productService = ProductService;
            _categoryService = categoryService;
            _activityLogService = activityLogService;
        }

        public async Task<IActionResult> ProductList()
        {
            var values = await _productService.GetAllProductAsync();
            return View(values);
        }
        [HttpGet]
        public async Task<IActionResult> CreateProduct()
        {
            var categories = await _categoryService.GetAllCategoriesAsync();
            ViewBag.Categories = new SelectList(categories, "CategoryId", "CategoryName");
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> CreateProduct(CreateProductDto createProductDto)
        {
            await _productService.CreateProductAsync(createProductDto);
            _activityLogService.LogActivity(
                "Yeni ürün eklendi",
                $"{createProductDto.ProductName} — ₺ {createProductDto.Price}",
                "bi bi-box-seam",
                "var(--green)",
                "var(--green-soft)"
            );
            return RedirectToAction("ProductList");
        }
        [HttpGet]
        public async Task<IActionResult> UpdateProduct(int id)
        {
            var categories = await _categoryService.GetAllCategoriesAsync();
            ViewBag.Categories = new SelectList(categories, "CategoryId", "CategoryName");
            var values = await _productService.GetProductByIdAsync(id);
            return View(values);
        }
        [HttpPost]
        public async Task<IActionResult> UpdateProduct(UpdateProductDto updateProductDto)
        {
            await _productService.UpdateProductAsync(updateProductDto);
            _activityLogService.LogActivity(
                "Ürün güncellendi",
                $"{updateProductDto.ProductName} düzenlendi",
                "bi bi-pencil-square",
                "var(--blue)",
                "var(--blue-soft)"
            );
            return RedirectToAction("ProductList");
        }
        public async Task<IActionResult> DeleteProduct(int id)
        {
            await _productService.DeleteProductAsync(id);
            return RedirectToAction("ProductList");
        }
    }
}
