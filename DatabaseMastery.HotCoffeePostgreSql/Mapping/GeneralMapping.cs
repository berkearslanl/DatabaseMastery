using AutoMapper;
using DatabaseMastery.HotCoffeePostgreSql.Dtos.CategoryDtos;
using DatabaseMastery.HotCoffeePostgreSql.Dtos.ProductDtos;
using DatabaseMastery.HotCoffeePostgreSql.Dtos.CampaignDtos;
using DatabaseMastery.HotCoffeePostgreSql.Dtos.ReservationDtos;
using DatabaseMastery.HotCoffeePostgreSql.Entities;
using DatabaseMastery.HotCoffeePostgreSql.Dtos.ReviewDtos;

namespace DatabaseMastery.HotCoffeePostgreSql.Mapping
{
    public class GeneralMapping : Profile
    {
        public GeneralMapping() 
        {
            CreateMap<Category, ResultCategoryDto>().ReverseMap();
            CreateMap<Category, CreateCategoryDto>().ReverseMap();
            CreateMap<Category, UpdateCategoryDto>().ReverseMap();
            CreateMap<Category, GetCategoryByIdDto>().ReverseMap();

            CreateMap<Product, ResultProductDto>().ForMember(dest=>dest.CategoryName,
                opt=>opt.MapFrom(src=>src.Category.CategoryName)).ReverseMap();
            CreateMap<Product, CreateProductDto>().ReverseMap();
            CreateMap<Product, UpdateProductDto>().ReverseMap();
            CreateMap<Product, GetProductByIdDto>().ReverseMap();

            CreateMap<Reservation, ResultReservationDto>().ReverseMap();
            CreateMap<Reservation, CreateReservationDto>().ReverseMap();
            CreateMap<Reservation, UpdateReservationDto>().ReverseMap();
            CreateMap<Reservation, GetReservationByIdDto>().ReverseMap();

            CreateMap<Campaign, ResultCampaignDto>().ReverseMap();
            CreateMap<Campaign, CreateCampaignDto>().ReverseMap();
            CreateMap<Campaign, UpdateCampaignDto>().ReverseMap();
            CreateMap<Campaign, GetCampaignByIdDto>().ReverseMap();

            CreateMap<Review, ResultReviewDto>().ForMember(dest => dest.ProductName,
                opt => opt.MapFrom(src => src.Product.ProductName)).ReverseMap();
            CreateMap<Review, CreateReviewDto>().ReverseMap();
            CreateMap<Review, UpdateReviewDto>().ReverseMap();
            CreateMap<Review, GetReviewByIdDto>().ReverseMap();
        }
    }
}
