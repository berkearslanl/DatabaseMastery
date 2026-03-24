using Microsoft.AspNetCore.Mvc;

namespace DatabaseMastery.HotCoffeePostgreSql.ViewComponents.DashboardViewComponents
{
    public class _DashboardChartsComponentPartial : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}
