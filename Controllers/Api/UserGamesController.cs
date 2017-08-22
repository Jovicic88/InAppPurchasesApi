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
        [HttpGet]
        [Route("api/usergames/{UserId}/{GameId}")]
        public IHttpActionResult GetUserGame(int UserId, int GameId)
        {
            var userGame = purchaseRepository.GetUserGame(UserId, GameId);
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

            _game.HasGame = userGame.HasGame;

            if (userGame.Coins != null)
                _game.Coins = userGame.Coins;
            if (userGame.Diamonds != null)
                _game.Diamonds = userGame.Diamonds;
            if (userGame.Premium != null)
                _game.Premium = userGame.Premium;

            if (!string.IsNullOrEmpty(userGame.Achivements))
                _game.Achivements = userGame.Achivements;

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

            if (userGame.Achivements == null)
                userGame.Achivements = "";

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