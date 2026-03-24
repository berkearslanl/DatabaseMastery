using DatabaseMastery.HotCoffeePostgreSql.Context;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DatabaseMastery.HotCoffeePostgreSql.ViewComponents.StatisticsViewComponents
{
    public class _StatisticsTopProductsComponentPartial : ViewComponent
    {
        private readonly AppDbContext _context;

        public _StatisticsTopProductsComponentPartial(AppDbContext context)
        {
            _context = context;
        }

        public IViewComponentResult Invoke()
        {
            // Önce DB'den ham veriyi çek, sonra client-side hesapla
            var rawProducts = _context.Products
                .Where(p => p.Status)
                .Include(p => p.Category)
                .Include(p => p.Reviews)
                .Where(p => p.Reviews.Any())
                .ToList();

            var topProducts = rawProducts
                .Select(p => new
                {
                    p.ProductName,
                    p.ImageUrl,
                    CategoryName = p.Category.CategoryName,
                    ReviewCount = p.Reviews.Count,
                    AvgRating = Math.Round(p.Reviews.Average(r => (double)r.Rating), 1)
                })
                .OrderByDescending(p => p.AvgRating)
                .ThenByDescending(p => p.ReviewCount)
                .Take(5)
                .ToList();

            ViewBag.TopProducts = topProducts;
            return View();
        }
    }
}
