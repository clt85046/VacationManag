using BAL.Interface;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.Cookies;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Web;
using System.Web.Mvc;
using VacationManageApi.Models;

namespace VacationManageApi.Controllers
{
	public class HomeController : Controller
	{
		private readonly IUserManager userManager;
		public HomeController(IUserManager userManager)
		{
			this.userManager = userManager;
		}
		private IAuthenticationManager AuthenticationManager
		{
			get { return HttpContext.GetOwinContext().Authentication; }
		}

		public ActionResult Index()
		{
			ViewBag.Title = "Home Page";

			return View();
		}
		/// <summary>
		///     Post for logout
		/// </summary>
		/// <returns></returns>
		public ActionResult Logout()
		{
			AuthenticationManager.SignOut(CookieAuthenticationDefaults.AuthenticationType);
			HttpContext.Response.Cookies.Set(new HttpCookie("token") { Value = string.Empty });
			return RedirectToAction("Index", "Home");
		}

		/// <summary>
		/// View for login page
		/// </summary>
		/// <returns></returns>
		[HttpGet]
		public ActionResult Login()
		{
			var model = new LoginModel();
			return View(model);
		}
		/// <summary>
		///     Method for login post;
		/// </summary>
		/// <param name="loguser"></param>
		/// <returns></returns>
		[HttpPost]
		public ActionResult Login(LoginModel loguser)
		{
			var userDb = userManager.Find(loguser.Name, loguser.Password);
			var user = new ApplicationUser
			{
				Id = userDb.Id.ToString(),
				Email = userDb.Email,
				UserName = userDb.Email
			};

			var cookiesIdentity = new ClaimsIdentity(CookieAuthenticationDefaults.AuthenticationType,
				ClaimsIdentity.DefaultNameClaimType, ClaimsIdentity.DefaultRoleClaimType);
			cookiesIdentity.AddClaim(new Claim(ClaimTypes.NameIdentifier, user.Id, ClaimValueTypes.String));
			cookiesIdentity.AddClaim(new Claim(ClaimsIdentity.DefaultNameClaimType, user.Email, ClaimValueTypes.String));
			cookiesIdentity.AddClaim(new Claim("http://schemas.microsoft.com/accesscontrolservice/201..",
				"OWIN Provider", ClaimValueTypes.String));
			AuthenticationManager.SignIn(new AuthenticationProperties { IsPersistent = true }, cookiesIdentity);
			return RedirectToAction("Index", "Home");
		}
	}
}
