using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Web.Http;
using System.Web.Http.Description;
using InAppPurchasesApi.Models;
using InAppPurchasesApi.DataAccessLayer;

namespace InAppPurchasesApi.Controllers
{
    public class LevelsController : ApiController
    {
        private IPuchasesRespository purchaseRepository;

        public LevelsController()
        {
            this.purchaseRepository = new PurchasesRespository(new PurchasesIAPEntities());
        }

        // GET: api/Levels
        public IQueryable<Level> GetLevels()
        {
            return purchaseRepository.GetLevels();
        }

        // GET: api/Levels/5
        [ResponseType(typeof(Level))]
        public IHttpActionResult GetLevel(int id)
        {
            var level = purchaseRepository.GetLevelById(id);
            if (level == null)
                return NotFound();

            return Ok(level);
        }

        // PUT: api/Levels/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutLevel(Level level)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var _level = purchaseRepository.GetLevelById(level.LevelId);

            if (_level == null)
                return BadRequest();

            purchaseRepository.EditLevel(level);

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

        // POST: api/Levels
        [ResponseType(typeof(Level))]
        public IHttpActionResult PostLevel(Level level)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            purchaseRepository.AddLevel(level);

            try
            {
                purchaseRepository.Save();
            }
            catch (DbUpdateException)
            {
                throw;
            }

            return CreatedAtRoute("DefaultApi", new { id = level.LevelId }, level);
        }

        // DELETE: api/Levels/5
        [ResponseType(typeof(Level))]
        public IHttpActionResult DeleteLevel(int id)
        {
            var level = purchaseRepository.GetLevelById(id);
            if (level == null)
                return NotFound();

            purchaseRepository.DeleteLevel(id);
            purchaseRepository.Save();

            return Ok(level);
        }
    }
}