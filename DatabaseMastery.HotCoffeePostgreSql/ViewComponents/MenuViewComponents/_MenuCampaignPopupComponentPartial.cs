using DatabaseMastery.HotCoffeePostgreSql.Services.CampaignServices;
using Microsoft.AspNetCore.Mvc;

namespace DatabaseMastery.HotCoffeePostgreSql.ViewComponents.MenuViewComponents
{
    public class _MenuCampaignPopupComponentPartial : ViewComponent
    {
        private readonly ICampaignService _campaignService;

        public _MenuCampaignPopupComponentPartial(ICampaignService campaignService)
        {
            _campaignService = campaignService;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var values = await _campaignService.GetActiveCampaignsAsync();
            return View(values);
        }
    }
}
