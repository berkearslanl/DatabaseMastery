using AutoMapper;
using DatabaseMastery.HotCoffeePostgreSql.Context;
using DatabaseMastery.HotCoffeePostgreSql.Dtos.CategoryDtos;
using DatabaseMastery.HotCoffeePostgreSql.Entities;
using Microsoft.EntityFrameworkCore;

namespace DatabaseMastery.HotCoffeePostgreSql.Services.CategoryServices
{
    public class CategoryService : ICategoryService
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public CategoryService(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task CreateCategoryAsync(CreateCategoryDto createCategoryDto)
        {
            var value = _mapper.Map<Category>(createCategoryDto);

            await _context.Categories.AddAsync(value);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteCategoryAsync(int id)
        {
            var value = await _context.Categories.FindAsync(id);
            _context.Categories.Remove(value);
            await _context.SaveChangesAsync();
        }

        public async Task<List<ResultCategoryDto>> GetAllCategoriesAsync()
        {
            var value = await _context.Categories.ToListAsync();
            return _mapper.Map<List<ResultCategoryDto>>(value);
        }

        public async Task<GetCategoryByIdDto> GetCategoryByIdAsync(int id)
        {
            var value = await _context.Categories.FindAsync(id);
            return _mapper.Map<GetCategoryByIdDto>(value);
        }

        public async Task UpdateCategoryAsync(UpdateCategoryDto updateCategoryDto)
        {
            var value = _mapper.Map<Category>(updateCategoryDto);
            _context.Categories.Update(value);
            await _context.SaveChangesAsync();
        }
    }
}
