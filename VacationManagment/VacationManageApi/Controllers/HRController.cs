using BAL.Interface;
using DAL.Model;
using Model.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace VacationManageApi.Controllers
{
	[Authorize]
	[RoutePrefix("api/HR")]
	public class HRController : ApiController
    {
		private readonly IHRManager hrManager;
		public HRController(IHRManager hrManager)
		{
			this.hrManager = hrManager;
		}
		/// <summary>
		/// Create user
		/// </summary>
		/// <param name="user"></param>
		/// <returns></returns>
		[HttpPost]
		[Route("Create")]
		public IHttpActionResult Create([FromBody]User user)
		{
			try
			{
				hrManager.Create(user);
				return Ok();
			}
			catch (Exception ex)
			{
				return NotFound();
			}
		}
		/// <summary>
		/// Set company holiday
		/// </summary>
		/// <param name="holiday"></param>
		/// <returns></returns>
		[HttpPost]
		[Route("SetCompanyHoliday")]
		public IHttpActionResult SetCompanyHoliday([FromBody]SetHolidayDTO holiday)
		{
			try
			{
				hrManager.SetCompanyHoliday(holiday);
				return Ok();
			}
			catch (Exception ex)
			{
				return NotFound();
			}
		}
		/// <summary>
		/// Set policy
		/// </summary>
		/// <param name="policy"></param>
		/// <returns></returns>
		[HttpPost]
		[Route("SetPolicy")]
		public IHttpActionResult SetPolicy([FromBody]Policy policy)
		{
			try
			{
				hrManager.SetVacationPolicy(policy);
				return Ok();
			}
			catch (Exception ex)
			{
				return NotFound();
			}
		}
	}
}
