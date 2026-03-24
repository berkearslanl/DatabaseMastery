using DatabaseMastery.HotCoffeePostgreSql.Services.ActivityLogServices;
using DatabaseMastery.HotCoffeePostgreSql.Services.CampaignServices;
using DatabaseMastery.HotCoffeePostgreSql.Services.CategoryServices;
using DatabaseMastery.HotCoffeePostgreSql.Services.ChartServices;
using DatabaseMastery.HotCoffeePostgreSql.Services.DashboardServices;
using DatabaseMastery.HotCoffeePostgreSql.Services.ProductServices;
using DatabaseMastery.HotCoffeePostgreSql.Services.ReservationServices;
using DatabaseMastery.HotCoffeePostgreSql.Services.ReviewServices;

namespace DatabaseMastery.HotCoffeePostgreSql.Extensions
{
    public static class ServiceRegistration
    {
        public static void AddApplicationServices(this IServiceCollection services)
        {
            services.AddScoped<ICategoryService, CategoryService>();
            services.AddScoped<IProductService, ProductService>();
            services.AddScoped<IReservationService, ReservationService>();
            services.AddScoped<IDashboardService, DashboardService>();
            services.AddScoped<IActivityLogService, ActivityLogService>();
            services.AddScoped<IChartService, ChartService>();
            services.AddScoped<ICampaignService, CampaignService>();
            services.AddScoped<IReviewService, ReviewService>();
        }
    }
}
