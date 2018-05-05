using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using TimeTracker.Web.Models;

namespace TimeTracker.Web.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return RedirectToAction("Index", "Logs");
        }
    }
}
