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
    public class GamesController : Controller
    {
        private AppDBContext db = new AppDBContext();

        private static readonly Dictionary<string, (string Description, string GifUrl)> GameMeta = new Dictionary<string, (string, string)>
        {
            { "SimonSays", ("Old school classic Simon Says", "/Content/gifs/simonsays.gif") },
            { "AimTrainer", ("Train your mouse precision", "/Content/gifs/aimtrainer.gif") },
            { "Reflex", ("Check how fast your reflexes are", "/Content/gifs/reflex.gif") },
            { "HoverPointer", ("Can you follow the target precisely?", "/Content/gifs/hoverpointer.gif") },
            { "GridExperiment", ("Check how good your photographic memory is", "/Content/gifs/gridexperiment.gif") }
        };

        // GET: Games
        public ActionResult Index()
        {
            if (!AuthHelper.IsAdmin(Session))
            {
                return new HttpStatusCodeResult(403); // Forbidden
            }
            return View(db.Games.ToList());

        }

        // GET: Games/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            if (!AuthHelper.IsAdmin(Session))
                return new HttpStatusCodeResult(403);

            Game game = db.Games.Include("Category").FirstOrDefault(g => g.gameId == id);
            if (game == null)
                return HttpNotFound();

            return View(game);
        }

        // GET: Games/Create
        public ActionResult Create()
        {
            if (!AuthHelper.IsAdmin(Session))
            {
                return new HttpStatusCodeResult(403); // Forbidden
            }
            ViewBag.CategoryId = new SelectList(db.Categories, "CategoryId", "Name");
            return View();
        }

        // POST: Games/Create
        // Aby zapewnić ochronę przed atakami polegającymi na przesyłaniu dodatkowych danych, włącz określone właściwości, z którymi chcesz utworzyć powiązania.
        // Aby uzyskać więcej szczegółów, zobacz https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "gameId,name,CategoryId")] Game game)
        {
            if (ModelState.IsValid)
            {
                db.Games.Add(game);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CategoryId = new SelectList(db.Categories, "CategoryId", "Name");
            return View(game);
        }

        // GET: Games/Edit/5
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
            Game game = db.Games.Find(id);
            if (game == null)
            {
                return HttpNotFound();
            }
            ViewBag.CategoryId = new SelectList(db.Categories, "CategoryId", "Name");
            return View(game);
        }

        // POST: Games/Edit/5
        // Aby zapewnić ochronę przed atakami polegającymi na przesyłaniu dodatkowych danych, włącz określone właściwości, z którymi chcesz utworzyć powiązania.
        // Aby uzyskać więcej szczegółów, zobacz https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "gameId,name,CategoryId")] Game game)
        {
            if (ModelState.IsValid)
            {
                db.Entry(game).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CategoryId = new SelectList(db.Categories, "CategoryId", "Name");
            return View(game);
        }

        // GET: Games/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            if (!AuthHelper.IsAdmin(Session))
                return new HttpStatusCodeResult(403);

            Game game = db.Games.Include("Category").FirstOrDefault(g => g.gameId == id);
            if (game == null)
                return HttpNotFound();

            return View(game);
        }

        // POST: Games/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Game game = db.Games.Find(id);
            db.Games.Remove(game);
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

        public ActionResult SimonSays() {
            /*if (!AuthHelper.IsLoggedIn(Session))
            {
                return new HttpStatusCodeResult(403); // Forbidden
            }*/
            return View(); }

        public ActionResult AimTrainer() {
            /*if (!AuthHelper.IsLoggedIn(Session))
            {
                return new HttpStatusCodeResult(403); // Forbidden
            }*/
            return View(); }

        public ActionResult Reflex()
        {
            /*if (!AuthHelper.IsLoggedIn(Session))
            {
                return new HttpStatusCodeResult(403); // Forbidden
            }*/
            return View();
        }

        public ActionResult HoverPointer()
        {
            /*if (!AuthHelper.IsLoggedIn(Session))
            {
                return new HttpStatusCodeResult(403); // Forbidden
            }*/
            return View();
        }

        public ActionResult GridExperiment()
        {
            /*if (!AuthHelper.IsLoggedIn(Session))
            {
                return new HttpStatusCodeResult(403); // Forbidden
            }*/
            return View();
        }

        public ActionResult Browse()
        {
            var games = db.Games.Include("Category").ToList();
            var grouped = games
                .GroupBy(g => g.Category)
                .Select(grp => new
                {
                    Category = grp.Key,
                    Games = grp.Select(g => new Helpers.ViewModelGame
                    {
                        Game = g,
                        Description = GameMeta.ContainsKey(g.name) ? GameMeta[g.name].Description : "",
                        GifUrl = GameMeta.ContainsKey(g.name) ? GameMeta[g.name].GifUrl : ""
                    }).ToList()
                }).ToList();

            return View(grouped);
        }
    }
}
