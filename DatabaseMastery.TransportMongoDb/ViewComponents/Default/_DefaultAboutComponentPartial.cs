using DatabaseMastery.TransportMongoDb.Services.AboutServices;
using Microsoft.AspNetCore.Mvc;

namespace DatabaseMastery.TransportMongoDb.ViewComponents.Default
{
    public class _DefaultAboutComponentPartial:ViewComponent
    {
        private readonly IAboutService _aboutService;

        public _DefaultAboutComponentPartial(IAboutService AboutService)
        {
            _aboutService = AboutService;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var values = await _aboutService.GetAllAboutAsync();
            return View(values);
        }
    }
}
