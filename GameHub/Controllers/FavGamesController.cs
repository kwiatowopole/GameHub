using GameHub.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GameHub.Controllers
{
    public class FavGamesController : Controller
    {
        private AppDBContext db = new AppDBContext();

        // GET: FavGames
        public ActionResult Index()
        {
            if (Session["userId"] == null)
                return RedirectToAction("Login", "Users");

            int userId = (int)Session["userId"];
            var favGames = db.FavGames
                .Where(f => f.UserId == userId)
                .Select(f => f.Game)
                .ToList();

            return View(favGames);
        }

        // POST: FavGames/Add/5
        [HttpPost]
        public ActionResult Add(int gameId)
        {
            if (Session["userId"] == null)
                return Json(new { success = false, message = "Not logged in" });

            int userId = (int)Session["userId"];
            bool alreadyFav = db.FavGames.Any(f => f.UserId == userId && f.GameId == gameId);
            if (!alreadyFav)
            {
                db.FavGames.Add(new FavGame { UserId = userId, GameId = gameId });
                db.SaveChanges();
            }
            return Json(new { success = true });
        }
        [HttpPost]
        public ActionResult Remove(int gameId)
        {
            if (Session["userId"] == null)
                return Json(new { success = false, message = "Not logged in" });

            int userId = (int)Session["userId"];
            var fav = db.FavGames.FirstOrDefault(f => f.UserId == userId && f.GameId == gameId);
            if (fav != null)
            {
                db.FavGames.Remove(fav);
                db.SaveChanges();
                return Json(new { success = true });
            }
            return Json(new { success = false, message = "Not in favorites" });
        }
    }
}