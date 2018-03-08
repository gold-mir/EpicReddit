using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using System;
using EpicReddit.Models;

namespace EpicReddit.Controllers
{
    public class PostsController : Controller
    {

        [HttpGet("/")]
        public ActionResult Index()
        {
            List<Post> allPosts = new List<Post> (Post.GetAll());

            ControllersHelper.SetLoginData(Request, ViewBag);

            return View(allPosts);
        }

        [HttpGet("/posts/new")]
        public ActionResult New()
        {
            ControllersHelper.SetLoginData(Request, ViewBag);

            return View();
        }

        [HttpPost("/")]
        public ActionResult Create()
        {
            ControllersHelper.SetLoginData(Request, ViewBag);

            string title = Request.Form["postsTitle"];
            string body = Request.Form["postsBody"];

            if(ViewBag.isLoggedIn)
            {
                Post newPost = new Post(title, body, ViewBag.user.GetID());
                newPost.Save();
                return Redirect($"/posts/{newPost.GetID()}");
            } else {
                return Redirect("/");
            }
        }

        [HttpGet("/posts/{id}")]
        public ActionResult Details(int id)
        {
            ControllersHelper.SetLoginData(Request, ViewBag);
            Post post = Post.GetByID(id);
            if(post != null)
            {
                return View(post);
            } else {
                return Redirect("/");
            }
        }

    }
}
