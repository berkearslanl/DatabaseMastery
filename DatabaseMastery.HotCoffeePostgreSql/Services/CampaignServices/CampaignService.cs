using AutoMapper;
using DatabaseMastery.HotCoffeePostgreSql.Context;
using DatabaseMastery.HotCoffeePostgreSql.Dtos.CampaignDtos;
using DatabaseMastery.HotCoffeePostgreSql.Entities;
using Microsoft.EntityFrameworkCore;

namespace DatabaseMastery.HotCoffeePostgreSql.Services.CampaignServices
{
    public class CampaignService : ICampaignService
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public CampaignService(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task CreateCampaignAsync(CreateCampaignDto createCampaignDto)
        {
            var value = _mapper.Map<Campaign>(createCampaignDto);
            value.StartDate = DateTime.SpecifyKind(value.StartDate, DateTimeKind.Utc);
            value.EndDate = DateTime.SpecifyKind(value.EndDate, DateTimeKind.Utc);
            await _context.Campaigns.AddAsync(value);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteCampaignAsync(int id)
        {
            var value = await _context.Campaigns.FindAsync(id);
            _context.Campaigns.Remove(value);
            await _context.SaveChangesAsync();
        }

        public async Task<List<ResultCampaignDto>> GetAllCampaignsAsync()
        {
            var values = await _context.Campaigns.ToListAsync();
            return _mapper.Map<List<ResultCampaignDto>>(values);
        }

        public async Task<GetCampaignByIdDto> GetCampaignByIdAsync(int id)
        {
            var value = await _context.Campaigns.FindAsync(id);
            return _mapper.Map<GetCampaignByIdDto>(value);
        }

        public async Task UpdateCampaignAsync(UpdateCampaignDto updateCampaignDto)
        {
            var value = _mapper.Map<Campaign>(updateCampaignDto);
            value.StartDate = DateTime.SpecifyKind(value.StartDate, DateTimeKind.Utc);
            value.EndDate = DateTime.SpecifyKind(value.EndDate, DateTimeKind.Utc);
            _context.Campaigns.Update(value);
            await _context.SaveChangesAsync();
        }

        public async Task<List<ResultCampaignDto>> GetActiveCampaignsAsync()
        {
            var today = DateTime.UtcNow.Date;
            var values = await _context.Campaigns
                .Where(x => x.Status && x.StartDate.Date <= today && x.EndDate.Date >= today)
                .OrderByDescending(x => x.DiscountPercentage)
                .ToListAsync();
            return _mapper.Map<List<ResultCampaignDto>>(values);
        }
    }
}
