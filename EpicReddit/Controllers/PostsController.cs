using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using System;
using EpicReddit.Models;

namespace EpicReddit.Controllers
{
    public class PostsController : Controller
    {

        [HttpGet("/posts")]
        public ActionResult Index()
        {
            List<Post> allPosts = new List<Post>(Post.GetAll());
            return View(allPosts);
        }

        [HttpGet("/posts/new")]
        public ActionResult CreateForm()
        {
          return View();
        }

        [HttpPost("/posts")]
        public ActionResult Create()
        {
          Post newPost = new Post(Request.Form["post-name"]);
          newPost.Save();
          return RedirectToAction("Home");
        }

        [HttpGet("/posts/{id}")]
        public ActionResult PostDetail(int id)
        {
            Dictionary<string, object> model = new Dictionary<string, object>();
            Post selectedPost = Post.Find(id);
            List<Comment> postComments = selectedPost.GetComments();
            List<Comment> allComments = Comment.GetAll();
            model.Add("post", selectedPost);
            model.Add("postComments", postComments);
            model.Add("allComments", allComments);

            return View(model);
        }
        [HttpPost("/posts/{postId}/comments/new")]
        public ActionResult AddComment(int postId)
        {
            Post post = Post.Find(postId);
            Comment comment = Comment.Find(Int32.Parse(Request.Form["comment-id"]));
            post.AddComment(comment);
            return RedirectToAction("Home");
        }
        [HttpGet("/posts/{postId}/update")]
        public ActionResult UpdateForm(int postId)
        {
          Post thisPost = Post.Find(postId);
          return View("update", thisPost);
        }

        [HttpPost("/posts{postId}/update")]
        public ActionResult Update(int postId)
        {
          Post thisPost = Post.Find(postId);
          thisPost.Edit(Request.Form["newname"]);
          return RedirectToAction("Index");
        }

        [HttpGet("/posts/{postId}/delete")]
        public ActionResult DeleteOne(int postId)
        {
          Post thisPost = Post.Find(postId);
          thisPost.Delete();
          return RedirectToAction("index");
        }

        [HttpPost("/posts/delete")]
        public ActionResult DeleteAll()
        {
          Post.DeleteAll();
          return View();
        }
    }
}
