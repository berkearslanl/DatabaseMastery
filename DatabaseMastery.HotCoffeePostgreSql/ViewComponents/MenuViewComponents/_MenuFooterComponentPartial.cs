using Microsoft.AspNetCore.Mvc;

namespace DatabaseMastery.HotCoffeePostgreSql.ViewComponents.MenuViewComponents
{
    public class _MenuFooterComponentPartial:ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}
