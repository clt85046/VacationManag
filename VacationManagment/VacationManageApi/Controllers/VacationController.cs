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
				var resultRequests = new List<VacationRequestDTO>();
				foreach (var req in vacations)
				{
					var requestDTO = new VacationRequestDTO()
					{
						Id = req.Id,
						ApprovedBy = req.ApprovedBy,
						EndDate = req.EndDate,
						StartDate = req.StartDate,
						FullName = String.Format("{0} {1}", userManager.GetById(req.UserId).LastName, userManager.GetById(req.UserId).FirstName),
						Status = Enum.GetName(typeof(Status), req.Status),
						UserId = req.UserId,
						VacationType = Enum.GetName(typeof(VacationType), req.VacationType)
					};
					resultRequests.Add(requestDTO);
				}
				return Ok(resultRequests);
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
				var resultRequests = new List<VacationRequestDTO>();
				foreach (var req in vacations)
				{
					var requestDTO = new VacationRequestDTO()
					{
						Id = req.Id,
						ApprovedBy = req.ApprovedBy,
						EndDate = req.EndDate,
						StartDate = req.StartDate,
						FullName = String.Format("{0} {1}", userManager.GetById(req.UserId).LastName, userManager.GetById(req.UserId).FirstName),
						Status = Enum.GetName(typeof(Status), req.Status),
						UserId = req.UserId,
						VacationType = Enum.GetName(typeof(VacationType), req.VacationType)
					};
					resultRequests.Add(requestDTO);
				}
				return Ok(resultRequests);
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
				var vacations = vacationManager.GetAll().Where(i=>i.Status==Status.Approved);
				var resultRequests = new List<VacationRequestDTO>();
				foreach (var req in vacations)
				{
					var requestDTO = new VacationRequestDTO()
					{
						Id = req.Id,
						ApprovedBy = req.ApprovedBy,
						EndDate = req.EndDate,
						StartDate = req.StartDate,
						FullName = String.Format("{0} {1}", userManager.GetById(req.UserId).LastName, userManager.GetById(req.UserId).FirstName),
						Status = Enum.GetName(typeof(Status), req.Status),
						UserId = req.UserId,
						VacationType = Enum.GetName(typeof(VacationType), req.VacationType)
					};
					resultRequests.Add(requestDTO);
				}
				return Ok(resultRequests);
			}
			catch (Exception ex)
			{
				return NotFound();
			}
		}
	}
}
