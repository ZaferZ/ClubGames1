using _8BallPool.Database;
using _8BallPool.Entities;
using _8BallPool.Extentions;
using _8BallPool.Models;
using _8BallPool.ViewModels;
using _8BallPool;
using Microsoft.AspNetCore.Mvc;
using _8BallPool.ViewModels.GameReservation;
using _8BallPool.Repository;

namespace _8BallPool.Controllers
{
    public class GameReservationController : Controller
    {
        public IActionResult Index()
        {
            User loggedUser = HttpContext.Session.GetObject<User>("loggedUser");
            if (loggedUser == null || !loggedUser.IsAdmin)
            {
                return RedirectToAction("Index", "Home");
            }
            UsersRepository usersRepo = new UsersRepository();
            GameRepository gamesRepo = new GameRepository();
            GameReservationRepository reservationRepo = new GameReservationRepository();
            List<GameReseravations> games = reservationRepo.GetAll();
            ShowReservationVM model = new ShowReservationVM();
            foreach (var item in games)
            {
                model.Reservations.Add(new ReservationVM
                {
                    Id = item.Id,
                    GameId = item.GameId,
                    PlayerId = item.PlayerId,
                    PlayerName = usersRepo.GetFirstOrDefault(u => u.Id == item.PlayerId).Name,
                    GameName = gamesRepo.GetFirstOrDefault(x => x.Id == item.GameId).Name,
                    StartTime = item.StartTime,
                    EndTime = item.EndTime
                });
            }

            return View(model);
        }
        [HttpGet]
        public IActionResult EditReservations()
        {
            User loggedUser = HttpContext.Session.GetObject<User>("loggedUser");
            if (loggedUser == null || !loggedUser.IsAdmin)
            {
                return RedirectToAction("Index", "Home");
            }

            return View();
        }
        [HttpPost]
        public IActionResult EditReservations(int id)
        {
          
            EditReservationVM gameReservation = new EditReservationVM();
            UsersRepository usersRepo = new UsersRepository();
            GameRepository gamesRepo = new GameRepository();
            GameReservationRepository reservationRepo = new GameReservationRepository();
            GameReseravations item = reservationRepo.GetFirstOrDefault(u => u.PlayerId == id);
            User user = usersRepo.GetFirstOrDefault(u => u.Id == item.PlayerId);
            gameReservation.Id = item.Id;
            gameReservation.GameId = item.Id;
            gameReservation.PlayerId = item.PlayerId;
            gameReservation.PlayerName = item.Player.Name;
            gameReservation.GameName = item.Game.Name;
            gameReservation.StartTime = item.StartTime;
            gameReservation.EndTime = item.EndTime;
            gameReservation.Users = usersRepo.GetAll();
            gameReservation.Games = gamesRepo.GetAll();

            return View(gameReservation);
        }

