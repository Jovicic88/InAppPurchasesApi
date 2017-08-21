using System.Net;
using System.Web.Mvc;
using InAppPurchasesApi.Models;
using InAppPurchasesApi.DataAccessLayer;

namespace InAppPurchasesApi.Controllers
{
    public class GunsViewController : Controller
    {
        private IPuchasesRespository purchaseRepository;

        public GunsViewController()
        {
            this.purchaseRepository = new PurchasesRespository(new PurchasesIAPEntities());
        }

        // GET: GunsView
        public ActionResult Index()
        {
            return View(purchaseRepository.GetGunsToList());
        }

        // GET: GunsView/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: GunsView/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "GunId,Name,NameStatic,GunType,Pause,GunLevel,BoostCost,Unlocked,Description,PathToPrefab,PathToIcon,PathToAchivementIcon,PathToAchivementIconLocked,PathToWeaponAudio")] Gun gun)
        {
            if (ModelState.IsValid)
            {
                gun.Type = (int)gun.GunType;

                purchaseRepository.AddGun(gun);
                purchaseRepository.Save();

                return RedirectToAction("Index");
            }

            return View(gun);
        }

        // GET: GunsView/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            var gun = purchaseRepository.GetGunById(id);
            if (gun == null)
                return HttpNotFound();
            return View(gun);
        }

        // POST: GunsView/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "GunId,Name,NameStatic,GunType,Pause,GunLevel,BoostCost,Unlocked,Description,PathToPrefab,PathToIcon,PathToAchivementIcon,PathToAchivementIconLocked,PathToWeaponAudio")] Gun gun)
        {
            if (ModelState.IsValid)
            {
                gun.Type = (int)gun.GunType;

                purchaseRepository.EditGun(gun);
                purchaseRepository.Save();

                return RedirectToAction("Index");
            }
            return View(gun);
        }

        // GET: GunsView/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            var gun = purchaseRepository.GetGunById(id);
            if (gun == null)
                return HttpNotFound();
            return View(gun);
        }

        // POST: GunsView/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            purchaseRepository.DeleteGun(id);
            purchaseRepository.Save();
            return RedirectToAction("Index");
        }
    }
}
