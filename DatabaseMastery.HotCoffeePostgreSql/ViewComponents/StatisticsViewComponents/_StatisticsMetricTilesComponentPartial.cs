using DatabaseMastery.HotCoffeePostgreSql.Context;
using Microsoft.AspNetCore.Mvc;

namespace DatabaseMastery.HotCoffeePostgreSql.ViewComponents.StatisticsViewComponents
{
    public class _StatisticsMetricTilesComponentPartial : ViewComponent
    {
        private readonly AppDbContext _context;

        public _StatisticsMetricTilesComponentPartial(AppDbContext context)
        {
            _context = context;
        }

        public IViewComponentResult Invoke()
        {
            var now = DateTime.UtcNow;
            var today = now.Date;
            var startOfWeek = today.AddDays(-(int)today.DayOfWeek + (int)DayOfWeek.Monday);
            if (today.DayOfWeek == DayOfWeek.Sunday) startOfWeek = startOfWeek.AddDays(-7);
            var endOfWeek = startOfWeek.AddDays(7);
            var startOfMonth = new DateTime(now.Year, now.Month, 1, 0, 0, 0, DateTimeKind.Utc);

            // Bugünkü Rezervasyon
            ViewBag.TodayReservation = _context.Reservations
                .Count(r => r.ReservationDate.Date == today);

            // Bu Haftaki Rezervasyon
            ViewBag.WeekReservation = _context.Reservations
                .Count(r => r.ReservationDate.Date >= startOfWeek && r.ReservationDate.Date < endOfWeek);

            // Bu Ayki Rezervasyon
            ViewBag.MonthReservation = _context.Reservations
                .Count(r => r.ReservationDate.Date >= startOfMonth);

            // Yayındaki Yorum
            ViewBag.ActiveReview = _context.Reviews.Count(r => r.Status);

            // Gizli Yorum
            ViewBag.HiddenReview = _context.Reviews.Count(r => !r.Status);

            // Aktif Ürün
            ViewBag.ActiveProduct = _context.Products.Count(p => p.Status);

            // Aktif Kategori
            ViewBag.ActiveCategory = _context.Categories.Count(c => c.CategoryStatus);

            // Toplam Müşteri (misafir)
            ViewBag.TotalCustomer = _context.Reservations.Sum(r => r.GuestCount);

            return View();
        }
    }
}
