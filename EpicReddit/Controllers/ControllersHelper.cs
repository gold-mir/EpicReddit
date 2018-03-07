using EpicReddit.Models;
namespace EpicReddit.Controllers
{
    public class ControllersHelper
    {
        public static void SetLoginData(Microsoft.AspNetCore.Http.HttpRequest request, dynamic viewbag)
        {
            viewbag.showLoginInfo = true;
            string username = request.Cookies["username"];
            if(ERUser.Exists(username))
            {
                viewbag.user = ERUser.Get(username);
                viewbag.isLoggedIn = true;
            } else {
                viewbag.user = null;
                viewbag.isLoggedIn = false;
            }
        }
    }
}
