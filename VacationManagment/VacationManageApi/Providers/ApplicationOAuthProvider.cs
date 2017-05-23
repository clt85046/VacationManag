using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.Cookies;
using Microsoft.Owin.Security.OAuth;
using VacationManageApi.Models;
using BAL.Manager;
using DAL;

namespace VacationManageApi.Providers
{
	public class ApplicationOAuthProvider : OAuthAuthorizationServerProvider
	{
		private readonly string _publicClientId;

		public ApplicationOAuthProvider(string publicClientId)
		{
			if (publicClientId == null)
				throw new ArgumentNullException("publicClientId");

			_publicClientId = publicClientId;
		}


		public override async Task GrantResourceOwnerCredentials(OAuthGrantResourceOwnerCredentialsContext context)
		{
			var userManager = new UserManager(new UnitOfWork());
			var userDb = userManager.Find(context.UserName, context.Password);
			//ApplicationUser user = await userManager.FindAsync(context.UserName, context.Password); 

			if (userDb == null)
			{
				context.SetError("invalid_grant", "The user name or password is incorrect.");
				return;
			}
			var user = new ApplicationUser
			{
				Id = userDb.Id.ToString(),
				Email = userDb.Email,
				UserName = userDb.UserName
			};

			var claim = new ClaimsIdentity(OAuthDefaults.AuthenticationType, ClaimsIdentity.DefaultNameClaimType,
				ClaimsIdentity.DefaultRoleClaimType);
			claim.AddClaim(new Claim(ClaimTypes.NameIdentifier, user.Id, ClaimValueTypes.String));
			claim.AddClaim(new Claim(ClaimsIdentity.DefaultNameClaimType, user.Email, ClaimValueTypes.String));
			claim.AddClaim(new Claim("http://schemas.microsoft.com/accesscontrolservice/201..",
				"OWIN Provider", ClaimValueTypes.String));

			var cookiesIdentity = new ClaimsIdentity(CookieAuthenticationDefaults.AuthenticationType,
				ClaimsIdentity.DefaultNameClaimType, ClaimsIdentity.DefaultRoleClaimType);
			cookiesIdentity.AddClaim(new Claim(ClaimTypes.NameIdentifier, user.Id, ClaimValueTypes.String));
			cookiesIdentity.AddClaim(new Claim(ClaimsIdentity.DefaultNameClaimType, user.Email, ClaimValueTypes.String));
			cookiesIdentity.AddClaim(new Claim("http://schemas.microsoft.com/accesscontrolservice/201..",
				"OWIN Provider", ClaimValueTypes.String));

			var properties = CreateProperties(user.UserName);
			var ticket = new AuthenticationTicket(claim, properties);
			context.Validated(ticket);
			context.Request.Context.Authentication.SignIn(cookiesIdentity);
		}

		public override Task TokenEndpoint(OAuthTokenEndpointContext context)
		{
			foreach (var property in context.Properties.Dictionary)
				context.AdditionalResponseParameters.Add(property.Key, property.Value);

			return Task.FromResult<object>(null);
		}

		public override Task ValidateClientAuthentication(OAuthValidateClientAuthenticationContext context)
		{
			// Resource owner password credentials does not provide a client ID.
			if (context.ClientId == null)
				context.Validated();

			return Task.FromResult<object>(null);
		}

		public override Task ValidateClientRedirectUri(OAuthValidateClientRedirectUriContext context)
		{
			if (context.ClientId == _publicClientId)
			{
				var expectedRootUri = new Uri(context.Request.Uri, "/");

				if (expectedRootUri.AbsoluteUri == context.RedirectUri)
					context.Validated();
			}

			return Task.FromResult<object>(null);
		}

		public static AuthenticationProperties CreateProperties(string userName)
		{
			IDictionary<string, string> data = new Dictionary<string, string>
			{
				{"userName", userName}
			};
			return new AuthenticationProperties(data);
		}
	}
}