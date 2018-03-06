using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using EpicReddit.Models;
using EpicReddit.Controllers;

namespace EpicReddit.Tests
{
[TestClass]
public class PostsControllerTest
{
  [TestMethod]

  public void Index_HasCorrectModelType_CountList()
  {
      //Arrange
      PostsController controller = new PostsController();
      IActionResult actionResult = controller.Index();
      ViewResult indexView = controller.Index() as ViewResult;

      //Act
      var result = indexView.ViewData.Model;

      //Assert
      Assert.IsInstanceOfType(result, typeof(List<Post>));
  }
}
}












// {
//     [TestClass]
//     public class PostsControllerTest
//     {
//       [TestMethod]
//           public void Index_ReturnsCorrectView_True()
//           {
//               //Arrange
//               PostsController controller = new PostsController();
//
//               //Act
//               ActionResult indexView = controller.Index();
//               result = indexView;
//               //Assert
//               Assert.IsInstanceOfType(result, typeof(ViewResult));
//           }
//      }
// }
