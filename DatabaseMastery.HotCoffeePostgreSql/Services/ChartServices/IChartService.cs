using DatabaseMastery.HotCoffeePostgreSql.Dtos.ChartDtos;

namespace DatabaseMastery.HotCoffeePostgreSql.Services.ChartServices
{
    public interface IChartService
    {
        Task<List<ReservationChartDto>> GetLast7DaysReservationCountAsync();
        Task<List<CategoryProductCountChartDto>> GetCategoryProductCountAsync();
        Task<List<CategoryAvgPriceChartDto>> GetCategoryAvgPriceAsync();
    }
}
