using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using EpicReddit.Models;
using System;

namespace EpicReddit.Controllers
{
    public class CommentsController : Controller
    {

<<<<<<< HEAD
        // [HttpGet("/comments")]
        // public ActionResult Index()
        // {
        //     List<Comment> allComments = Comment.GetAll();
        //     return View(allComments);
        // }
=======
        [HttpGet("/comments")]
        public ActionResult Index()
        {

            List<Comment> allComments = new List<Comment>(Comment.GetAll());
            return View(allComments);
        }
>>>>>>> 611117e2079d5307184b978e073f99d564977e50

        // [HttpGet("/comments/new")]
        // public ActionResult CreateForm()
        // {
        //     return View();
        // }

        [HttpPost("/comments")]
        public ActionResult Create()
        {
          Comment newComment = new Comment(Request.Form["comment-description"]);
          newComment.Save();
          return RedirectToAction("Success", "Home");
        }
        //ONE TASK
        [HttpGet("/comments/{id}")]
        public ActionResult Details(int id)
        {
          Dictionary<string, object> model = new Dictionary<string, object>();
          Comment selectedComment = Comment.Find(id);
          List<Comment> commentPosts = selectedComment.GetPosts();
          List<Comment> allPosts = Comment.GetAll();
          model.Add("comment", selectedComment);
          model.Add("commentPosts", commentPosts);
          model.Add("allPosts", allPosts);
          return View(model);
        }

        [HttpGet("/comments/{id}/update")]
        public ActionResult UpdateForm(int id)
        {
            Comment thisComment = Comment.Find(id);
            return View(thisComment);
        }

        [HttpPost("/comments/{id}/update")]
        public ActionResult Update(int id)
        {
            Comment thisComment = Comment.Find(id);
            thisComment.Edit(Request.Form["newname"]);
            return RedirectToAction("Index");
        }
        [HttpPost("/comments/{commentId}/comments/new")]
        public ActionResult AddComment(int commentId)
        {
            Comment comment = Comment.Find(commentId);
            Comment category = Comment.Find(Int32.Parse(Request.Form["category-id"]));
            comment.AddComment(category);
            return RedirectToAction("Success", "Home");
        }
        [HttpGet("/comments{id}/delete")]
        public ActionResult DeleteOne(int id)
        {
            Comment thisComment = Comment.Find(id);
            thisComment.Delete();
            return RedirectToAction("index");
        }

        [HttpPost("/comments/delete")]
        public ActionResult DeleteAll()
        {
          Comment.DeleteAll();
          return View();
        }
    }
}
