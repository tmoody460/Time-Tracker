using System;
using TimeTracker.Managers.Models;

namespace TimeTracker.Web.Models
{
    public class ActivityModel
    {
        public Activity Activity { get; set; }
        public int? LogId { get; set; }
    }
}