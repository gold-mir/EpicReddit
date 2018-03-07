using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using System;
using EpicReddit.Models;

namespace EpicReddit.Controllers
{
    public class PostsController : Controller
    {

        // [HttpGet("/posts")]
        // public ActionResult Index()
        // {
        //     List<Post> allPosts = new List<Post> (Post.GetAll());
        //     return View(allPosts);
        // }
        //
        // [HttpGet("/posts/new")]
        // public ActionResult CreateForm()
        // {
        //   return View();
        // }
        //
        // [HttpPost("/posts")]
        // public ActionResult Create()
        // {
        //   Post newPost = new Post(Request.Form["postsTitle"], Request.Form["postsBody"], Request.Form["posterName"]);
        //   newPost.Save();
        //   return RedirectToAction("Home");
        // }
        //
        // [HttpGet("/posts/{id}")]
        // public ActionResult PostDetail(int id)
        // {
        //     Dictionary<string, object> model = new Dictionary<string, object>();
        //     Post selectedPost = Post.GetByID(id);
        //     Comment[] postComments = selectedPost.GetComments();
        //     Comment[] allComments = Comment.GetAll();
        //     model.Add("post", selectedPost);
        //     model.Add("postComments", postComments);
        //     model.Add("allComments", allComments);
        //
        //     return View(model);
        // }
        // [HttpPost("/posts/{postId}/comments/new")]
        // public ActionResult AddComment(int postId)
        // {
        //     Post post = Post.GetByID(postId);
        //      Comment comment = Comment.GetByID(Int32.Parse(Request.Form["comment-id"]));
        //     // Post.GetTitle(comment);
        //     return RedirectToAction("Home");
        // }
        // [HttpGet("/posts/{postId}/update")]
        // public ActionResult UpdateForm(int postId)
        // {
        //   Post thisPost = Post.GetByID(postId);
        //   return View("update", thisPost);
        // }
        //
        // [HttpPost("/posts{postId}/update")]
        // public ActionResult Update(int postId)
        // {
        //   Post thisPost = Post.GetByID(postId);
        //   thisPost.Edit(Request.Form["newname"]);
        //   return RedirectToAction("Index");
        // }
        //
        // [HttpGet("/posts/{postId}/delete")]
        // public ActionResult DeleteOne(int postId)
        // {
        //   Post thisPost = Post.GetByID(postId);
        //   thisPost.Delete();
        //   return RedirectToAction("index");
        // }
        //
        // [HttpPost("/posts/delete")]
        // public ActionResult DeleteAll()
        // {
        //   Post.DeleteAll();
        //   return View();
        // }

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
            //   string postsTitle = Request.Form["postsTitle"];
            //   string postsBody = Request.Form["postsBody"];
            //   Post newPost = new Post(postsTitle, postsBody);
            //   newPost.Save();
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

        // [HttpGet("/")]













        // [HttpPost("/posts")]
        // public ActionResult Create()
        // {
        // string postsTitle = Request.Form["postsTitle"];
        // string postsBody = Request.Form["postsBody"];
        // Post newPost = new Post(postsTitle, postsBody);
        // newPost.Save();
        // return RedirectToAction("/");
        // }

        // [HttpGet("/posts/{id}")]
        // public ActionResult PostDetails(int id)
        // {
        //   Dictionary<string, object> model = new Dictionary<string, object>();
        //   Post selectedPost = Post.GetByID(id);
        //   Comment[] postComments = selectedPost.GetComments();
        //   Comment[] allComments = Comment.GetAll();
        //   model.Add("post", selectedPost);
        //   model.Add("postComments", postComments);
        //   model.Add("allComments", allComments);
        //   return View(model);
        // }
    }
}
