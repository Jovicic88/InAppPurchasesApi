using System.Net;
using System.Web.Mvc;
using InAppPurchasesApi.Models;
using InAppPurchasesApi.DataAccessLayer;

namespace InAppPurchasesApi.Controllers
{
    public class GamesViewController : Controller
    {
        private IPuchasesRespository purchaseRespository;

        public GamesViewController()
        {
            this.purchaseRespository = new PurchasesRespository(new PurchasesIAPEntities());
        }

        // GET: GamesView
        public ActionResult Index()
        {
            return View(purchaseRespository.GetGames());
        }

        // GET: GamesView/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: GamesView/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "GameId,Version,Name")] Game game)
        {
            if (ModelState.IsValid)
            {
                purchaseRespository.AddGame(game);
                purchaseRespository.Save();
                return RedirectToAction("Index");
            }

            return View(game);
        }

        // GET: GamesView/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            var game = purchaseRespository.GetGameById(id);
            if (game == null)
                return HttpNotFound();
            return View(game);
        }

        // POST: GamesView/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "GameId,Version,Name")] Game game)
        {
            if (ModelState.IsValid)
            {
                purchaseRespository.EditGame(game);
                purchaseRespository.Save();
                return RedirectToAction("Index");
            }
            return View(game);
        }

        // GET: GamesView/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            var game = purchaseRespository.GetGameById(id);
            if (game == null)
                return HttpNotFound();
            return View(game);
        }

        // POST: GamesView/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            purchaseRespository.DeleteGame(id);
            purchaseRespository.Save();
            return RedirectToAction("Index");
        }
    }
}