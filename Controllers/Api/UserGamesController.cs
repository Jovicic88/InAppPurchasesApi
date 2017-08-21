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
    public class UserGamesController : ApiController
    {
        private IPuchasesRespository purchaseRepository;

        public UserGamesController()
        {
            this.purchaseRepository = new PurchasesRespository(new PurchasesIAPEntities());
        }

        // GET: api/UserGames/userId/gameId
        [ResponseType(typeof(UserGame))]
        public IHttpActionResult GetUserGame(int userId, int gameId)
        {
            var userGame = purchaseRepository.GetUserGame(userId, gameId);
            if (userGame == null)
                return NotFound();

            return Ok(userGame);
        }

        // PUT: api/UserGames/
        [ResponseType(typeof(void))]
        public IHttpActionResult PutUserGame(UserGame userGame)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var _game = purchaseRepository.GetUserGame(userGame.UserId, userGame.GameId);

            if (_game == null)
                return BadRequest();

            _game.Coins = userGame.Coins;
            _game.Achivements = userGame.Achivements;
            _game.Diamonds = userGame.Diamonds;
            _game.Premium = userGame.Premium;

            purchaseRepository.EditUserGame(_game);

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

        // POST: api/UserGames
        [ResponseType(typeof(UserGame))]
        public IHttpActionResult PostUserGame(UserGame userGame)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            purchaseRepository.AddUserGame(userGame);
            purchaseRepository.Save();

            return CreatedAtRoute("DefaultApi", new { id = userGame.UserGameId }, userGame);
        }

        // DELETE: api/UserGames/userId/gameId
        [ResponseType(typeof(UserGame))]
        public IHttpActionResult DeleteUserGame(int userId, int gameId)
        {
            var userGame = purchaseRepository.GetUserGame(userId, gameId);
            if (userGame == null)
                return NotFound();

            purchaseRepository.DeleteUserGame(userGame.UserGameId);
            purchaseRepository.Save();

            return Ok(userGame);
        }
    }
}