using System.Web.Http;
using System.Web.Http.Description;
using InAppPurchasesApi.Models;
using InAppPurchasesApi.DataAccessLayer;

namespace InAppPurchasesApi.Controllers
{
    public class UsersController : ApiController
    {
        private IPuchasesRespository purchaseRepository;

        public UsersController()
        {
            this.purchaseRepository = new PurchasesRespository(new PurchasesIAPEntities());
        }

        // GET: api/Users/5
        [ResponseType(typeof(User))]
        public IHttpActionResult GetUser(User _user)
        {
            var user = purchaseRepository.GetUser(_user);
            if (user == null)
                return NotFound();

            return Ok(user);
        }

        //// PUT: api/Users/5
        //[ResponseType(typeof(void))]
        //public IHttpActionResult PutUser(User user)
        //{
        //    if (!ModelState.IsValid)
        //        return BadRequest(ModelState);

        //    user = db.Users.FirstOrDefault(u => u.Email == user.Email);

        //    if (user == null)
        //        return NotFound();

        //    db.Entry(user).State = EntityState.Modified;

        //    try
        //    {
        //        db.SaveChanges();
        //    }
        //    catch (DbUpdateConcurrencyException)
        //    {
        //        throw;
        //    }

        //    return StatusCode(HttpStatusCode.NoContent);
        //}

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

            return CreatedAtRoute("DefaultApi", new { id = user.UserId }, user);
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