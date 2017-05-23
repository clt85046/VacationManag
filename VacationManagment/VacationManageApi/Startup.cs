using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Owin;
using Owin;
using System.Threading.Tasks;
using System.Security.Claims;

[assembly: OwinStartup(typeof(VacationManageApi.Startup))]

namespace VacationManageApi
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
		private async Task<IEnumerable<Claim>> Authenticate(string username, string password)
		{
			// authenticate user
			if (username == password)
				return new List<Claim>
				{
					new Claim("name", username)
				};

			return null;
		}
	}
}
