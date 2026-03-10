using DatabaseMastery.TransportMongoDb.Services.BrandServices;
using DatabaseMastery.TransportMongoDb.Services.HowItWorkServices;
using DatabaseMastery.TransportMongoDb.Services.OfferServices;
using DatabaseMastery.TransportMongoDb.Services.ProjectServices;
using DatabaseMastery.TransportMongoDb.Services.QuestionServices;
using DatabaseMastery.TransportMongoDb.Services.ShipmentServices;
using DatabaseMastery.TransportMongoDb.Services.SliderServices;
using DatabaseMastery.TransportMongoDb.Services.TestimonialServices;
using Microsoft.AspNetCore.Mvc;

namespace DatabaseMastery.TransportMongoDb.Controllers
{
    public class DashboardController : Controller
    {
        private readonly IShipmentService _shipmentService;
        private readonly IBrandService _brandService;
        private readonly ITestimonialService _testimonialService;
        private readonly IProjectService _projectService;
        private readonly IQuestionService _questionService;
        private readonly ISliderService _sliderService;
        private readonly IOfferService _offerService;

        public DashboardController(
            IShipmentService shipmentService,
            IBrandService brandService,
            ITestimonialService testimonialService,
            IProjectService projectService,
            IQuestionService questionService,
            ISliderService sliderService,
            IOfferService offerService)
        {
            _shipmentService = shipmentService;
            _brandService = brandService;
            _testimonialService = testimonialService;
            _projectService = projectService;
            _questionService = questionService;
            _sliderService = sliderService;
            _offerService = offerService;
        }

        public async Task<IActionResult> Index()
        {
            // Kargo istatistikleri
            ViewBag.TotalShipment = await _shipmentService.GetTotalShipmentCountAsync();
            ViewBag.DeliveredShipment = await _shipmentService.GetDeliveredShipmentCountAsync();
            ViewBag.CityCount = await _shipmentService.GetDistinctDestinationCityCountAsync();

            // İçerik istatistikleri
            ViewBag.BrandCount = await _brandService.GetBrandCountAsync();
            ViewBag.TestimonialCount = await _testimonialService.GetTestimonialCountAsync();

            // Diğer içerik sayıları (GetAll ile)
            var projects = await _projectService.GetAllProjectAsync();
            ViewBag.ProjectCount = projects.Count;

            var questions = await _questionService.GetAllQuestionAsync();
            ViewBag.QuestionCount = questions.Count;

            var sliders = await _sliderService.GetAllSliderAsync();
            ViewBag.SliderCount = sliders.Count;

            var offers = await _offerService.GetAllOfferAsync();
            ViewBag.OfferCount = offers.Count;

            // Son kargolar
            var shipments = await _shipmentService.GetAllShipmentAsync();
            ViewBag.RecentShipments = shipments.OrderByDescending(x => x.CreatedDate).Take(5).ToList();

            return View();
        }
    }
}
