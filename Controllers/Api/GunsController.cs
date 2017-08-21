using System.Data.Entity.Infrastructure;
using System.Net;
using System.Web.Http;
using System.Web.Http.Description;
using InAppPurchasesApi.Models;
using InAppPurchasesApi.DataAccessLayer;

namespace InAppPurchasesApi.Controllers
{
    public class GunsController : ApiController
    {
        private IPuchasesRespository purchaseRepository;

        public GunsController()
        {
            this.purchaseRepository = new PurchasesRespository(new PurchasesIAPEntities());
        }

        // GET: api/Guns/5
        [ResponseType(typeof(Gun))]
        public IHttpActionResult GetGun(int id)
        {
            var gun = purchaseRepository.GetGunById(id);
            if (gun == null)
                return NotFound();

            return Ok(gun);
        }

        // PUT: api/Guns/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutGun(Gun gun)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if(purchaseRepository.GetGunById(gun.GunId) == null)
                return NotFound();

            purchaseRepository.EditGun(gun);

            try
            {
                purchaseRepository.Save();
            }
            catch (DbUpdateConcurrencyException)
            {
                throw;
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/Guns
        [ResponseType(typeof(Gun))]
        public IHttpActionResult PostGun(Gun gun)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            purchaseRepository.AddGun(gun);

            try
            {
                purchaseRepository.Save();
            }
            catch (DbUpdateException)
            {
                throw;
            }

            return CreatedAtRoute("DefaultApi", new { id = gun.GunId }, gun);
        }

        // DELETE: api/Guns/5
        [ResponseType(typeof(Gun))]
        public IHttpActionResult DeleteGun(int id)
        {
            var gun = purchaseRepository.GetGunById(id);
            if (gun == null)
                return NotFound();

            purchaseRepository.DeleteGun(id);
            purchaseRepository.Save();

            return Ok(gun);
        }
    }
}