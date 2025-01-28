using _8BallPool.Database;
using _8BallPool.Extentions;
using _8BallPool.Models;
using _8BallPool.Repository;
using _8BallPool.ViewModels;
using _8BallPool.ViewModels.User;
using Microsoft.AspNetCore.Mvc;

namespace _8BallPool.Controllers
{
    public class UserController : Controller
    {
        [HttpGet]
        public IActionResult Create()
        {
            User loggedUser = HttpContext.Session.GetObject<User>("loggedUser");
            if (loggedUser == null || !loggedUser.IsAdmin)
            {
                return RedirectToAction("Index", "Home");
            }
            return View();
        }
        [HttpPost]
        public IActionResult Create(CreateUserVM model)
        {
            User loggedUser = HttpContext.Session.GetObject<User>("loggedUser");
            if (loggedUser == null || !loggedUser.IsAdmin)
            {
                return RedirectToAction("Index", "Home");
            }
            User newUser = new User();
            UsersRepository repo = new UsersRepository();
            newUser.UserName = model.UserName;
            newUser.Password = model.Password;
            newUser.Name = model.Name;
            newUser.Email = model.Email;
            newUser.IsAdmin = model.IsAdmin;
            repo.Save(newUser);
            return RedirectToAction("Index", "Home");
        }
        [HttpGet]
        public IActionResult Edit(int id)
        {
            User loggedUser = HttpContext.Session.GetObject<User>("loggedUser");
            if (loggedUser == null || !loggedUser.IsAdmin)
            {
                return RedirectToAction("Index", "Home");
            }
            UsersRepository repo = new UsersRepository();
            User user = repo.GetFirstOrDefault(u => u.Id == id);
            if (user == null)
                return RedirectToAction("Index", "Home");
            EditUserVM model = new EditUserVM
            {
                Id = user.Id,
                UserName = user.UserName,
                Password = user.Password,
                Name = user.Name,
                Email = user.Email,
                IsAdmin = user.IsAdmin,
                Points = user.Points
            };
            return View(model);
        }
       
        [HttpPost]
        public IActionResult Edit(EditUserVM model)
        {
            User loggedUser = HttpContext.Session.GetObject<User>("loggedUser");
            if (loggedUser == null || !loggedUser.IsAdmin)
            {
                return RedirectToAction("Index", "Home");
            }
            if (!ModelState.IsValid)
                return View(model);

            User item = new User
            {
                Id = model.Id,
                UserName = model.UserName,
                Password = model.Password,
                Name = model.Name,
                Email = model.Email,
                IsAdmin = model.IsAdmin,
                Points = model.Points
            };
            UsersRepository repo = new UsersRepository();
            repo.Save(item);
            return RedirectToAction("Admin", "Home");

        }
        public IActionResult Delete(int id)
        {
            User loggedUser = HttpContext.Session.GetObject<User>("loggedUser");
            if (loggedUser == null || !loggedUser.IsAdmin)
            {
                return RedirectToAction("Index", "Home");
            }
            UsersRepository repo = new UsersRepository();
            User item = repo.GetFirstOrDefault(x => x.Id == id);
            if (item == null)
                return RedirectToAction("Admin", "Home");


            repo.Delete(item);
            return RedirectToAction("Admin", "Home");
        }
    }
}
