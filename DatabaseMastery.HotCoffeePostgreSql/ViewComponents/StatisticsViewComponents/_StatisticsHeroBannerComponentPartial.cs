using DatabaseMastery.HotCoffeePostgreSql.Context;
using Microsoft.AspNetCore.Mvc;

namespace DatabaseMastery.HotCoffeePostgreSql.ViewComponents.StatisticsViewComponents
{
    public class _StatisticsHeroBannerComponentPartial : ViewComponent
    {
        private readonly AppDbContext _context;

        public _StatisticsHeroBannerComponentPartial(AppDbContext context)
        {
            _context = context;
        }

        public IViewComponentResult Invoke()
        {
            ViewBag.totalRezervationCount = _context.Reservations.Count();
            ViewBag.totalCustomerCount = _context.Reservations.Sum(x => x.GuestCount);
            ViewBag.avgReview = _context.Reviews.Average(x => x.Rating).ToString("0.00");
            ViewBag.totalReview = _context.Reviews.Count();
            ViewBag.activeProduct = _context.Products.Where(x => x.Status == true).Count();
            ViewBag.categoryCount = _context.Categories.Count();

            var today = DateTime.UtcNow.Date;
            var startOfWeek = today.AddDays(-(int)today.DayOfWeek + (int)DayOfWeek.Monday);
            var endOfWeek = startOfWeek.AddDays(7);

            ViewBag.thisWeekTotalGuestCount = _context.Reservations
                .Where(x => x.ReservationDate.Date >= startOfWeek
                        && x.ReservationDate < endOfWeek
                        && x.Status == "Onaylandı")
                .Sum(x => x.GuestCount);

            return View();
        }
    }
}
