using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using GameHub.Models;
using GameHub.Helpers;

namespace GameHub.Controllers
{
    public class ScoresController : Controller
    {
        private AppDBContext db = new AppDBContext();

        // GET: Scores
        public ActionResult Index()
        {
            if (!AuthHelper.IsAdmin(Session))
            {
                return new HttpStatusCodeResult(403); // Forbidden
            }
            var scores = db.Scores.Include(s => s.game).Include(s => s.user);
            return View(scores.ToList());
        }

        // GET: Scores/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            if (!AuthHelper.IsAdmin(Session))
            {
                return new HttpStatusCodeResult(403); // Forbidden
            }
            Score score = db.Scores.Find(id);
            if (score == null)
            {
                return HttpNotFound();
            }
            return View(score);
        }

        // GET: Scores/Create
        public ActionResult Create()
        {
            if (!AuthHelper.IsAdmin(Session))
            {
                return new HttpStatusCodeResult(403); // Forbidden
            }
            ViewBag.gameId = new SelectList(db.Games, "gameId", "name");
            ViewBag.userId = new SelectList(db.Users, "userId", "username");
            return View();
        }

        // POST: Scores/Create
        // Aby zapewnić ochronę przed atakami polegającymi na przesyłaniu dodatkowych danych, włącz określone właściwości, z którymi chcesz utworzyć powiązania.
        // Aby uzyskać więcej szczegółów, zobacz https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "scoreId,points,date,userId,gameId")] Score score)
        {
            if (ModelState.IsValid)
            {
                db.Scores.Add(score);
                db.SaveChanges();
                return RedirectToAction("Index");
            }


            ViewBag.gameId = new SelectList(db.Games, "gameId", "name", score.gameId);
            ViewBag.userId = new SelectList(db.Users, "userId", "username", score.userId);
            return View(score);
        }

        // GET: Scores/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            if (!AuthHelper.IsAdmin(Session))
            {
                return new HttpStatusCodeResult(403); // Forbidden
            }
            Score score = db.Scores.Find(id);
            if (score == null)
            {
                return HttpNotFound();
            }
            ViewBag.gameId = new SelectList(db.Games, "gameId", "name", score.gameId);
            ViewBag.userId = new SelectList(db.Users, "userId", "username", score.userId);
            return View(score);
        }

        // POST: Scores/Edit/5
        // Aby zapewnić ochronę przed atakami polegającymi na przesyłaniu dodatkowych danych, włącz określone właściwości, z którymi chcesz utworzyć powiązania.
        // Aby uzyskać więcej szczegółów, zobacz https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "scoreId,points,date,userId,gameId")] Score score)
        {
            if (ModelState.IsValid)
            {
                db.Entry(score).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.gameId = new SelectList(db.Games, "gameId", "name", score.gameId);
            ViewBag.userId = new SelectList(db.Users, "userId", "username", score.userId);
            return View(score);
        }

        // GET: Scores/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            if (!AuthHelper.IsAdmin(Session))
            {
                return new HttpStatusCodeResult(403); // Forbidden
            }
            Score score = db.Scores.Find(id);
            if (score == null)
            {
                return HttpNotFound();
            }
            return View(score);
        }

        // POST: Scores/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Score score = db.Scores.Find(id);
            db.Scores.Remove(score);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        public ActionResult RankingPartial(string game)
        {
            if (string.IsNullOrEmpty(game))
                return HttpNotFound("Nie podano gry.");
            var scores = db.Scores
                .Include(s=>s.user)
                .Where(s => s.game.name == game)
                .GroupBy(s => s.userId)
                .Select(g => g.OrderByDescending(s => s.points).FirstOrDefault())
                .OrderByDescending(s => s.points)
                .Take(20)
                .ToList();

            return PartialView("RankingPartial", scores);
        }

        public ActionResult Results(string game = null)
        {

            ViewBag.Games = db.Games.ToList();

            var scores = db.Scores.Include(s => s.user).Include(s => s.game);

            if (!string.IsNullOrEmpty(game))
                scores = scores.Where(s => s.game.name == game);

            var resultList = scores.OrderByDescending(s => s.points).ToList();

            return View(resultList);
        }
        [HttpPost]
        public JsonResult SaveScore(int points, string gameName)
        {
            using (var db = new AppDBContext())
            {
                var userId = (int)Session["userId"];

                var game = db.Games.FirstOrDefault(g => g.name == gameName);
                if (game == null)
                {
                    return Json(new { success = false, message = "Gra nie istnieje" });
                }

                var score = new Score
                {
                    userId = userId,
                    gameId = game.gameId,
                    points = points,
                    date = DateTime.Now
                };

                db.Scores.Add(score);
                db.SaveChanges();
            }

            return Json(new { success = true });
        }
    }
}
