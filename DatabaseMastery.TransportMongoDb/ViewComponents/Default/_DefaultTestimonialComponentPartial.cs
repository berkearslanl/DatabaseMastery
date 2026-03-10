using DatabaseMastery.TransportMongoDb.Services.TestimonialServices;
using Microsoft.AspNetCore.Mvc;

namespace DatabaseMastery.TransportMongoDb.ViewComponents.Default
{
    public class _DefaultTestimonialComponentPartial:ViewComponent
    {
        private readonly ITestimonialService _testimonialService;

        public _DefaultTestimonialComponentPartial(ITestimonialService TestimonialService)
        {
            _testimonialService = TestimonialService;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var values = await _testimonialService.GetAllTestimonialAsync();
            return View(values);
        }
    }
}
