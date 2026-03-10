using DatabaseMastery.TransportMongoDb.Services.GetInTouchServices;
using Microsoft.AspNetCore.Mvc;

namespace DatabaseMastery.TransportMongoDb.ViewComponents.Default
{
    public class _DefaultContactComponentPartial:ViewComponent
    {
        private readonly IGetInTouchService _getInTouchService;

        public _DefaultContactComponentPartial(IGetInTouchService GetInTouchService)
        {
            _getInTouchService = GetInTouchService;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var values = await _getInTouchService.GetAllGetInTouchAsync();
            return View(values);
        }
    }
}
