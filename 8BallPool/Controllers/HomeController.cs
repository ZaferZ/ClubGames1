using _8BallPool.Database;
using _8BallPool.Extentions;
using _8BallPool.Models;
using _8BallPool.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Diagnostics;
using _8BallPool.ViewModels.User;
using _8BallPool.Entities;
using _8BallPool.Repository;

namespace _8BallPool.Controllers
{
    public class HomeController : Controller
    {

        public IActionResult Index()
        {
            User loggedUser = new User();
            loggedUser = HttpContext.Session.GetObject<User>("loggedUser");
            if (loggedUser != null && loggedUser.IsAdmin)
            {
                return RedirectToAction("Admin", "Home");
            }
            ScoreVM model = new ScoreVM();
            UsersRepository repo = new UsersRepository();
            model.ScoreList = repo.GetAllDecc(U => U.Points);

            return View(model);
        }
        public IActionResult Admin()
        {
            User loggedUser = HttpContext.Session.GetObject<User>("loggedUser");
            if (loggedUser == null || !loggedUser.IsAdmin)
            {
                return RedirectToAction("Index", "Home");  
            }
            AdminUserVM model = new AdminUserVM();
            UsersRepository repo = new UsersRepository();
            model.Users = repo.GetAll();
            return View(model);
        }
    }
}