using BAL.Interface;
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
	[RoutePrefix("api/Holiday")]
	public class HolidayController : ApiController
    {
		private readonly IHolidayManager holidayManager;
		public HolidayController(IHolidayManager holidayManager)
		{
			this.holidayManager = holidayManager;
		}
		[HttpGet]
		[Route("GetAll")]
		[ResponseType(typeof(List<HolidayLoadDTO>))]
		public IHttpActionResult GetAll()
		{
			try
			{
				var holidays = holidayManager.GetAll();
				return Ok(holidays);
			}
			catch (Exception ex)
			{
				return NotFound();
			}
		}

	}
}
