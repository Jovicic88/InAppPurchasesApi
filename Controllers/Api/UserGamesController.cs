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
        private readonly IPuchasesRespository purchaseRespository;

        public UserGamesController(IPuchasesRespository _purchaseRespository)
        {
            purchaseRespository = _purchaseRespository;/*new PurchasesRespository(new PurchasesIAPEntities());*/
        }

        // GET: api/UserGames/userId/gameId
        [ResponseType(typeof(UserGame))]
        [HttpGet]
        [Route("api/usergames/{UserId}/{GameId}")]
        public IHttpActionResult GetUserGame(int UserId, int GameId)
        {
            var userGame = purchaseRespository.GetUserGame(UserId, GameId);
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

            var _game = purchaseRespository.GetUserGame(userGame.UserId, userGame.GameId);

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

            purchaseRespository.EditUserGame(_game);

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

        // POST: api/UserGames
        [ResponseType(typeof(UserGame))]
        public IHttpActionResult PostUserGame(UserGame userGame)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (userGame.Achivements == null)
                userGame.Achivements = "";

            purchaseRespository.AddUserGame(userGame);
            purchaseRespository.Save();

            return CreatedAtRoute("DefaultApi", new { id = userGame.UserGameId }, userGame);
        }

        // DELETE: api/UserGames/userId/gameId
        [ResponseType(typeof(UserGame))]
        public IHttpActionResult DeleteUserGame(int userId, int gameId)
        {
            var userGame = purchaseRespository.GetUserGame(userId, gameId);
            if (userGame == null)
                return NotFound();

            purchaseRespository.DeleteUserGame(userGame.UserGameId);
            purchaseRespository.Save();

            return Ok(userGame);
        }
    }
}