using DatabaseMastery.HotCoffeePostgreSql.Services.DashboardServices;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace DatabaseMastery.HotCoffeePostgreSql.ViewComponents.DashboardViewComponents
{
    public class _DashboardReservationTableComponentPartial : ViewComponent
    {
        private readonly IDashboardService _dashboardService;

        public _DashboardReservationTableComponentPartial(IDashboardService dashboardService)
        {
            _dashboardService = dashboardService;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var values = await _dashboardService.GetTodayReservationListAsync();
            return View(values);
        }
    }
}
