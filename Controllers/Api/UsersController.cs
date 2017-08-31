﻿using System.Web.Http;
using System.Web.Http.Description;
using InAppPurchasesApi.Models;
using InAppPurchasesApi.DataAccessLayer;
using System.Data.Entity.Infrastructure;
using System.Net;

namespace InAppPurchasesApi.Controllers
{
    public class UsersController : ApiController
    {
        private IPuchasesRespository purchaseRepository;

        public UsersController()
        {
            this.purchaseRepository = new PurchasesRespository(new PurchasesIAPEntities());
        }

        // POST: api/Users
        [ResponseType(typeof(User))]
        public IHttpActionResult PostUser(User user)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (!purchaseRepository.CheckIfUserExists(user.Email))
            {
                purchaseRepository.AddUser(user);
                purchaseRepository.Save();
            }
            else
            {
                user = purchaseRepository.GetUser(user.Email, user.Password);
                if (user == null)
                    return BadRequest("Check your password!");
                return Ok(user);
            }

            return CreatedAtRoute("DefaultApi", new { id = user.UserId }, user);
        }

        //// GET: api/Users/5
        //[ResponseType(typeof(User))]
        //public IHttpActionResult GetUser(User _user)
        //{
        //    var user = purchaseRepository.GetUser(_user);
        //    if (user == null)
        //        return NotFound();

        //    return Ok(user);
        //}

        // PUT: api/Users/5
        [HttpPut]
        [Route("api/users/{Email}/{Password}/{NewPassword}")]
        [ResponseType(typeof(void))]
        public IHttpActionResult PutUser(string Email, string Password, string NewPassword)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var user = purchaseRepository.GetUser(Email, Password);

            if (user == null)
                return NotFound();

            user.Password = NewPassword;

            purchaseRepository.EditUser(user);

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

        //// DELETE: api/Users/5
        //[ResponseType(typeof(User))]
        //public IHttpActionResult DeleteUser(int id)
        //{
        //    var user = db.Users.Find(id);
        //    if (user == null)
        //        return NotFound();

        //    db.Users.Remove(user);
        //    db.SaveChanges();

        //    return Ok(user);
        //}
    }
}