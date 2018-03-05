using Microsoft.AspNetCore.Mvc;
using System;
using EpicReddit.Models;

namespace EpicReddit.Controllers
{
    public class HomeController : Controller
    {
        [Route("/")]
        public ActionResult Index()
        {
            return View();
        }
    }
}