        [HttpGet]
        public IActionResult Create()
        {
            User loggedUser = HttpContext.Session.GetObject<User>("loggedUser");
            if (loggedUser == null || !loggedUser.IsAdmin)
            {
                return RedirectToAction("Index", "Home");
            }
            CreateReservationVM model = new CreateReservationVM();
            UsersRepository usersRepo = new UsersRepository();
            GameRepository gamesRepo = new GameRepository();
            TableRepository tableRepository = new();
            model.Games = gamesRepo.GetAll();
            model.Users = usersRepo.GetAll();
            model.Tables = tableRepository.GetAll();
            return View(model);
        }
        [HttpPost]
        public IActionResult Create(CreateReservationVM item)
        {


            User loggedUser = HttpContext.Session.GetObject<User>("loggedUser");
            if (loggedUser == null || !loggedUser.IsAdmin)
            {
                return RedirectToAction("Index", "Home");
            }
            UsersRepository usersRepo = new UsersRepository();
            GameRepository gamesRepo = new GameRepository();
            GameReservationRepository reservationRepo = new GameReservationRepository();
            TableRepository tableRepo = new();
            GameReseravations newReservation = new GameReseravations
            {
                Id = item.Id,
                GameId = item.GameId,
                PlayerId = item.PlayerId,
                StartTime = item.StartTime,
                EndTime = item.EndTime,
                TablesId = item.TableId

            };
            Table table = tableRepo.GetFirstOrDefault(x => x.Id == newReservation.TablesId);
            table.IsAvailable = true;
            int timeCompare = DateTime.Compare(newReservation.EndTime, newReservation.StartTime);

            //if ( timeCompare<1 )
            //    {  
            //    ViewBag.TimeError = "Please fix the time span of the reservation. A reservation cannot be made for time less than 30 minutes and more than 4 hours";
            //    return View(new CreateReservationVM {
            //        Id = item.Id,
            //        GameId = newReservation.GameId,
            //        PlayerId = newReservation.PlayerId,
            //        StartTime = newReservation.StartTime,
            //        EndTime = newReservation.EndTime
            //      });
            //    }

            tableRepo.Save(table);
            reservationRepo.Save(newReservation);
            return RedirectToAction("Index", "Game");

        }
        [HttpGet]
        public IActionResult Edit(int id)
        {
            User loggedUser = HttpContext.Session.GetObject<User>("loggedUser");
            if (loggedUser == null || !loggedUser.IsAdmin)
            {
                return RedirectToAction("Index", "Home");
            }
            UsersRepository usersRepo = new UsersRepository();
            GameRepository gamesRepo = new GameRepository();
            TableRepository tableRepository = new();
            GameReservationRepository reservationRepo = new GameReservationRepository();
            GameReseravations gameReservation = reservationRepo.GetFirstOrDefault(x => x.Id == id);
            if (gameReservation == null)
                return RedirectToAction("Admin", "Game");
            CreateReservationVM model = new CreateReservationVM
            {
                Id = id,
                PlayerId = gameReservation.PlayerId,
                GameId = gameReservation.GameId,
                StartTime = gameReservation.StartTime,
                EndTime = gameReservation.EndTime,
                Games = gamesRepo.GetAll(),
                Users = usersRepo.GetAll(),
                Tables = tableRepository.GetAll()
            };

            return View(model);
        }
        [HttpPost]
        public IActionResult Edit(CreateReservationVM model)
        {
            User loggedUser = HttpContext.Session.GetObject<User>("loggedUser");
            if (loggedUser == null || !loggedUser.IsAdmin)
            {
                return RedirectToAction("Index", "Home");
            }

            //if (!ModelState.IsValid)
            //    return View(model);
            UsersRepository usersRepo = new UsersRepository();
            GameRepository gamesRepo = new GameRepository();
            GameReservationRepository reservationRepo = new GameReservationRepository();
            GameReseravations item = new GameReseravations
            {
                Id = model.Id,
                PlayerId = model.PlayerId,
                GameId = model.GameId,
                StartTime = model.StartTime,
                EndTime = model.EndTime,
                Player = usersRepo.GetFirstOrDefault(x => x.Id == model.PlayerId),
                Game = gamesRepo.GetFirstOrDefault(x => x.Id == model.GameId)
            };

            reservationRepo.Save(item);
            return RedirectToAction("Index", "Game");
        }
        public IActionResult Delete(int id)
        {
            User loggedUser = HttpContext.Session.GetObject<User>("loggedUser");
            if (loggedUser == null || !loggedUser.IsAdmin)
            {
                return RedirectToAction("Index", "Home");
            }
            UsersRepository usersRepo = new UsersRepository();
            GameRepository gamesRepo = new GameRepository(); 
            TableRepository tableRepo = new();
            GameReservationRepository reservationRepo = new GameReservationRepository();
            GameReseravations item = reservationRepo.GetFirstOrDefault(x => x.Id == id);
            if (item == null)
            {
                return RedirectToAction("Index", "Game");
            }
            Table table = tableRepo.GetFirstOrDefault(x => x.Id == item.TablesId);
            table.IsAvailable = false;
            tableRepo.Save(table);
            reservationRepo.Delete(item);
            return RedirectToAction("Index", "GameReservation");
        }
        public IActionResult MyReservation()
        {
            User loggedUser = HttpContext.Session.GetObject<User>("loggedUser");
            if (loggedUser == null)
            {
                return RedirectToAction("Index", "Home");
            }
            UsersRepository usersRepo = new UsersRepository();
            GameRepository gamesRepo = new GameRepository();
            GameReservationRepository reservationRepo = new GameReservationRepository();
            var playerResrv = reservationRepo.GetAll().Where(u => u.PlayerId == loggedUser.Id);
            foreach (var reservation in playerResrv)
            {
                if (reservation.EndTime < DateTime.Now)
                {
                    reservationRepo.Delete(reservation);
                }
            }
            MyReservationsVM model = new MyReservationsVM();
            foreach (var item in playerResrv)
            {
                model.GameReseravations.Add(new ReservationVM
                {
                    Id = item.Id,
                    GameId = item.GameId,
                    MyGame = gamesRepo.GetFirstOrDefault(g => g.Id == item.GameId),
                    PlayerId = item.PlayerId,
                    MyUser = usersRepo.GetFirstOrDefault(g => g.Id == item.PlayerId),
                    StartTime = item.StartTime,
                    EndTime = item.EndTime,
                });
            }
            return View(model);
        }
        [HttpGet]
        public IActionResult EndGame(int id)
        {
            User loggedUser = HttpContext.Session.GetObject<User>("loggedUser");
            if (loggedUser == null)
            {
                return RedirectToAction("Index", "Home");
            }
            GameRepository gameRepository = new GameRepository();
            GameReservationRepository reservationRepo = new GameReservationRepository();
            GameReseravations gameReservation = reservationRepo.GetFirstOrDefault(x => x.Id == id);
            UsersRepository usersRepository = new UsersRepository();
            if (gameReservation == null)
                return RedirectToAction("Index", "GameReservation");
            Game game = gameRepository.GetFirstOrDefault(x => x.Id == gameReservation.GameId);
            EndGameVM model = new EndGameVM
            {
                GameId = gameReservation.GameId,
                GameName = game.Name,
                GamePoints = game.WinnerPoints,
                ReservationId = id,
                TableId = gameReservation.TablesId,
                Users = usersRepository.GetAll()
            };

            return View(model);
        }
        [HttpPost]
        public IActionResult EndGame(EndGameVM item)
        {
            User loggedUser = HttpContext.Session.GetObject<User>("loggedUser");
            if (loggedUser == null)
            {
                return RedirectToAction("Index", "Home");
            }
            UsersRepository userRepo = new UsersRepository();
            GameReservationRepository reservationRepo = new GameReservationRepository();
            GameHistoryRepository gameHistoryRepo = new GameHistoryRepository();
            GameHistory gameHistory = new GameHistory
            {
                WinnerId = item.WinnerId,
                GameId = item.GameId,
                TableId = item.TableId
            };
            User winner = userRepo.GetFirstOrDefault(x => x.Id == item.WinnerId);
            winner.Points += item.GamePoints;
            gameHistoryRepo.Save(gameHistory);
            reservationRepo.Delete(reservationRepo.GetFirstOrDefault(x => x.Id == item.ReservationId));
            userRepo.Save(winner);
            return RedirectToAction("Index", "GameReservation");
        }
        public IActionResult ShowGameHistory()
        {
            UsersRepository userRepo = new UsersRepository();
            GameHistoryRepository historyRepository = new();
            GameRepository gameRepository = new GameRepository();
            TableRepository tableRepo = new();

            ShowGameHistoryVM model = new();
            foreach (var item in historyRepository.GetAll())
            {
                User user = userRepo.GetFirstOrDefault(x => x.Id == item.WinnerId);
                Game game = gameRepository.GetFirstOrDefault(x => x.Id == item.GameId);
                Table table = tableRepo.GetFirstOrDefault(x => x.Id == item.TableId);
                model.GameHistories.Add(new GameHistoryVM
                {
                    GamePoints = game.WinnerPoints,
                    GameName = game.Name,
                    WinnerName = user.Name,
                    Id = item.Id,
                    TableNumber = table.TableNumber,
                    EndTime = DateTime.Now
                });
            }
            return View(model);
        }
    }
}
