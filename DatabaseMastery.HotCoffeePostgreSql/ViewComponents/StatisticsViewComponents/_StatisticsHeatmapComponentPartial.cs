using DatabaseMastery.HotCoffeePostgreSql.Context;
using Microsoft.AspNetCore.Mvc;

namespace DatabaseMastery.HotCoffeePostgreSql.ViewComponents.StatisticsViewComponents
{
    public class _StatisticsHeatmapComponentPartial : ViewComponent
    {
        private readonly AppDbContext _context;

        public _StatisticsHeatmapComponentPartial(AppDbContext context)
        {
            _context = context;
        }

        public IViewComponentResult Invoke()
        {
            // Saat dilimleri: 10-11, 12-13, 14-15, 16-17, 18-19, 20-21
            var hourSlots = new[] { 10, 12, 14, 16, 18, 20 };

            var reservations = _context.Reservations
                .Select(r => new
                {
                    DayOfWeek = (int)r.ReservationDate.DayOfWeek,
                    Hour = r.ReservationTime.Hours
                })
                .ToList();

            // 6 saat dilimi x 7 gün matrisi
            var heatData = new int[6, 7];

            foreach (var r in reservations)
            {
                // DayOfWeek: 0=Pazar -> index 6, 1=Pzt -> index 0, ...
                int dayIndex = r.DayOfWeek == 0 ? 6 : r.DayOfWeek - 1;

                // Saat dilimini bul
                int slotIndex = -1;
                for (int i = hourSlots.Length - 1; i >= 0; i--)
                {
                    if (r.Hour >= hourSlots[i])
                    {
                        slotIndex = i;
                        break;
                    }
                }

                if (slotIndex >= 0 && slotIndex < 6 && dayIndex >= 0 && dayIndex < 7)
                {
                    heatData[slotIndex, dayIndex]++;
                }
            }

            ViewBag.HeatData = heatData;
            ViewBag.HourSlots = hourSlots;
            return View();
        }
    }
}
