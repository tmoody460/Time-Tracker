using System;
using TimeTracker.Accessors;
using TimeTracker.Managers.Models;

namespace TimeTracker.Managers
{
    public interface IActivityManager
    {
        Activity SaveActivity(Activity activity);
        Activity GetActivity(int id);
    }

    public class ActivityManager : IActivityManager
    {
        readonly IActivityAccessor _accessor;

        public ActivityManager(IActivityAccessor accessor)
        {
            _accessor = accessor;
        }

        public Activity GetActivity(int id)
        {
            return _accessor.GetActivity(id).MapTo<Managers.Models.Activity>();
        }

        public Activity SaveActivity(Activity activity)
        {
            var mappedActivity = activity.MapTo<Accessors.Models.Activity>();
            return _accessor.SaveActivity(mappedActivity).MapTo<Managers.Models.Activity>();
        }
    }
}
