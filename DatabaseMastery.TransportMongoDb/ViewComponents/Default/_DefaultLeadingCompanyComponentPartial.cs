using DatabaseMastery.TransportMongoDb.Services.BrandServices;
using DatabaseMastery.TransportMongoDb.Services.ShipmentServices;
using DatabaseMastery.TransportMongoDb.Services.TestimonialServices;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace DatabaseMastery.TransportMongoDb.ViewComponents.Default
{
    public class _DefaultLeadingCompanyComponentPartial:ViewComponent
    {
        private readonly IShipmentService _shipmentService;
        private readonly ITestimonialService _testimonialService;
        private readonly IBrandService _brandService;

        public _DefaultLeadingCompanyComponentPartial(IShipmentService shipmentService, ITestimonialService testimonialService, IBrandService brandService)
        {
            _shipmentService = shipmentService;
            _testimonialService = testimonialService;
            _brandService = brandService;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            ViewBag.a1 = await _shipmentService.GetTotalShipmentCountAsync();//toplam kargo sayısı
            ViewBag.a2 = await _shipmentService.GetDeliveredShipmentCountAsync();//teslim edilen kargo sayısı
            ViewBag.a3 = await _shipmentService.GetDistinctDestinationCityCountAsync();//kaç farklı şehir
            ViewBag.a4 = await _testimonialService.GetTestimonialCountAsync();//memnun müşteri sayısı
            ViewBag.a5 = await _brandService.GetBrandCountAsync();//destek verdiğimiz şirket sayısı
            return View();
        }
    }
}
