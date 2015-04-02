namespace SoftUniSystem.Services.Controllers
{
    using System.Web.Http;

    using SoftUniSystem.Data;

    public abstract class BaseApiConroller : ApiController
    {
        private ISoftUniSystemData data;

        protected BaseApiConroller(ISoftUniSystemData data)
        {
            this.data = data;
        }

        public ISoftUniSystemData Data
        {
            get
            {
                return this.data;
            }
        }
    }
}