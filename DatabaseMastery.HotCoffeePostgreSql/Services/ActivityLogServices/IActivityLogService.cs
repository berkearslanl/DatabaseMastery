using DatabaseMastery.HotCoffeePostgreSql.Entities;

namespace DatabaseMastery.HotCoffeePostgreSql.Services.ActivityLogServices
{
    public interface IActivityLogService
    {
        void LogActivity(string title, string description, string icon, string iconColor, string iconBackground);
        List<ActivityLog> GetTodayActivities();
        List<ActivityLog> GetLatestActivities(int count);
    }
}
