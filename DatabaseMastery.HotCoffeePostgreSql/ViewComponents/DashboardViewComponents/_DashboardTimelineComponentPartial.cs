using DatabaseMastery.HotCoffeePostgreSql.Services.ActivityLogServices;
using Microsoft.AspNetCore.Mvc;

namespace DatabaseMastery.HotCoffeePostgreSql.ViewComponents.DashboardViewComponents
{
    public class _DashboardTimelineComponentPartial : ViewComponent
    {
        private readonly IActivityLogService _activityLogService;
        public _DashboardTimelineComponentPartial(IActivityLogService activityLogService)
        {
            _activityLogService = activityLogService;
        }

        public IViewComponentResult Invoke()
        {
            var activities = _activityLogService.GetTodayActivities();
            return View(activities);
        }
    }
}
