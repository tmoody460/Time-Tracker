using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper.QueryableExtensions;
using TimeTracker.Accessors.Models;

namespace TimeTracker.Accessors
{
    public interface ILogAccessor
    {
        Log GetCurrentLog();
        Log SaveLog(Log currentLog);
        List<Log> GetLogsBetween(DateTime start, DateTime stop);
        Log GetLog(int logId);
    }

    public class LogAccessor : ILogAccessor
    {
        readonly ApplicationDbContext _db;

        public LogAccessor(ApplicationDbContext context)
        {
            _db = context;
        }

        public Log GetCurrentLog()
        {
            return _db.Logs.Where(l => l.IsCurrent).ProjectTo<Log>(DTOMapper.Configuration).SingleOrDefault();
        }

        public Log GetLog(int logId)
        {
            return _db.Logs.Where(l => l.Id == logId).ProjectTo<Log>(DTOMapper.Configuration).Single();
        }

        public List<Log> GetLogsBetween(DateTime start, DateTime stop)
        {
            return _db.Logs.Where(l => l.Start >= start && l.Start <= stop).ProjectTo<Log>(DTOMapper.Configuration).ToList();
        }

        public Log SaveLog(Log currentLog)
        {
            var dbLog = currentLog.MapTo<EntityFramework.Log>();

            if (dbLog.Id == 0)
            {
                _db.Logs.Add(dbLog);
            }
            else
            {
                _db.Logs.Update(dbLog);
            }

            _db.SaveChanges();
            return dbLog.MapTo<Log>();
        }
    }
}
