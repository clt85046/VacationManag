using BAL.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace VacationManageApi.Controllers
{
	[Authorize]
	[RoutePrefix("api/Manager")]
	public class ManagerController : ApiController
    {
		private readonly IManagerManager managerManager;
		public ManagerController(IManagerManager managerManager)
		{
			this.managerManager = managerManager;
		}
		[HttpPut]
		[Route("Approve/{id}/{userId}")]
		public IHttpActionResult Approve(int id, int userId)
		{
			try
			{
				managerManager.ApproveRequest(id, userId);
				return Ok();
			}
			catch (Exception ex)
			{
				return NotFound();
			}
		}

		[HttpPut]
		[Route("Decline/{id}/{userId}")]
		public IHttpActionResult Decline(int id, int userId)
		{
			try
			{
				managerManager.DeclineRequest(id,userId);
				return Ok();
			}
			catch (Exception ex)
			{
				return NotFound();
			}
		}
	}
}
