using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Net;
using System.Web.Http;
using System.Web.Http.Description;
using InAppPurchasesApi.Models;
using InAppPurchasesApi.DataAccessLayer;

namespace InAppPurchasesApi.Controllers
{
    public class UserWeaponsController : ApiController
    {
        private readonly IPuchasesRespository purchaseRespository;

        public UserWeaponsController(IPuchasesRespository _purchaseRespository)
        {
            purchaseRespository = _purchaseRespository;/*new PurchasesRespository(new PurchasesIAPEntities());*/
        }

        // GET: api/UserWeapons/5/11
        [ResponseType(typeof(List<UserWeapon>))]
        [HttpGet]
        [Route("api/UserWeapons/{UserId}/{GameId}")]
        public IHttpActionResult GetUserWeapons(int UserId, int GameId)
        {
            var userWeapons = purchaseRespository.GetUserWeaponsToList(UserId, GameId);

            if (userWeapons == null)
                return NotFound();

            return Ok(userWeapons);
        }

        // PUT: api/UserWeapons/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutUserWeapon(UserWeapon userWeapon)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var _weapon = purchaseRespository.GetUserWeapon(userWeapon);

            if (_weapon == null)
                return BadRequest();

            if (userWeapon.GunLevel != null)
                _weapon.GunLevel = userWeapon.GunLevel;
            if (userWeapon.Unlocked != null)
                _weapon.Unlocked = userWeapon.Unlocked;
            if (!string.IsNullOrEmpty(userWeapon.Experience.ToString()))
                _weapon.Experience = userWeapon.Experience;
            if (userWeapon.BoostActive != null)
                _weapon.BoostActive = userWeapon.BoostActive;
            if (!string.IsNullOrEmpty(userWeapon.BoostEndTime))
                _weapon.BoostEndTime = userWeapon.BoostEndTime;

            purchaseRespository.EditUserWeapon(_weapon);

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

        // POST: api/UserWeapons
        [ResponseType(typeof(UserWeapon))]
        public IHttpActionResult PostUserWeapon(UserWeapon userWeapon)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var _userWeapon = purchaseRespository.GetUserWeapon(userWeapon);

            if (_userWeapon != null)
                return BadRequest("User already has this weapon!");

            if (userWeapon.BoostEndTime == null)
                userWeapon.BoostEndTime = "";

            purchaseRespository.AddUserWeapon(userWeapon);
            purchaseRespository.Save();

            return CreatedAtRoute("DefaultApi", new { id = userWeapon.UserWeaponId }, userWeapon);
        }

        // DELETE: api/UserWeapons/5
        [ResponseType(typeof(UserWeapon))]
        public IHttpActionResult DeleteUserWeapon(UserWeapon userWeapon)
        {
            userWeapon = purchaseRespository.GetUserWeapon(userWeapon);
            if (userWeapon == null)
                return NotFound();

            purchaseRespository.DeleteUserWeapon(userWeapon.UserWeaponId);
            purchaseRespository.Save();

            return Ok(userWeapon);
        }
    }
}