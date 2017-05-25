using BAL.Interface;
using DAL.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;

namespace VacationManagmentApi.Controllers
{
	[Authorize]
	[RoutePrefix("api/User")]
	public class UserController : ApiController
	{
		private readonly IUserManager userManager;
		public UserController(IUserManager userManager)
		{
			this.userManager = userManager;
		}

		/// <summary>
		///     Get All users from database
		/// </summary>
		[HttpGet]
		[ResponseType(typeof(List<User>))]
		[Route("GetAll")]
		public IHttpActionResult GetAll()
		{
			try
			{
				var users = userManager.GetAll();
				return Ok(users);
			}
			catch (Exception ex)
			{
				return NotFound();
			}
		}
		/// <summary>
		/// Get role name of user by id
		/// </summary>
		/// <param name="userId"></param>
		/// <returns></returns>
		[HttpGet]
		[Route("GetRoleName/{userId}")]
		public IHttpActionResult GetRoleName(int userId)
		{
			try
			{
				var roleName = userManager.GetRoleName(userId);
				return Ok(roleName);
			}
			catch (Exception ex)
			{
				return NotFound();
			}
		}
		/// <summary>
		/// Get full name of user by id
		/// </summary>
		/// <param name="userId"></param>
		/// <returns></returns>
		[HttpGet]
		[Route("GetFullName/{userId}")]
		public IHttpActionResult GetFullName(int userId)
		{
			try
			{
				var fullName = userManager.GetFullName(userId);
				return Ok(fullName);
			}
			catch (Exception ex)
			{
				return NotFound();
			}
		}

		/// <summary>
		///  Update user in database
		/// </summary>
		/// <param name="user"></param>
		[HttpPut]
		[Route("UpdateUser")]
		public IHttpActionResult UpdateUser(User user)
		{
			try
			{
				userManager.Update(user);
				return Ok();
			}
			catch (Exception ex)
			{
				return NotFound();
			}
		}

	}
}
