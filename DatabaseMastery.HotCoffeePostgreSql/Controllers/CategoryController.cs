using DatabaseMastery.HotCoffeePostgreSql.Dtos.CategoryDtos;
using DatabaseMastery.HotCoffeePostgreSql.Services.ActivityLogServices;
using DatabaseMastery.HotCoffeePostgreSql.Services.CategoryServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace DatabaseMastery.HotCoffeePostgreSql.Controllers
{
    [Authorize]
    public class CategoryController : Controller
    {
        private readonly ICategoryService _categoryService;
        private readonly IActivityLogService _activityLogService;

        public CategoryController(ICategoryService categoryService, IActivityLogService activityLogService)
        {
            _categoryService = categoryService;
            _activityLogService = activityLogService;
        }

        public async Task<IActionResult> CategoryList()
        {
            var values = await _categoryService.GetAllCategoriesAsync();
            return View(values);
        }
        [HttpGet]
        public IActionResult CreateCategory()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> CreateCategory(CreateCategoryDto createCategoryDto)
        {
            await _categoryService.CreateCategoryAsync(createCategoryDto);
            _activityLogService.LogActivity(
                "Yeni kategori eklendi",
                $"{createCategoryDto.CategoryName} kategorisi oluşturuldu",
                "bi bi-folder-plus",
                "var(--purple)",
                "var(--purple-soft)"
            );
            return RedirectToAction("CategoryList");
        }
        [HttpGet]
        public async Task<IActionResult> UpdateCategory(int id)
        {
            var values = await _categoryService.GetCategoryByIdAsync(id);
            return View(values);
        }
        [HttpPost]
        public async Task<IActionResult> UpdateCategory(UpdateCategoryDto updateCategoryDto)
        {
            await _categoryService.UpdateCategoryAsync(updateCategoryDto);
            _activityLogService.LogActivity(
                "Kategori güncellendi",
                $"{updateCategoryDto.CategoryName} kategorisi düzenlendi",
                "bi bi-folder-symlink",
                "var(--blue)",
                "var(--blue-soft)"
            );
            return RedirectToAction("CategoryList");
        }
        public async Task<IActionResult> DeleteCategory(int id)
        {
            await _categoryService.DeleteCategoryAsync(id);
            return RedirectToAction("CategoryList");
        }
    }
}
