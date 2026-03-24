using DatabaseMastery.HotCoffeePostgreSql.Services.ChartServices;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace DatabaseMastery.HotCoffeePostgreSql.ViewComponents.ChartsViewComponents
{
    public class _DashboardLineChartComponentPartial:ViewComponent
    {
        private readonly IChartService _chartService;

        public _DashboardLineChartComponentPartial(IChartService chartService)
        {
            _chartService = chartService;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var values = await _chartService.GetLast7DaysReservationCountAsync();
            return View(values);
        }
    }
}
