using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using EpicReddit.Models;
using System;

namespace EpicReddit.Controllers
{
    public class CommentsController : Controller
    {

        [HttpPost("/posts/{postid}/comments/new")]
        public ActionResult Create(int postid)
        {
            Comment thisComment = Comment.GetByID(postid);
            thisComment.Edit(Request.Form["newcomment"]);
            return RedirectToAction("{postid}");
        }

        [HttpPost("/posts/{postid}/comments/{commentid}/new")]
        public ActionResult CreateReply(int postid, int parentCommentID)
        {
            Comment thisComment = Comment.GetByID(postid);
            thisComment.Edit(Request.Form["newchildcomment"]);
            return RedirectToAction("{postid}");
        }

        [HttpPost("/comments/{commentid}/edit")]
        public ActionResult Update(int id)
        {
            Comment thisComment = Comment.GetByID(id);
            return View(thisComment);
        }

        [HttpPost("/comments/{commentid}/delete")]
        public ActionResult Delete(int id)
        {
            Comment thisComment = Comment.GetByID(id);
            thisComment.Delete();
            return RedirectToAction("{postid}");
        }
    }
}
