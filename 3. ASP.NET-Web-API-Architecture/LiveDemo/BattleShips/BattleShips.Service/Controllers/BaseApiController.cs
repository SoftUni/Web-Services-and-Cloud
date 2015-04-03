namespace BattleShips.Service.Controllers
{
    using System.Web.Http;

    using BattleShips.Data;

    public abstract class BaseApiController : ApiController
    {
        private IBattleShipsData data;

        protected BaseApiController()
            : this(new BattleShipsData(new ApplicationDbContext()))
        {
        }

        protected BaseApiController(IBattleShipsData data)
        {
            this.data = data;
        }

        protected IBattleShipsData Data
        {
            get
            {
                return this.data;
            }
        }
    }
}