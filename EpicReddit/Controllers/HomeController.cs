using Microsoft.AspNetCore.Mvc;
using System;
using EpicReddit.Models;

namespace EpicReddit.Controllers
{
  public class HomeController : Controller
  {

    [HttpGet("/")]
    public ActionResult Index()
    {
      List<Posts> allPosts = Posts.GetAll();
      return View(allPosts);
    }
  }
}
