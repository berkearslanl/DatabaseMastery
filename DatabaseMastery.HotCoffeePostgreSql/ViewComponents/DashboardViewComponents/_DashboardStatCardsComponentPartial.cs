using DatabaseMastery.HotCoffeePostgreSql.Services.DashboardServices;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace DatabaseMastery.HotCoffeePostgreSql.ViewComponents.DashboardViewComponents
{
    public class _DashboardStatCardsComponentPartial : ViewComponent
    {
        private readonly IDashboardService _dashboardService;

        public _DashboardStatCardsComponentPartial(IDashboardService dashboardService)
        {
            _dashboardService = dashboardService;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            ViewBag.totalReservationCount = await _dashboardService.GetTotalReservationCountAsync();
            ViewBag.pendingReservationCount = await _dashboardService.GetPendingReservationCountAsync();
            ViewBag.approvedReservationCount = await _dashboardService.GetApprovedReservationCountAsync();
            ViewBag.cancelledReservationCount = await _dashboardService.GetCancelledReservationCountAsync();
            ViewBag.todayReservationCount = await _dashboardService.GetTodayReservationCountAsync();
            ViewBag.totalCustomerCount = await _dashboardService.GetTotalCustomerCountAsync();
            ViewBag.totalMenuProductCount = await _dashboardService.GetTotalMenuProductCountAsync();
            ViewBag.todayOrderCount = await _dashboardService.GetTodayOrderCountAsync();

            return View();
        }
    }
}
