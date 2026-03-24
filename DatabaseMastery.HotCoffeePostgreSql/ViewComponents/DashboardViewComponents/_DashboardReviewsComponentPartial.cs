using DatabaseMastery.HotCoffeePostgreSql.Context;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DatabaseMastery.HotCoffeePostgreSql.ViewComponents.DashboardViewComponents
{
    public class _DashboardReviewsComponentPartial : ViewComponent
    {
        private readonly AppDbContext _context;

        public _DashboardReviewsComponentPartial(AppDbContext context)
        {
            _context = context;
        }

        public IViewComponentResult Invoke()
        {
            var latestReviews = _context.Reviews
                .Include(r => r.Product)
                .OrderByDescending(r => r.CreatedAt)
                .Take(4)
                .ToList();

            ViewBag.LatestReviews = latestReviews;
            return View();
        }
    }
}
