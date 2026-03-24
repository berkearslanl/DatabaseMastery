using AutoMapper;
using DatabaseMastery.HotCoffeePostgreSql.Context;
using DatabaseMastery.HotCoffeePostgreSql.Dtos.ReviewDtos;
using DatabaseMastery.HotCoffeePostgreSql.Entities;
using Microsoft.EntityFrameworkCore;

namespace DatabaseMastery.HotCoffeePostgreSql.Services.ReviewServices
{
    public class ReviewService:IReviewService
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public ReviewService(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task CreateReviewAsync(CreateReviewDto createReviewDto)
        {
            var value = _mapper.Map<Review>(createReviewDto);

            await _context.Reviews.AddAsync(value);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteReviewAsync(int id)
        {
            var value = await _context.Reviews.FindAsync(id);
            _context.Reviews.Remove(value);
            await _context.SaveChangesAsync();
        }

        public async Task<List<ResultReviewDto>> GetAllReviewsAsync()
        {
            var value = await _context.Reviews.Include(x => x.Product).ToListAsync();
            return _mapper.Map<List<ResultReviewDto>>(value);
        }

        public async Task<GetReviewByIdDto> GetReviewByIdAsync(int id)
        {
            var value = await _context.Reviews.FindAsync(id);
            return _mapper.Map<GetReviewByIdDto>(value);
        }

        public async Task UpdateReviewAsync(UpdateReviewDto updateReviewDto)
        {
            var value = _mapper.Map<Review>(updateReviewDto);
            _context.Reviews.Update(value);
            await _context.SaveChangesAsync();
        }
    }
}
