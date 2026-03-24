using DatabaseMastery.HotCoffeePostgreSql.Dtos.CampaignDtos;
using DatabaseMastery.HotCoffeePostgreSql.Services.ActivityLogServices;
using DatabaseMastery.HotCoffeePostgreSql.Services.CampaignServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DatabaseMastery.HotCoffeePostgreSql.Controllers
{
    [Authorize]
    public class CampaignController : Controller
    {
        private readonly ICampaignService _campaignService;
        private readonly IActivityLogService _activityLogService;

        public CampaignController(ICampaignService campaignService, IActivityLogService activityLogService)
        {
            _campaignService = campaignService;
            _activityLogService = activityLogService;
        }

        public async Task<IActionResult> CampaignList()
        {
            var values = await _campaignService.GetAllCampaignsAsync();
            return View(values);
        }

        [HttpGet]
        public IActionResult CreateCampaign()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateCampaign(CreateCampaignDto createCampaignDto)
        {
            await _campaignService.CreateCampaignAsync(createCampaignDto);
            _activityLogService.LogActivity(
                "Yeni kampanya eklendi",
                $"{createCampaignDto.Title} — %{createCampaignDto.DiscountPercentage} indirim",
                "bi bi-megaphone-fill",
                "var(--accent-dark)",
                "var(--accent-soft)"
            );
            return RedirectToAction("CampaignList");
        }

        [HttpGet]
        public async Task<IActionResult> UpdateCampaign(int id)
        {
            var values = await _campaignService.GetCampaignByIdAsync(id);
            return View(values);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateCampaign(UpdateCampaignDto updateCampaignDto)
        {
            await _campaignService.UpdateCampaignAsync(updateCampaignDto);
            _activityLogService.LogActivity(
                "Kampanya güncellendi",
                $"{updateCampaignDto.Title} kampanyası düzenlendi",
                "bi bi-megaphone-fill",
                "var(--blue)",
                "var(--blue-soft)"
            );
            return RedirectToAction("CampaignList");
        }

        public async Task<IActionResult> DeleteCampaign(int id)
        {
            var campaign = await _campaignService.GetCampaignByIdAsync(id);
            await _campaignService.DeleteCampaignAsync(id);
            _activityLogService.LogActivity(
                "Kampanya silindi",
                $"{campaign.Title} kampanyası kaldırıldı",
                "bi bi-megaphone-fill",
                "var(--red)",
                "var(--red-soft)"
            );
            return RedirectToAction("CampaignList");
        }
    }
}
