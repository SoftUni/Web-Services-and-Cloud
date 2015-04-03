namespace BattleShips.Services.Controllers
{
    using System;
    using System.Linq;
    using System.Text;
    using System.Web.Http;

    using BattleShips.Data;
    using BattleShips.Models;
    using BattleShips.Service.Controllers;
    using BattleShips.Service.Models;

    using Microsoft.AspNet.Identity;

    [Authorize]
    public class GamesController : BaseApiController
    {
        public IHttpActionResult GetGamesCount()
        {
            var gamesCount = this.Data.Games.All().Count();
            return this.Ok(gamesCount);
        }

        [HttpPost]
        [ActionName("create")]
        public IHttpActionResult CreateGame()
        {
            var userId = this.User.Identity.GetUserId();
            var game = new Game 
            {
                PlayerOneId = userId,
                // TODO: Generate ships
            };

            this.Data.Games.Add(game);
            this.Data.SaveChanges();

            return this.Ok(game.Id);
        }

        [HttpGet]
        [ActionName("getGames")]
        public IHttpActionResult GetAllAvailableGames()
        {
            var games = this.Data.Games
                .All()
                .Where(x => x.State == GameState.WaitingForPlayer)
                .Select(x => new
                {
                    Id = x.Id,
                    PlayerOne = x.PlayerOne.UserName,
                    State = x.State.ToString(),
                })
                .ToList();

            return this.Ok(games);
        }

        [HttpPost]
        [ActionName("join")]
        public IHttpActionResult JoinGame(JoinGameBindingModel model)
        {
            var guidGameId = new Guid(model.GameId);
            var game = this.Data.Games
                .All()
                .Where(x => x.Id == guidGameId)
                .FirstOrDefault();
            if (game == null)
            {
                return this.NotFound();
            }

            var userId = this.User.Identity.GetUserId();
            if (game.PlayerOneId == userId)
            {
                return this.BadRequest("You can not join in your game!");
            }

            game.PlayerTwoId = userId;
            game.State = GameState.TurnOne;

            this.Data.SaveChanges();

            return this.Ok(game.Id);
        }

        [HttpPost]
        [ActionName("play")]
        public IHttpActionResult PlayTurn(PlayTurnBindingModel model)
        {
            if (model == null)
            {
                this.ModelState.AddModelError("model", "The model is empty");
            }

            if (!ModelState.IsValid)
            {
                return this.BadRequest(this.ModelState);
            }

            var guidGameId = new Guid(model.GameId);
            var game = this.Data.Games
                .All()
                .Where(x => x.Id == guidGameId)
                .FirstOrDefault();
            if (game == null)
            {
                return this.NotFound();
            }

            var userId = this.User.Identity.GetUserId();
            if (game.PlayerOneId != userId && game.PlayerTwoId != userId)
            {
                return this.BadRequest("You can't make turn in this game!");
            }

            if (game.PlayerOneId == userId && game.State == GameState.TurnTwo ||
                game.PlayerTwoId == userId && game.State == GameState.TurnOne)
            {
                return this.BadRequest("It's not your turn!");
            }

            var fieldSideLength = (int)Math.Sqrt(game.Field.Length);
            if (model.PositionX >= fieldSideLength || model.PositionY >= fieldSideLength)
            {
                return this.BadRequest("Invalid position!");
            }

            var fieldPosition = model.PositionX + model.PositionY * fieldSideLength; // Don't do this at home!
            if (game.Field[fieldPosition] == 'X')
            {
                return this.BadRequest("Position already bombed!");
            }
            
            var field = new StringBuilder(game.Field);
            field[fieldPosition] = 'X';
            game.Field = field.ToString();
            game.State = game.State == GameState.TurnOne ? GameState.TurnTwo : GameState.TurnOne;
            this.Data.SaveChanges();

            return this.Ok();
        }
    }
}