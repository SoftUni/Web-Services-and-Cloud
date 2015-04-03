using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Owin;
using Owin;

[assembly: OwinStartup(typeof(BattleShips.Service.Startup))]

namespace BattleShips.Service
{
    using System.Web.Http;
    using System.Reflection;
    using BattleShips.Data;

    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
