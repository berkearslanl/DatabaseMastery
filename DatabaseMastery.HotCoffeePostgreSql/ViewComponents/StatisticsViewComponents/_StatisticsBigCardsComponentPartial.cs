using DatabaseMastery.HotCoffeePostgreSql.Context;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DatabaseMastery.HotCoffeePostgreSql.ViewComponents.StatisticsViewComponents
{
    public class _StatisticsBigCardsComponentPartial : ViewComponent
    {
        private readonly AppDbContext _context;

        public _StatisticsBigCardsComponentPartial(AppDbContext context)
        {
            _context = context;
        }

        public IViewComponentResult Invoke()
        {
            // Ortalama grup büyüklüğü (rezervasyon başına kişi)
            var totalReservation = _context.Reservations.Count();
            var totalGuest = _context.Reservations.Sum(r => r.GuestCount);
            ViewBag.avgGroupSize = totalReservation > 0
                ? Math.Round((double)totalGuest / totalReservation, 1)
                : 0;

            // Ortalama müşteri puanı
            ViewBag.avgRating = _context.Reviews.Any()
                ? Math.Round(_context.Reviews.Average(r => r.Rating), 1)
                : 0;
            ViewBag.totalReview = _context.Reviews.Count();

            // Günlük ortalama rezervasyon (son 30 gün)
            var thirtyDaysAgo = DateTime.UtcNow.Date.AddDays(-30);
            var last30Count = _context.Reservations
                .Count(r => r.ReservationDate.Date >= thirtyDaysAgo);
            ViewBag.dailyAvgReservation = Math.Round((double)last30Count / 30, 1);

            return View();
        }
    }
}
