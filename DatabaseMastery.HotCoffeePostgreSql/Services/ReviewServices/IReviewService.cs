using DatabaseMastery.HotCoffeePostgreSql.Dtos.ReviewDtos;

namespace DatabaseMastery.HotCoffeePostgreSql.Services.ReviewServices
{
    public interface IReviewService
    {
        Task<List<ResultReviewDto>> GetAllReviewsAsync();
        Task<GetReviewByIdDto> GetReviewByIdAsync(int id);
        Task CreateReviewAsync(CreateReviewDto createReviewDto);
        Task UpdateReviewAsync(UpdateReviewDto updateReviewDto);
        Task DeleteReviewAsync(int id);
    }
}
