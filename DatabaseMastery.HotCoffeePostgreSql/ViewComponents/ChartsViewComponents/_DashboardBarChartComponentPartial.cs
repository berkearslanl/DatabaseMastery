using DatabaseMastery.HotCoffeePostgreSql.Services.ChartServices;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace DatabaseMastery.HotCoffeePostgreSql.ViewComponents.ChartsViewComponents
{
    public class _DashboardBarChartComponentPartial:ViewComponent
    {
        private readonly IChartService _chartService;

        public _DashboardBarChartComponentPartial(IChartService chartService)
        {
            _chartService = chartService;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var values = await _chartService.GetCategoryProductCountAsync();
            return View(values);
        }
    }
}
