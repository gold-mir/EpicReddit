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
            string username = Request.Form["userName"];
            string password = Request.Form["password"];

            if(username == "")
            {
                ViewBag.ErrorMessage = "Please enter a username";
                return View("New");
            } else if (username.Length > 30)
            {
                ViewBag.ErrorMessage = "Username must be between 1 and 30 characters.";
                return View("New");
            } else if (ERUser.Exists(username))
            {
                ViewBag.ErrorMessage = $"The username {username} is already taken.";
                return View("New");
            } else {
                ERUser newUser = ERUser.Create(username, password);
                Response.Cookies.Append("username", username);
                return Redirect("/");
            }
        }

        [HttpGet("users/loginpage")]
        public ActionResult LoginPage()
        {
            return View();
        }

        [HttpPost("users/login")]
        public ActionResult Login()
        {
            string username = Request.Form["userName"];
            string password = Request.Form["password"];

            if(username == "" || password == "")
            {
                ViewBag.ErrorMessage = "Please enter a username";
                return View("LoginPage");
            } else if (!ERUser.Exists(username))
            {
                ViewBag.ErrorMessage = "Please enter a valid username.";
                return View("LoginPage");
            } else {
                ERUser user = ERUser.Get(username);

                if(user.ValidatePassword(password))
                {
                    Response.Cookies.Append("username", username);
                    return Redirect("/");
                } else {
                    ViewBag.ErrorMessage = "Incorrect username or password.";
                    return View("LoginPage");
                }
            }
        }

        [HttpGet("users/logout")]
        public ActionResult Logout()
        {
            Response.Cookies.Delete("username");
            return Redirect("/");
        }

        [HttpGet("users/new")]
        public ActionResult New()
        {
            return View();
        }
    }
}
