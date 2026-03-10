using DatabaseMastery.TransportMongoDb.Services.BrandServices;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace DatabaseMastery.TransportMongoDb.ViewComponents.Default
{
    public class _DefaultBrandComponentPartial:ViewComponent
    {
        private readonly IBrandService _brandService;

        public _DefaultBrandComponentPartial(IBrandService brandService)
        {
            _brandService = brandService;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var values = await _brandService.GetAllBrandAsync();
            return View(values);
        }
    }
}
