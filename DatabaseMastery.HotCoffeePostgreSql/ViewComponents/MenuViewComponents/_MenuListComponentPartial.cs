using DatabaseMastery.HotCoffeePostgreSql.Context;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DatabaseMastery.HotCoffeePostgreSql.ViewComponents.MenuViewComponents
{
    public class _MenuListComponentPartial:ViewComponent
    {
        private readonly AppDbContext _context;

        public _MenuListComponentPartial(AppDbContext context)
        {
            _context = context;
        }

        public IViewComponentResult Invoke()
        {
            var values = _context.Products
        .Include(x => x.Category)
        .Where(x => x.Status && x.Category.CategoryStatus)
        .OrderBy(x => x.Category.CategoryId)
        .ThenBy(x => x.ProductName)
        .ToList();
            return View(values);
        }
    }
}
