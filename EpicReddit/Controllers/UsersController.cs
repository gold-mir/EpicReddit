using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using EpicReddit.Models;
using System;

namespace EpicReddit.Controllers
{
    public class UsersController : Controller
    {
        [HttpPost("users/create")]
        public ActionResult Create()
        {
            return null;
        }

        [HttpPost("users/login")]
        public ActionResult Login()
        {
            return null;
        }

        [HttpGet("users/logout")]
        public ActionResult Logout()
        {
            return View();
        }
    }
}
