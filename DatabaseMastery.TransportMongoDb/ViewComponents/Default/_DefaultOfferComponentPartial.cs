using DatabaseMastery.TransportMongoDb.Services.OfferServices;
using Microsoft.AspNetCore.Mvc;

namespace DatabaseMastery.TransportMongoDb.ViewComponents.Default
{
    public class _DefaultOfferComponentPartial:ViewComponent
    {
        private readonly IOfferService _offerService;

        public _DefaultOfferComponentPartial(IOfferService OfferService)
        {
            _offerService = OfferService;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var values = await _offerService.GetAllOfferAsync();
            return View(values);
        }
    }
}
