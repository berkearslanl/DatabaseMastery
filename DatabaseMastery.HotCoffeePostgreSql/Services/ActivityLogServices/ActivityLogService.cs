using DatabaseMastery.HotCoffeePostgreSql.Context;
using DatabaseMastery.HotCoffeePostgreSql.Entities;

namespace DatabaseMastery.HotCoffeePostgreSql.Services.ActivityLogServices
{
    public class ActivityLogService : IActivityLogService
    {
        private readonly AppDbContext _context;

        public ActivityLogService(AppDbContext context)
        {
            _context = context;
        }

        public List<ActivityLog> GetTodayActivities()
        {
            return _context.ActivityLogs
                .Where(x => x.CreatedAt.Date == DateTime.UtcNow.Date)
                .OrderByDescending(x => x.CreatedAt)
                .Take(15)
                .ToList();
        }

        public List<ActivityLog> GetLatestActivities(int count)
        {
            return _context.ActivityLogs
                .OrderByDescending(x => x.CreatedAt)
                .Take(count)
                .ToList();
        }

        public void LogActivity(string title, string description, string icon, string iconColor, string iconBackground)
        {
            var log = new ActivityLog
            {
                Title = title,
                Description = description,
                Icon = icon,
                IconColor = iconColor,
                IconBackground = iconBackground,
                CreatedAt = DateTime.UtcNow
            };
            _context.ActivityLogs.Add(log);
            _context.SaveChanges();
        }
    }
}
