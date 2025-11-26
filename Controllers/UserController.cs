using Microsoft.AspNetCore.Mvc;

namespace BlogApp.Controllers
{
    public class UserController : Controller
    {
        public UserController()
        {
            
        }
        public IActionResult Login()
        {
            return View();
        }
    }
}
