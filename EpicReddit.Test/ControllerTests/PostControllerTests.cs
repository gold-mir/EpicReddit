<<<<<<< HEAD
// using Microsoft.VisualStudio.TestTools.UnitTesting;
// using Microsoft.AspNetCore.Mvc;
// using System.Collections.Generic;
// using EpicReddit.Controllers;
// using EpicReddit.Models;
//
// namespace EpicReddit.Tests
// {
//     [TestClass]
//     public class HomeControllerTest
//     {
//       // [TestMethod]
//       //     public void Index_ReturnsCorrectView_True()
//       //     {
//       //         //Arrange
//       //         PostsController controller = new PostsController();
//       //
//       //         //Act
//       //         ActionResult indexView = controller.Index();
//       //         int result = indexView;
//       //         //Assert
//       //         Assert.IsInstanceOfType(result, typeof(ViewResult));
//       //     }
=======
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
>>>>>>> Controllers
//      }
// }
