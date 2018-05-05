using System.Linq;
using TimeTracker.Accessors.Models;

namespace TimeTracker.Accessors
{
    public interface IActivityAccessor
    {
        Activity SaveActivity(Activity activity);
        Activity GetActivity(int id);
    }

    public class ActivityAccessor : IActivityAccessor
    {
        readonly ApplicationDbContext _db;

        public ActivityAccessor(ApplicationDbContext context)
        {
            _db = context;
        }

        public Activity GetActivity(int id)
        {
            return _db.Activities.First(x => x.Id == id).MapTo<Activity>();
        }

        public Activity SaveActivity(Activity activity)
        {
            var dbActivity = activity.MapTo<EntityFramework.Activity>();
            if (dbActivity.Id == 0)
            {
                _db.Activities.Add(dbActivity);
            }
            else
            {
                _db.Activities.Update(dbActivity);
            }
            _db.SaveChanges();

            return dbActivity.MapTo<Activity>();
        }
    }
}