using System;

namespace TimeTracker.Managers.Models
{
    public class Log
    {
        public int Id { get; set; }
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
    }

    public class LogWithActivity : Log
    {
        public Activity Activity { get; set; }
    }
}