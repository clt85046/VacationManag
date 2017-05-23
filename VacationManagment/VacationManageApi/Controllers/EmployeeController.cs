using BAL.Interface;
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
	[RoutePrefix("api/Employee")]
	public class EmployeeController : ApiController
    {
		private readonly IEmployeeManager employeeManager;
		public EmployeeController(IEmployeeManager employeeManager)
		{
			this.employeeManager = employeeManager;
		}

		[HttpPost]
		[Route("Create")]
		public IHttpActionResult Create([FromBody]CreateRequestDTO request)
		{
			try
			{
				employeeManager.Create(request);
				return Ok();
			}
			catch (Exception ex)
			{
				return NotFound();
			}
		}

		[HttpDelete]
		[Route("Remove/{id}")]
		public IHttpActionResult Remove(int id)
		{
			try
			{
				employeeManager.Remove(id);
				return Ok();
			}
			catch (Exception ex)
			{
				return NotFound();
			}
		}

	}
}
