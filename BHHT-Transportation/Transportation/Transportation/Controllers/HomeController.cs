using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Drawing;
using Transportation.Models;
using Transportation.Repositories;
using Transportation.HelpingClasses;

namespace Transportation.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IUserRepo userRepo;
        private readonly GeneralPurpose gp;


        public HomeController(ILogger<HomeController> logger , IUserRepo userRepo,GeneralPurpose _generalPurpose)
        {
            _logger = logger;
            this.userRepo = userRepo;   
            this.gp = _generalPurpose;
        }

        public async Task<IActionResult> Index()
        {
            User? loggedinUser = gp.GetUserClaims();
            if (loggedinUser == null)
            {
                return RedirectToAction("Login", "Auth", new { msg = "session expire", color = "red" });

            }

            var transportersCount = await userRepo.GetActiveTransporterRecordList();
            var Count= transportersCount.Where(x=>x.DriverId!=null &&x.DriverId==loggedinUser.Id).ToList().Count();
            ViewBag.TransportationCount = Count;
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}