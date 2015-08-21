using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SocialNetwork.Services.Controllers
{
    using System.Web.Http;
    using Data;

    public class BaseApiController : ApiController
    {
        public BaseApiController()
            : this(new SocialNetworkContext())
        {
        }

        public BaseApiController(SocialNetworkContext data)
        {
            this.Data = data;
        }

        protected SocialNetworkContext Data { get; set; }
    }
}