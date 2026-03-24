using DatabaseMastery.HotCoffeePostgreSql.Dtos.CampaignDtos;

namespace DatabaseMastery.HotCoffeePostgreSql.Services.CampaignServices
{
    public interface ICampaignService
    {
        Task<List<ResultCampaignDto>> GetAllCampaignsAsync();
        Task<GetCampaignByIdDto> GetCampaignByIdAsync(int id);
        Task CreateCampaignAsync(CreateCampaignDto createCampaignDto);
        Task UpdateCampaignAsync(UpdateCampaignDto updateCampaignDto);
        Task DeleteCampaignAsync(int id);
        Task<List<ResultCampaignDto>> GetActiveCampaignsAsync();
    }
}
