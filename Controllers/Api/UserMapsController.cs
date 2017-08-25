using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Net;
using System.Web.Http;
using System.Web.Http.Description;
using InAppPurchasesApi.Models;
using InAppPurchasesApi.DataAccessLayer;

namespace InAppPurchasesApi.Controllers.Api
{
    public class UserMapsController : ApiController
    {
        private IPuchasesRespository purchaseRepository;

        public UserMapsController()
        {
            this.purchaseRepository = new PurchasesRespository(new PurchasesIAPEntities());
        }

        // GET: api/UserMapss/5/11
        [ResponseType(typeof(List<UserMap>))]
        [HttpGet]
        [Route("api/UserMaps/{UserId}/{GameId}")]
        public IHttpActionResult GetUserMaps(int UserId, int GameId)
        {
            var userMaps = purchaseRepository.GetUserMapsToList(UserId, GameId);

            if (userMaps == null)
                return NotFound();

            return Ok(userMaps);
        }

        // PUT: api/UserMaps/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutUserMap(UserMap userMap)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var _map = purchaseRepository.GetUserMap(userMap);

            if (_map == null)
                return BadRequest();

            _map.Unlocked = userMap.Unlocked;

            if (!string.IsNullOrEmpty(userMap.Name))
                _map.Name = userMap.Name;
 
            purchaseRepository.EditUserMap(_map);

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

        // POST: api/UserMaps
        [ResponseType(typeof(UserMap))]
        public IHttpActionResult PostUserMap(UserMap userMap)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var _userMap = purchaseRepository.GetUserMap(userMap);

            if (_userMap != null)
                return BadRequest("User already has this map!");

            purchaseRepository.AddUserMap(userMap);
            purchaseRepository.Save();

            return CreatedAtRoute("DefaultApi", new { id = userMap.MapId }, userMap);
        }

        // DELETE: api/UserMaps/5
        [ResponseType(typeof(UserMap))]
        public IHttpActionResult DeleteUserMap(UserMap userMap)
        {
            userMap = purchaseRepository.GetUserMap(userMap);
            if (userMap == null)
                return NotFound();

            purchaseRepository.DeleteUserMap(userMap.UserMapId);
            purchaseRepository.Save();

            return Ok(userMap);
        }
    }
}