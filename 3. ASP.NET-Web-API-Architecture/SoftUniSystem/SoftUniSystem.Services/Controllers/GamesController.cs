namespace SoftUniSystem.Services.Controllers
{
    using System;
    using System.Linq;
    using System.Web.Http;

    using Microsoft.AspNet.Identity;

    using SoftUniSystem.Data;
    using SoftUniSystem.Models;

    [Authorize]
    public class GamesController : BaseApiConroller
    {
        public GamesController(ISoftUniSystemData data) : base(data)
        {
        }

        [HttpPost]
        [ActionName("create")]
        public IHttpActionResult CreateGame()
        {
            var game = new Game
            {
                UserOneId = this.User.Identity.GetUserId()
            };

            this.Data.Games.Add(game);
            this.Data.SaveChanges();

            return this.Ok(game.Id);
        }

        [HttpPost]
        [Route("join")]
        public IHttpActionResult JoinGame(string id)
        {
            var guidId = new Guid(id);
            var game = this.Data.Games.All().FirstOrDefault(x => x.Id == guidId);
            if (game == null)
            {
                return this.NotFound();
            }

            game.UserTwoId = this.User.Identity.GetUserId();
            game.State = GameState.TurnOne;
            this.Data.SaveChanges();

            return this.Ok(game.Id);
        }

//        public IHttpActionResult GetGameInfo(string gameId)
//        {
//            var gameGuidId = new Guid(gameId);
//            var game = this.Data.Games
//                .All()
//                .Where(x => x.Id == gameGuidId)
//                .Select(x => new
//                {
//                    x.Id,
//                    x.Number,
//                    x.State,
//                    x.UserOneId,
//                    x.UserTwoId
//                })
//                .FirstOrDefault();
//            if (game == null)
//            {
//                return this.NotFound();
//            }
//
//            var userId = this.User.Identity.GetUserId();
//            if (userId != game.UserOneId && userId != game.UserTwoId)
//            {
//                return this.BadRequest("You have no rights for this game");
//            }
//        }

        public IHttpActionResult Play()
        {
            throw new NotImplementedException();
        }
    }
}