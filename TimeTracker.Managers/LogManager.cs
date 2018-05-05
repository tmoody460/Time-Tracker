using System;
using TimeTracker.Accessors;
using TimeTracker.Managers.Models;
using AutoMapper;
using System.Linq;
using System.Collections.Generic;

namespace TimeTracker.Managers
{
    public interface ILogManager
    {
        LogWithActivity CreateNewCurrentLog();
        LogWithActivity GetCurrentLog();
        List<LogWithActivity> GetLogs(DateTime start, DateTime stop);
        void SetActivityOnLog(int activityId, int logId);
        void Finalize(int logId);
    }

    public class LogManager : ILogManager
    {
        private readonly ILogAccessor _accessor;

        public LogManager(ILogAccessor accessor)
        {
            this._accessor = accessor;
        }

        public LogWithActivity GetCurrentLog()
        {
            return _accessor.GetCurrentLog().MapTo<Managers.Models.LogWithActivity>();
        }

        public LogWithActivity CreateNewCurrentLog()
        {
            var now = DateTime.Now;

            var currentLog = _accessor.GetCurrentLog();
            if (currentLog != null)
            {
                currentLog.IsCurrent = false;
                currentLog.End = now;
                _accessor.SaveLog(currentLog);
            }

            var newLog = new Accessors.Models.Log()
            {
                Start = now,
                IsCurrent = true,
            };
            return _accessor.SaveLog(newLog).MapTo<Managers.Models.LogWithActivity>();
        }

        public List<LogWithActivity> GetLogs(DateTime start, DateTime stop)
        {
            return _accessor.GetLogsBetween(start, stop).Select(l => l.MapTo<LogWithActivity>()).ToList();
        }

        public void SetActivityOnLog(int activityId, int logId)
        {
            var log = _accessor.GetLog(logId);
            log.ActivityId = activityId;
            _accessor.SaveLog(log);
        }

        public void Finalize(int logId)
        {
            var log = _accessor.GetLog(logId);
            log.End = DateTime.Now;
            log.IsCurrent = false;
            _accessor.SaveLog(log);
        }
    }
}