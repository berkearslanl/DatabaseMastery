using DatabaseMastery.HotCoffeePostgreSql.Dtos.CategoryDtos;

namespace DatabaseMastery.HotCoffeePostgreSql.Services.CategoryServices
{
    public interface ICategoryService
    {
        Task<List<ResultCategoryDto>> GetAllCategoriesAsync();
        Task<GetCategoryByIdDto> GetCategoryByIdAsync(int id);
        Task CreateCategoryAsync(CreateCategoryDto createCategoryDto);
        Task UpdateCategoryAsync(UpdateCategoryDto updateCategoryDto);
        Task DeleteCategoryAsync(int id);
    }
}
