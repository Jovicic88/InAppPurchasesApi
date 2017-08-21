using System.Net;
using System.Web.Mvc;
using InAppPurchasesApi.Models;
using InAppPurchasesApi.DataAccessLayer;

namespace InAppPurchasesApi.Controllers
{
    public class LevelsViewController : Controller
    {
        private IPuchasesRespository purchaseRepository;

        public LevelsViewController()
        {
            this.purchaseRepository = new PurchasesRespository(new PurchasesIAPEntities());
        }

        // GET: LevelsView
        public ActionResult Index()
        {
            return View(purchaseRepository.GetLevelsToList());
        }

        // GET: LevelsView/Create
        public ActionResult Create()
        {
            ViewBag.GunId = new SelectList(purchaseRepository.GetGunsToList(), "GunId", "Name");
            return View();
        }

        // POST: LevelsView/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "LevelId,GunId,GunLevel,Cost,Damage,BulletsInClip,BulletsMax")] Level level)
        {
            if (ModelState.IsValid)
            {
                purchaseRepository.AddLevel(level);
                purchaseRepository.Save();

                return RedirectToAction("Index");
            }

            ViewBag.GunId = new SelectList(purchaseRepository.GetGunsToList(), "GunId", "Name", level.GunId);
            return View(level);
        }

        // GET: LevelsView/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            var level = purchaseRepository.GetLevelById(id);
            if (level == null)
                return HttpNotFound();
            ViewBag.GunId = new SelectList(purchaseRepository.GetGunsToList(), "GunId", "Name", level.GunId);
            return View(level);
        }

        // POST: LevelsView/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "LevelId,GunId,GunLevel,Cost,Damage,BulletsInClip,BulletsMax")] Level level)
        {
            if (ModelState.IsValid)
            {
                purchaseRepository.EditLevel(level);
                purchaseRepository.Save();
                return RedirectToAction("Index");
            }
            ViewBag.GunId = new SelectList(purchaseRepository.GetGunsToList(), "GunId", "Name", level.GunId);
            return View(level);
        }

        // GET: LevelsView/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            var level = purchaseRepository.GetLevelById(id);
            if (level == null)
                return HttpNotFound();
            return View(level);
        }

        // POST: LevelsView/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            purchaseRepository.DeleteLevel(id);
            purchaseRepository.Save();
            return RedirectToAction("Index");
        }
    }
}
