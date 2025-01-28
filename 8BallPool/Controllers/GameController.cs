using _8BallPool.Database;
using _8BallPool.Extentions;
using _8BallPool.Models;
using _8BallPool.Repository;
using _8BallPool.ViewModels.Game;
using Microsoft.AspNetCore.Hosting.Server;
using Microsoft.AspNetCore.Mvc;

namespace _8BallPool.Controllers
{
    public class GameController : Controller
    {
        public IActionResult Index()
        {
            GameRepository repo = new();
            List<Game> games = repo.GetAll();
            ShowGameVM model = new ShowGameVM();

            foreach (var game in games)
            {
                model.Games.Add(new PostGame{ 
                    Name = game.Name,
                    Description = game.Description,
                    ImagePath = game.ImagePath,
                });
            }
          
            return View(model);
        }
        public IActionResult Admin()
        {
            User loggedUser = HttpContext.Session.GetObject<User>("loggedUser");
            if (loggedUser == null || !loggedUser.IsAdmin)
            {
                return RedirectToAction("Index", "Home");
            }
            GameRepository repo = new GameRepository();
            List<Game> games = repo.GetAll();

            return View(games);
        }
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
        public IActionResult Create(CreateGameVM item)
        {
            User loggedUser = HttpContext.Session.GetObject<User>("loggedUser");
            if (loggedUser == null || !loggedUser.IsAdmin)
            {
                return RedirectToAction("Index", "Home");
            }
            Game newGame = new Game();

            GameRepository repo = new GameRepository();

            newGame.Description = item.Description;
            newGame.Name = item.Name;
            newGame.PriceForHour = item.PriceForHour;
            newGame.WinnerPoints = item.WinnerPoints;
            
            if (item.ImageFile != null && item.ImageFile.Length > 0)
            {
              
                var fileName = Path.GetFileName(item.ImageFile.FileName);
                var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "Uploads", fileName);
                newGame.ImagePath = @"\Uploads\" + item.ImageFile.FileName;
                using (var stream = new FileStream(path, FileMode.Create))
                {
                    item.ImageFile.CopyTo(stream);
                }
            }
            repo.Save(newGame);
            return RedirectToAction("Admin", "Game");

        }
        [HttpGet]
        public IActionResult Edit(int id)
        {
            User loggedUser = HttpContext.Session.GetObject<User>("loggedUser");
            if (loggedUser == null || !loggedUser.IsAdmin)
            {
                return RedirectToAction("Index", "Home");
            }
            GameRepository repo = new GameRepository();
            Game game = repo.GetFirstOrDefault(x => x.Id == id); 
            if (game == null)
                return RedirectToAction("Admin", "Game");
            EditGameVM model = new EditGameVM
            {
                Name = game.Name,
                PriceForHour = game.PriceForHour,
                ImagePath = game.ImagePath,
                Description = game.Description,
                WinnerPoints = game.WinnerPoints
            };

            return View(model);
        }
        [HttpPost]
        public IActionResult Edit(CreateGameVM model)
        {
            User loggedUser = HttpContext.Session.GetObject<User>("loggedUser");
            if (loggedUser == null || !loggedUser.IsAdmin)
            {
                return RedirectToAction("Index", "Home");
            }

            //if (!ModelState.IsValid)
            //    return View(model);
            Game item = new Game
            {
                Id = model.Id,
                Name = model.Name,
                PriceForHour = model.PriceForHour
            };
            if (model.ImageFile != null && model.ImageFile.Length > 0)
            {
                var fileName = Path.GetFileName(model.ImageFile.FileName);
                var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "Uploads", fileName);
                item.ImagePath = @"\Uploads\" + model.ImageFile.FileName;
                using (var stream = new FileStream(path, FileMode.Create))
                {
                    model.ImageFile.CopyTo(stream);
                }
            }
            item.Description = model.Description;
            item.WinnerPoints = model.WinnerPoints;
            GameRepository repo = new GameRepository();
            repo.Save(item);
            return RedirectToAction("Admin", "Game");
        }
        public IActionResult Delete(int id)
        {
            User loggedUser = HttpContext.Session.GetObject<User>("loggedUser");
            if (loggedUser == null || !loggedUser.IsAdmin)
            {
                return RedirectToAction("Index", "Home");
            }
            GameRepository repo = new GameRepository();
            Game item = repo.GetFirstOrDefault(x => x.Id == id);
            if (item == null)
            {
                return RedirectToAction("Admin", "Game");
            }
            repo.Delete(item);
            return RedirectToAction("Admin", "Game");
        }
    }
}
