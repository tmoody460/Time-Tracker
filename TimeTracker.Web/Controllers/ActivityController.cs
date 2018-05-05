using Microsoft.AspNetCore.Mvc;
using TimeTracker.Managers.Models;
using TimeTracker.Managers;
using TimeTracker.Web.Models;

namespace TimeTracker.Web.Controllers
{
    public class ActivitiesController : Controller
    {
        readonly IActivityManager _manager;
        readonly ILogManager _logManager;

        public ActivitiesController(IActivityManager manager, ILogManager logManager)
        {
            _manager = manager;
            _logManager = logManager;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Create(int? logId)
        {
            var blankModel = new ActivityModel()
            {
                LogId = logId
            };
            return View(blankModel);
        }

        [HttpPost]
        public IActionResult Create(ActivityModel model)
        {
            var savedActivity = _manager.SaveActivity(model.Activity);

            if (model.LogId != null)
            {
                _logManager.SetActivityOnLog(savedActivity.Id, model.LogId.Value);
                return RedirectToAction("Index", "Logs");
            }
            else
            {
                return RedirectToAction("Index");
            }
        }

        public IActionResult Edit(int id)
        {
            var activity = _manager.GetActivity(id);
            return View(activity);
        }

        [HttpPost]
        public IActionResult Edit(Activity model)
        {
            var savedActivity = _manager.SaveActivity(model);
            return RedirectToAction("Index", "Logs");
        }
    }
}