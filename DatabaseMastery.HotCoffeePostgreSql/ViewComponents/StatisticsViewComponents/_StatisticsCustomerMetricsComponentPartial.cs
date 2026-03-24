using DatabaseMastery.HotCoffeePostgreSql.Context;
using Microsoft.AspNetCore.Mvc;

namespace DatabaseMastery.HotCoffeePostgreSql.ViewComponents.StatisticsViewComponents
{
    public class _StatisticsCustomerMetricsComponentPartial : ViewComponent
    {
        private readonly AppDbContext _context;

        public _StatisticsCustomerMetricsComponentPartial(AppDbContext context)
        {
            _context = context;
        }

        public IViewComponentResult Invoke()
        {
            var allReservations = _context.Reservations.ToList();

            // Tekrar Gelen Müşteri Oranı (aynı isimle birden fazla rez.)
            var nameGroups = allReservations.GroupBy(r => r.Name.Trim().ToLower()).ToList();
            var totalUniqueNames = nameGroups.Count;
            var returningCount = nameGroups.Count(g => g.Count() > 1);
            ViewBag.ReturningRate = totalUniqueNames > 0
                ? Math.Round((double)returningCount / totalUniqueNames * 100, 0)
                : 0;

            // Bu Ay Yeni Müşteri (bu ay ilk kez gelen)
            var now = DateTime.UtcNow;
            var startOfMonth = new DateTime(now.Year, now.Month, 1, 0, 0, 0, DateTimeKind.Utc);
            var thisMonthNames = allReservations
                .Where(r => r.ReservationDate >= startOfMonth)
                .Select(r => r.Name.Trim().ToLower())
                .Distinct()
                .ToList();
            var previousNames = allReservations
                .Where(r => r.ReservationDate < startOfMonth)
                .Select(r => r.Name.Trim().ToLower())
                .Distinct()
                .ToHashSet();
            var newCustomerCount = thisMonthNames.Count(n => !previousNames.Contains(n));
            ViewBag.NewCustomerCount = newCustomerCount;

            // Ortalama Grup Büyüklüğü
            ViewBag.AvgGroup = allReservations.Any()
                ? Math.Round(allReservations.Average(r => (double)r.GuestCount), 1)
                : 0;

            // Yorum Yapan Müşteri Oranı
            var totalReviews = _context.Reviews.Count();
            var totalReservations = allReservations.Count;
            ViewBag.ReviewRate = totalReservations > 0
                ? Math.Round((double)totalReviews / totalReservations * 100, 0)
                : 0;

            // En Yoğun Gün
            var dayNames = new[] { "Paz", "Pzt", "Sal", "Çar", "Per", "Cum", "Cmt" };
            if (allReservations.Any())
            {
                var busiestDay = allReservations
                    .GroupBy(r => r.ReservationDate.DayOfWeek)
                    .OrderByDescending(g => g.Count())
                    .First();
                ViewBag.BusiestDay = dayNames[(int)busiestDay.Key];
            }
            else
            {
                ViewBag.BusiestDay = "—";
            }

            // En Yoğun Saat
            if (allReservations.Any())
            {
                var busiestHour = allReservations
                    .GroupBy(r => r.ReservationTime.Hours)
                    .OrderByDescending(g => g.Count())
                    .First();
                ViewBag.BusiestHour = $"{busiestHour.Key:D2}:00";
            }
            else
            {
                ViewBag.BusiestHour = "—";
            }

            return View();
        }
    }
}
