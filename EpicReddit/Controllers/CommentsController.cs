<<<<<<< HEAD
// using System.Collections.Generic;
// using Microsoft.AspNetCore.Mvc;
// using EpicReddit.Models;
// using System;
//
// namespace EpicReddit.Controllers
// {
//     public class CommentsController : Controller
//     {
//         [HttpPost("/comments")]
//         public ActionResult Create()
//         {
//           Comment newComment = new Comment(Request.Form["comment-description"],Request.Form["comment-description"],(Int32.Parse(Request.Form["comment-description"])), Int32.Parse(Request.Form["comment-description"]));
//           newComment.Save();
//           return RedirectToAction("Success", "Home");
//         }
//         //ONE TASK
//         [HttpGet("/comments/{id}")]
//         public ActionResult Details(int id)
//         {
//           Dictionary<string, object> model = new Dictionary<string, object>();
//           Comment selectedComment = Comment.GetByID(id);
//           // Comment[] commentPosts = selectedComment.GetParentPost();
//           Comment[] allPosts = Comment.GetAll();
//           model.Add("comment", selectedComment);
//           // model.Add("commentPosts", commentPosts);
//           model.Add("allPosts", allPosts);
//           return View(model);
//         }
//
//         [HttpGet("/comments/{id}/update")]
//         public ActionResult UpdateForm(int id)
//         {
//             Comment thisComment = Comment.GetByID(id);
//             return View(thisComment);
//         }
//
//         [HttpPost("/comments/{id}/update")]
//         public ActionResult Update(int id)
//         {
//             Comment thisComment = Comment.GetByID(id);
//             thisComment.Edit(Request.Form["newname"]);
//             return RedirectToAction("Index");
//         }
//
//         [HttpPost("/comments/{commentId}/comments/new")]
//           public ActionResult AddComment(int commentId)
//           {
//               Comment comment = Comment.GetByID(commentId);
//               Comment.GetByID(Int32.Parse(Request.Form["comment-id"]));
//               // Comment.GetParentComment(Request.Form["comment-description"]);
//               return RedirectToAction("Success", "Home");
//           }
//
//         [HttpGet("/comments{id}/delete")]
//         public ActionResult DeleteOne(int id)
//         {
//             Comment thisComment = Comment.GetByID(id);
//             thisComment.Delete();
//             return RedirectToAction("index");
//         }
//
//         [HttpPost("/comments/delete")]
//         public ActionResult DeleteAll()
//         {
//           Comment.DeleteAll();
//           return View();
//         }
//     }
// }
=======
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using EpicReddit.Models;
using System;

namespace EpicReddit.Controllers
{
    public class CommentsController : Controller
    {
        [HttpPost("/comments")]
        public ActionResult Create()
        {
          Comment newComment = new Comment(Request.Form["comment-description"],Request.Form["comment-description"],(Int32.Parse(Request.Form["comment-description"])));
          newComment.Save();
          return RedirectToAction("Home");
        }
        //ONE TASK
        [HttpGet("/comments/{id}")]
        public ActionResult Details(int id)
        {
          Dictionary<string, object> model = new Dictionary<string, object>();
          Comment selectedComment = Comment.GetByID(id);
          // Comment[] commentPosts = selectedComment.GetParentPost();
          Comment[] allPosts = Comment.GetAll();
          model.Add("comment", selectedComment);
          // model.Add("commentPosts", commentPosts);
          model.Add("allPosts", allPosts);
          return View(model);
        }

        [HttpGet("/comments/{id}/update")]
        public ActionResult UpdateForm(int id)
        {
            Comment thisComment = Comment.GetByID(id);
            return View(thisComment);
        }

        [HttpPost("/comments/{id}/update")]
        public ActionResult Update(int id)
        {
            Comment thisComment = Comment.GetByID(id);
            thisComment.Edit(Request.Form["newname"]);
            return RedirectToAction("Index");
        }

        [HttpPost("/comments/{commentId}/comments/new")]
          public ActionResult AddComment(int commentId)
          {
              Comment comment = Comment.GetByID(commentId);
              Comment.GetByID(Int32.Parse(Request.Form["comment-id"]));
              // Comment.GetParentComment(Request.Form["comment-description"]);
              return RedirectToAction("Success", "Home");
          }

        [HttpGet("/comments{id}/delete")]
        public ActionResult DeleteOne(int id)
        {
            Comment thisComment = Comment.GetByID(id);
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
>>>>>>> Controllers
