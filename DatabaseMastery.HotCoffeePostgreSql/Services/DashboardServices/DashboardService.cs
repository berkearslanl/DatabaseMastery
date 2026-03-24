using AutoMapper;
using DatabaseMastery.HotCoffeePostgreSql.Context;
using DatabaseMastery.HotCoffeePostgreSql.Dtos.ReservationDtos;
using Microsoft.EntityFrameworkCore;

namespace DatabaseMastery.HotCoffeePostgreSql.Services.DashboardServices
{
    public class DashboardService : IDashboardService
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public DashboardService(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<int> GetTotalReservationCountAsync()
        {
            return await _context.Reservations.CountAsync();
        }

        public async Task<int> GetPendingReservationCountAsync()
        {
            return await _context.Reservations
                .CountAsync(x => x.Status == "Beklemede");
        }

        public async Task<int> GetApprovedReservationCountAsync()
        {
            return await _context.Reservations
                .CountAsync(x => x.Status == "Onaylandı");
        }

        public async Task<int> GetCancelledReservationCountAsync()
        {
            return await _context.Reservations
                .CountAsync(x => x.Status == "İptal Edildi");
        }

        public async Task<int> GetTodayReservationCountAsync()
        {
            return await _context.Reservations
                .CountAsync(x => x.ReservationDate.Date == DateTime.Today);
        }

        public async Task<int> GetTotalCustomerCountAsync()
        {
            return await _context.Reservations.SumAsync(x => x.GuestCount);
        }

        public async Task<int> GetTotalMenuProductCountAsync()
        {
            return await _context.Products.CountAsync();
        }

        public async Task<int> GetTodayOrderCountAsync()
        {
            return await _context.Products.CountAsync();
        }

        public async Task<List<ResultReservationDto>> GetTodayReservationListAsync()
        {
            var today = DateTime.UtcNow.Date;
            var values = await _context.Reservations
                .Where(x => x.ReservationDate == today)
                .OrderBy(x => x.ReservationTime)
                .ToListAsync();

            return _mapper.Map<List<ResultReservationDto>>(values);
        }
    }
}
