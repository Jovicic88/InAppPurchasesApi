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
        private readonly IPuchasesRespository purchaseRespository;

        public LevelsController(IPuchasesRespository _purchaseRespository)
        {
            purchaseRespository = _purchaseRespository;/*new PurchasesRespository(new PurchasesIAPEntities());*/
        }

        // GET: api/Levels
        public IQueryable<Level> GetLevels()
        {
            return purchaseRespository.GetLevels();
        }

        // GET: api/Levels/5
        [ResponseType(typeof(Level))]
        public IHttpActionResult GetLevel(int id)
        {
            var level = purchaseRespository.GetLevelById(id);
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

            var _level = purchaseRespository.GetLevelById(level.LevelId);

            if (_level == null)
                return BadRequest();

            purchaseRespository.EditLevel(level);

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

        // POST: api/Levels
        [ResponseType(typeof(Level))]
        public IHttpActionResult PostLevel(Level level)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            purchaseRespository.AddLevel(level);

            try
            {
                purchaseRespository.Save();
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
            var level = purchaseRespository.GetLevelById(id);
            if (level == null)
                return NotFound();

            purchaseRespository.DeleteLevel(id);
            purchaseRespository.Save();

            return Ok(level);
        }
    }
}