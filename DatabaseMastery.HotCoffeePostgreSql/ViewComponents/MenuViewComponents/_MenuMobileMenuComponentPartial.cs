using Microsoft.AspNetCore.Mvc;

namespace DatabaseMastery.HotCoffeePostgreSql.ViewComponents.MenuViewComponents
{
    public class _MenuMobileMenuComponentPartial:ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}
