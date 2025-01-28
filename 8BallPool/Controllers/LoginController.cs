using _8BallPool.Database;
using _8BallPool.Extentions;
using _8BallPool.Models;
using _8BallPool.Repository;
using _8BallPool.ViewModels;
using _8BallPool.ViewModels.User;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace _8BallPool.Controllers
{
    public class LoginController : Controller
    {
        [HttpGet]
        public IActionResult Index()
        {

            return View();
        }
        [HttpPost]
        public IActionResult Index(LoginVM model)
        {
            if (!this.ModelState.IsValid)
                return View(model);
            UsersRepository repo = new UsersRepository();
            User loggedUser = repo.GetFirstOrDefault(u => u.UserName == model.UserName
                                                    && u.Password == model.Password);
            if (loggedUser == null)
            {
                return View(model);
            }
            HttpContext.Session.SetObject("loggedUser", loggedUser);

            return RedirectToAction("Index", "Home");
        }
        [HttpGet]
       
        public IActionResult Logout()
        {
            HttpContext.Session.Remove("loggedUser");
            return RedirectToAction("Index", "Home");
        }
    }
}
