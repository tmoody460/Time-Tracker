using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using TimeTracker.Managers;
using TimeTracker.Web.Models;

namespace TimeTracker.Web.Controllers
{
    public class LogsController : Controller
    {
        readonly ILogManager _manager;

        public LogsController(ILogManager manager)
        {
            _manager = manager;
        }

        public IActionResult Index()
        {
            var log = _manager.GetCurrentLog();
            return View(log);
        }

        public IActionResult Create(bool createActivity)
        {
            var log = _manager.CreateNewCurrentLog();

            if (createActivity)
            {
                return RedirectToAction("Create", "Activities", new { logId = log.Id });
            }
            else
            {
                return RedirectToAction("Index");
            }
        }

        public IActionResult List(DateTime date)
        {
            var list = _manager.GetLogs(date.Date, date.Date.AddDays(1));
            return View(list);
        }

        public IActionResult Finalize(int logId)
        {
            _manager.Finalize(logId);
            return RedirectToAction("index");
        }
    }
}