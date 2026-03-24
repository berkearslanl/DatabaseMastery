using DatabaseMastery.HotCoffeePostgreSql.Context;
using Microsoft.AspNetCore.Mvc;

namespace DatabaseMastery.HotCoffeePostgreSql.ViewComponents.DashboardViewComponents
{
    public class _DashboardWeeklySummaryComponentPartial : ViewComponent
    {
        private readonly AppDbContext _context;

        public _DashboardWeeklySummaryComponentPartial(AppDbContext context)
        {
            _context = context;
        }

        public IViewComponentResult Invoke()
        {
            var now = DateTime.UtcNow;
            var today = now.Date;

            // Bu haftanın başlangıcı (Pazartesi)
            var startOfWeek = today.AddDays(-(int)today.DayOfWeek + (int)DayOfWeek.Monday);
            if (today.DayOfWeek == DayOfWeek.Sunday) startOfWeek = startOfWeek.AddDays(-7);
            var endOfWeek = startOfWeek.AddDays(7);

            // Geçen haftanın aralığı
            var startOfLastWeek = startOfWeek.AddDays(-7);
            var endOfLastWeek = startOfWeek;

            // Bu haftaki rezervasyonlar
            var thisWeekReservations = _context.Reservations
                .Where(r => r.ReservationDate.Date >= startOfWeek && r.ReservationDate.Date < endOfWeek)
                .ToList();

            var thisWeekTotal = thisWeekReservations.Count;
            var thisWeekApproved = thisWeekReservations.Count(r => r.Status == "Onaylandı");
            var thisWeekCancelled = thisWeekReservations.Count(r => r.Status == "İptal Edildi");
            var thisWeekGuests = thisWeekReservations.Sum(r => r.GuestCount);

            // Onay Oranı
            ViewBag.ApprovalRate = thisWeekTotal > 0
                ? (int)Math.Round((double)thisWeekApproved / thisWeekTotal * 100)
                : 0;

            // İptal Oranı
            ViewBag.CancelRate = thisWeekTotal > 0
                ? (int)Math.Round((double)thisWeekCancelled / thisWeekTotal * 100)
                : 0;

            // Müşteri Memnuniyeti (bu haftaki yorumların ort. puanı / 5 * 100)
            var thisWeekReviews = _context.Reviews
                .Where(r => r.CreatedAt.Date >= startOfWeek && r.CreatedAt.Date < endOfWeek)
                .ToList();

            if (thisWeekReviews.Any())
            {
                var avgRating = thisWeekReviews.Average(r => r.Rating);
                ViewBag.SatisfactionRate = (int)Math.Round(avgRating / 5.0 * 100);
            }
            else
            {
                // Bu hafta yorum yoksa tüm zamanların ortalamasını al
                var allReviews = _context.Reviews.ToList();
                if (allReviews.Any())
                {
                    var avgRating = allReviews.Average(r => r.Rating);
                    ViewBag.SatisfactionRate = (int)Math.Round(avgRating / 5.0 * 100);
                }
                else
                {
                    ViewBag.SatisfactionRate = 0;
                }
            }

            // Haftalık Rez. Yoğunluğu (bu hafta / geçen hafta oranı)
            var lastWeekTotal = _context.Reservations
                .Count(r => r.ReservationDate.Date >= startOfLastWeek && r.ReservationDate.Date < endOfLastWeek);

            if (lastWeekTotal > 0)
            {
                var ratio = (double)thisWeekTotal / lastWeekTotal * 100;
                ViewBag.DensityRate = (int)Math.Min(Math.Round(ratio), 100);
            }
            else
            {
                ViewBag.DensityRate = thisWeekTotal > 0 ? 100 : 0;
            }

            // Alt kısım: Bu haftanın özeti
            ViewBag.ThisWeekTotal = thisWeekTotal;
            ViewBag.ThisWeekGuests = thisWeekGuests;

            // Geçen haftayla fark
            var lastWeekGuests = _context.Reservations
                .Where(r => r.ReservationDate.Date >= startOfLastWeek && r.ReservationDate.Date < endOfLastWeek)
                .Sum(r => r.GuestCount);

            ViewBag.GuestDiff = thisWeekGuests - lastWeekGuests;
            ViewBag.ReservationDiff = thisWeekTotal - lastWeekTotal;

            return View();
        }
    }
}
