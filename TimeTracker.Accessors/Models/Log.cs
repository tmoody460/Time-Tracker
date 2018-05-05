using System;

namespace TimeTracker.Accessors.Models
{
    public class Log
    {
        public int Id { get; set; }
        public bool IsCurrent { get; set; }
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
        public int? ActivityId { get; set; }
        public Activity Activity { get; set; }
    }
}