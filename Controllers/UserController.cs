using BlogApp.Data.Abstract;
using BlogApp.Entity;
using BlogApp.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace BlogApp.Controllers
{
    public class UserController : Controller
    {
        private IUserRepository _userRepository;
        public UserController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        [HttpGet]
        public IActionResult Login()
        {
            if (User.Identity!.IsAuthenticated)
            {
                RedirectToAction("Index", "Posts");
            }
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if(ModelState.IsValid)
            {
                var isUser = _userRepository.Users.FirstOrDefault(x=> x.Email == model.Email && x.Password == model.Password);
                if (isUser != null)
                {
                    List<Claim> userClaims = new List<Claim>();
                    userClaims.Add(new Claim(ClaimTypes.NameIdentifier,isUser.UserId.ToString()));
                    userClaims.Add(new Claim(ClaimTypes.Name,isUser.UserName ?? ""));

                    if(isUser.Email == "eyupturkey641@gmail.com")
                    {
                        userClaims.Add(new Claim(ClaimTypes.Role, "admin"));
                    }
                    ClaimsIdentity claimsIdentity = new ClaimsIdentity(userClaims, CookieAuthenticationDefaults.AuthenticationScheme);
                    AuthenticationProperties authProperties = new AuthenticationProperties
                    {
                        IsPersistent = true
                    };
                    await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
                    await HttpContext.SignInAsync(
                        CookieAuthenticationDefaults.AuthenticationScheme,
                        new ClaimsPrincipal(claimsIdentity),
                        authProperties
                    );
                    return RedirectToAction("Index", "Posts");
                }
                else
                {
                    ModelState.AddModelError("", "Kullanıcı adı veya şifre yanlış.");
                }
            }
            return View(model);
        }
        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login");
        }
    }
}
