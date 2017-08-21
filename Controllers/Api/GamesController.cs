using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Web.Http;
using System.Web.Http.Description;
using InAppPurchasesApi.Models;
using InAppPurchasesApi.DataAccessLayer;

namespace InAppPurchasesApi.Controllers
{
    public class GamesController : ApiController
    {
        private IPuchasesRespository purchaseRespository;

        public GamesController()
        {
            this.purchaseRespository = new PurchasesRespository(new PurchasesIAPEntities());
        }

        // GET: api/Games/5
        [ResponseType(typeof(Game))]
        public IHttpActionResult GetGameVersionById(int id)
        {
            var game = purchaseRespository.GetGameById(id);
            if (game == null)
            {
                return NotFound();
            }

            return Ok(game.GameId);
        }
    }
}