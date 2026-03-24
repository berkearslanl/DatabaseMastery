using DatabaseMastery.HotCoffeePostgreSql.Services.ChartServices;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace DatabaseMastery.HotCoffeePostgreSql.ViewComponents.ChartsViewComponents
{
    public class _DashboardPieChartComponentPartial:ViewComponent
    {
        private readonly IChartService _chartService;

        public _DashboardPieChartComponentPartial(IChartService chartService)
        {
            _chartService = chartService;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var values = await _chartService.GetCategoryAvgPriceAsync();
            return View(values);
        }
    }
}
