using DatabaseMastery.HotCoffeePostgreSql.Context;
using DatabaseMastery.HotCoffeePostgreSql.Dtos.ReviewDtos;
using DatabaseMastery.HotCoffeePostgreSql.Services.ReviewServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace DatabaseMastery.HotCoffeePostgreSql.Controllers
{
    [Authorize]
    public class ReviewController : Controller
    {
        private readonly IReviewService _reviewService;
        private readonly AppDbContext _context;

        public ReviewController(IReviewService reviewService, AppDbContext context)
        {
            _reviewService = reviewService;
            _context = context;
        }

        // ── LİSTE ──
        public async Task<IActionResult> ReviewList()
        {
            var values = await _reviewService.GetAllReviewsAsync();
            return View(values);
        }

        // ── EKLEME ──
        [HttpGet]
        public async Task<IActionResult> CreateReview()
        {
            ViewBag.Products = new SelectList(
                await _context.Products.Where(p => p.Status).ToListAsync(),
                "ProductId", "ProductName"
            );
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateReview(CreateReviewDto createReviewDto)
        {
            createReviewDto.CreatedAt = DateTime.SpecifyKind(DateTime.Now, DateTimeKind.Utc);
            await _reviewService.CreateReviewAsync(createReviewDto);
            return RedirectToAction("ReviewList");
        }

        // ── SİLME ──
        public async Task<IActionResult> DeleteReview(int id)
        {
            await _reviewService.DeleteReviewAsync(id);
            return RedirectToAction("ReviewList");
        }

        // ── GÜNCELLEME ──
        [HttpGet]
        public async Task<IActionResult> UpdateReview(int id)
        {
            var value = await _reviewService.GetReviewByIdAsync(id);
            ViewBag.Products = new SelectList(
                await _context.Products.Where(p => p.Status).ToListAsync(),
                "ProductId", "ProductName",
                value.ProductId  // mevcut ürünü seçili göster
            );
            return View(value);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateReview(UpdateReviewDto updateReviewDto)
        {
            updateReviewDto.CreatedAt = DateTime.SpecifyKind(updateReviewDto.CreatedAt, DateTimeKind.Utc);
            await _reviewService.UpdateReviewAsync(updateReviewDto);
            return RedirectToAction("ReviewList");
        }
        public async Task<IActionResult> ToggleStatus(int id)
        {
            var review = await _context.Reviews.FindAsync(id);
            review.Status = !review.Status;
            await _context.SaveChangesAsync();
            return RedirectToAction("ReviewList");
        }
    }
}
