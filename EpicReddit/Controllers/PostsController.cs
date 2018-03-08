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
            } else

            {
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
                Console.WriteLine(post.GetTitle());
                Console.WriteLine(post.GetUserID());
                return View(post);
            } else {
                return Redirect("/");
            }
        }

        [HttpPost("/posts/{id}/delete")]
        public ActionResult Delete(int id)
        {
            ControllersHelper.SetLoginData(Request, ViewBag);
            ERUser user = ViewBag.user;

            Post post = Post.GetByID(id);
            if(post != null && ViewBag.isLoggedIn && user.GetID() == post.GetUserID())
            {
                post.Delete();
            }

            return Redirect("/");
        }

        [HttpGet("/posts/{id}/edit")]
        public ActionResult Edit(int id)
        {
            ControllersHelper.SetLoginData(Request, ViewBag);
            ERUser user = ViewBag.user;

            Post post = Post.GetByID(id);
            if(post != null && ViewBag.isLoggedIn && user.GetID() == post.GetUserID())
            {
                return View(post);
            } else {
                return Redirect("/");
            }
        }

        [HttpPost("/posts/{id}/update")]
        public ActionResult Update(int id)
        {
            ControllersHelper.SetLoginData(Request, ViewBag);
            ERUser user = ViewBag.user;

            Post post = Post.GetByID(id);

            if(post != null && ViewBag.isLoggedIn && user.GetID() == post.GetUserID())
            {
                string newBody = Request.Form["postsBody"];
                post.Edit(newBody);
                return Redirect($"/posts/{post.GetID()}");
            } else {
                return Redirect("/");
            }
        }

        [HttpPost("/posts/{id}/comments/create")]
        public ActionResult AddComment(int id)
        {
            ControllersHelper.SetLoginData(Request, ViewBag);
            ERUser user = ViewBag.user;

            Post post = Post.GetByID(id);
            if(post != null && ViewBag.isLoggedIn)
            {
                string body = Request.Form["comment-body"];
                Comment comment = new Comment(body, user.GetID(), post.GetID());
                comment.Save();
                return Redirect($"/posts/{id}");
            } else {
                return Redirect($"/posts/{id}");
            }
        }

        [HttpPost("/posts/{postid}/comments/create/{parentid}")]
        public ActionResult AddReply(int postid, int parentid)
        {
            ControllersHelper.SetLoginData(Request, ViewBag);
            ERUser user = ViewBag.user;
            Post post = Post.GetByID(postid);
            Comment parent = Comment.GetByID(parentid);

            if(post != null && parent != null && ViewBag.isLoggedIn)
            {
                string body = Request.Form["comment-body"];
                Comment comment = new Comment(body, user.GetID(), post.GetID(), -1, parent.GetID());
                comment.Save();
                return Redirect($"/posts/{postid}");
            }
            return Redirect($"/posts/{postid}");
        }

    }
}
