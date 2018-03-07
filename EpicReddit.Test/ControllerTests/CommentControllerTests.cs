// using System;
// using System.Collections.Generic;
// using Microsoft.AspNetCore.Mvc;
// using Microsoft.VisualStudio.TestTools.UnitTesting;
// using EpicReddit.Models;
// using EpicReddit.Controllers;
//
// namespace EpicReddit.Tests
// {
//     [TestClass]
//     public class CommentsControllerTest
//     {
//
//         [TestMethod]
//         public void Create_HasCorrectModelType_CountList()
//         {
//             //Arrange
//             CommentsController controller = new CommentsController();
//             IActionResult actionResult = controller.Create();
//             ViewResult indexView = controller.Create() as ViewResult;
//
//             //Act
//             var result = indexView.ViewData.Model;
//
//             //Assert
//             Assert.IsInstanceOfType(result, typeof(Comment[]));
//         }
//     }
// }
