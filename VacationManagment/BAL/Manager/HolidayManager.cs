using BAL.Interface;
using DAL.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Model;
using Model.DTO;
using AutoMapper;

namespace BAL.Manager
{
	public class HolidayManager : BaseManager,IHolidayManager
	{
		public HolidayManager(IUnitOfWork uOW) : base(uOW)
		{
		}

		public List<HolidayLoadDTO> GetAll()
		{
			var holidays=uOW.HolidayRepo.All.ToList();
			return Mapper.Map<List<HolidayLoadDTO>>(holidays);
		}
	}
}
