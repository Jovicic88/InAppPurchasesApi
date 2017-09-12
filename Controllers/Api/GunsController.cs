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
        private readonly IPuchasesRespository purchaseRespository;

        public GunsController(IPuchasesRespository _purchaseRespository)
        {
            purchaseRespository = _purchaseRespository;/*new PurchasesRespository(new PurchasesIAPEntities());*/
        }

        // GET: api/Guns/5
        [ResponseType(typeof(Gun))]
        public IHttpActionResult GetGun(int id)
        {
            var gun = purchaseRespository.GetGunById(id);
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

            if(purchaseRespository.GetGunById(gun.GunId) == null)
                return NotFound();

            purchaseRespository.EditGun(gun);

            try
            {
                purchaseRespository.Save();
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

            purchaseRespository.AddGun(gun);

            try
            {
                purchaseRespository.Save();
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
            var gun = purchaseRespository.GetGunById(id);
            if (gun == null)
                return NotFound();

            purchaseRespository.DeleteGun(id);
            purchaseRespository.Save();

            return Ok(gun);
        }
    }
}