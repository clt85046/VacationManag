using BAL.Interface;
using DAL.Common.Enum;
using Model.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;

namespace VacationManageApi.Controllers
{
	[Authorize]
	[RoutePrefix("api/Vacation")]
	public class VacationController : ApiController
    {
		private readonly IUserManager userManager;
		private readonly IVacationManager vacationManager;
		public VacationController(IUserManager userManager,IVacationManager vacationManager)
		{
			this.userManager = userManager;
			this.vacationManager = vacationManager;
		}
		/// <summary>
		/// Get all vacation requests
		/// </summary>
		/// <returns></returns>
		[HttpGet]
		[Route("GetAll")]
		[ResponseType(typeof(List<VacationRequestDTO>))]
		public IHttpActionResult GetAll()
		{
			try
			{
				var vacations = vacationManager.GetAll();
				return Ok(vacations);
			}
			catch (Exception ex)
			{
				return NotFound();
			}
		}

		/// <summary>
		/// Get all request for one employee
		/// </summary>
		/// <param name="userId"></param>
		/// <returns></returns>
		[HttpGet]
		[Route("GetAllById/{userId}")]
		[ResponseType(typeof(List<VacationRequestDTO>))]
		public IHttpActionResult GetAllById(int userId)
		{
			try
			{
				var vacations = vacationManager.GetAllById(userId);
				return Ok(vacations);
			}
			catch (Exception ex)
			{
				return NotFound();
			}
		}
		/// <summary>
		/// Get all approved requests for default user
		/// </summary>
		/// <returns></returns>
		[HttpGet]
		[Route("GetAllApproved")]
		[ResponseType(typeof(List<VacationRequestDTO>))]
		public IHttpActionResult GetAllApproved()
		{
			try
			{
				var approvedVacations = vacationManager.GetAllApproved();
				return Ok(approvedVacations);
			}
			catch (Exception ex)
			{
				return NotFound();
			}
		}
	}
}
