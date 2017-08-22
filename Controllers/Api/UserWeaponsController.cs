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
        private IPuchasesRespository purchaseRepository;

        public UserWeaponsController()
        {
            this.purchaseRepository = new PurchasesRespository(new PurchasesIAPEntities());
        }

        // GET: api/UserWeapons/5
        [ResponseType(typeof(List<UserWeapon>))]
        public IHttpActionResult GetUserWeapons(int userId, int gameId)
        {
            var userWeapons = purchaseRepository.GetUserWeaponsToList(userId, gameId);

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

            var _weapon = purchaseRepository.GetUserWeapon(userWeapon);

            if (_weapon == null)
                return BadRequest();

            if (!string.IsNullOrEmpty(userWeapon.GunLevel.ToString()))
                _weapon.GunLevel = userWeapon.GunLevel;
            if(!string.IsNullOrEmpty(userWeapon.Unlocked.ToString()))
                _weapon.Unlocked = userWeapon.Unlocked;
            if (!string.IsNullOrEmpty(userWeapon.Experience.ToString()))
                _weapon.Experience = userWeapon.Experience;
            if (!string.IsNullOrEmpty(userWeapon.BoostActive.ToString()))
                _weapon.BoostActive = userWeapon.BoostActive;
            if (!string.IsNullOrEmpty(userWeapon.BoostEndTime))
                _weapon.BoostEndTime = userWeapon.BoostEndTime;

            purchaseRepository.EditUserWeapon(_weapon);

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

        // POST: api/UserWeapons
        [ResponseType(typeof(UserWeapon))]
        public IHttpActionResult PostUserWeapon(UserWeapon userWeapon)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var _userWeapon = purchaseRepository.GetUserWeapon(userWeapon);

            if (_userWeapon != null)
                return BadRequest("User already has this weapon!");

            if (userWeapon.BoostEndTime == null)
                userWeapon.BoostEndTime = "";

            purchaseRepository.AddUserWeapon(userWeapon);
            purchaseRepository.Save();

            return CreatedAtRoute("DefaultApi", new { id = userWeapon.UserWeaponId }, userWeapon);
        }

        // DELETE: api/UserWeapons/5
        [ResponseType(typeof(UserWeapon))]
        public IHttpActionResult DeleteUserWeapon(UserWeapon userWeapon)
        {
            userWeapon = purchaseRepository.GetUserWeapon(userWeapon);
            if (userWeapon == null)
                return NotFound();

            purchaseRepository.DeleteUserWeapon(userWeapon.UserWeaponId);
            purchaseRepository.Save();

            return Ok(userWeapon);
        }
    }
}