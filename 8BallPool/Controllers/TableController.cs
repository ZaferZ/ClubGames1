using _8BallPool.Entities;
using _8BallPool.Extentions;
using _8BallPool.Models;
using _8BallPool.Repository;
using _8BallPool.ViewModels.Game;
using _8BallPool.ViewModels.TableVM;
using Microsoft.AspNetCore.Mvc;

namespace _8BallPool.Controllers
{
    public class TableController : Controller
    {
        public IActionResult Index()
        {
            User loggedUser = HttpContext.Session.GetObject<User>("loggedUser");
            if (loggedUser == null || !loggedUser.IsAdmin)
            {
                return RedirectToAction("Index", "Home");
            }
            TableRepository repo = new();
            List<Table> tables = repo.GetAll();
            ShowVM model = new ();
            
            foreach (var table in tables)
            {
                model.Tables.Add(new TablesVM
                {
                    Id = table.Id,
                    TableName = table.TableNumber,
                    GameName = table.Game.Name,
                    NumberOfPlayers = table.NumberPlayers,
                    PriceForHour = table.Game.PriceForHour,
                    Points = table.Game.WinnerPoints,
                    IsReserved =table.IsAvailable
                }) ;
            }

            return View(model);
        }
        [HttpGet]
        public IActionResult Create()
        {
            User loggedUser = HttpContext.Session.GetObject<User>("loggedUser");
            if (loggedUser == null || !loggedUser.IsAdmin)
            {
                return RedirectToAction("Index", "Home");
            }
            AddVm model = new();
            GameRepository gamesRepo = new GameRepository();
            model.Games = gamesRepo.GetAll();
            return View(model);
        }
        [HttpPost]
        public IActionResult Create(AddVm item)
        {
            User loggedUser = HttpContext.Session.GetObject<User>("loggedUser");
            if (loggedUser == null || !loggedUser.IsAdmin)
            {
                return RedirectToAction("Index", "Home");
            }
            TableRepository repo = new();
            Table model = new();
            model.TableNumber = item.TableNumber;
            model.NumberPlayers = item.NumberPlayers; 
            model.GameId = item.GameId;
            repo.Save(model);

            return RedirectToAction("Index");
        }
       
        public IActionResult Remove(int id)
        {
            User loggedUser = HttpContext.Session.GetObject<User>("loggedUser");
            if (loggedUser == null || !loggedUser.IsAdmin)
            {
                return RedirectToAction("Index", "Home");
            }
            TableRepository repo = new();
            Table item = repo.GetFirstOrDefault(x => x.Id == id);
            repo.Delete(item);
            return RedirectToAction("Index");
        }
        [HttpGet]
        public IActionResult Edit(int id)
        {
            User loggedUser = HttpContext.Session.GetObject<User>("loggedUser");
            if (loggedUser == null || !loggedUser.IsAdmin)
            {
                return RedirectToAction("Index", "Home");
            }
            EditVM model = new();
            GameRepository gamesRepo = new GameRepository();
            TableRepository tableRepository = new();
            Table table = tableRepository.GetFirstOrDefault(x => x.Id == id);

            model = new EditVM
            {
                Id = id,
                Games = gamesRepo.GetAll(),
                GameId = table.GameId,
                TableNumber = table.TableNumber,
                NumberPlayers = table.NumberPlayers
            };

            return View(model);

        }
        [HttpPost]
        public IActionResult Edit(EditVM item)
        {
            User loggedUser = HttpContext.Session.GetObject<User>("loggedUser");
            if (loggedUser == null || !loggedUser.IsAdmin)
            {
                return RedirectToAction("Index", "Home");
            }
            TableRepository repo = new();
            Table model = new();
            model.Id = item.Id;
            model.TableNumber = item.TableNumber;
            model.NumberPlayers = item.NumberPlayers;
            model.GameId = item.GameId;
            repo.Save(model);

            return RedirectToAction("Index");
        }
    }
}
