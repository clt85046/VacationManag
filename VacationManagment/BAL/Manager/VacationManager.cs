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
using DAL.Common.Enum;
using System.Globalization;

namespace BAL.Manager
{
	public class VacationManager : BaseManager, IVacationManager
	{
		public VacationManager(IUnitOfWork uOW) : base(uOW)
		{
		}

		public List<VacationRequestDTO> GetAll()
		{
			return Mapper.Map<List<VacationRequestDTO>>(uOW.VacationRepo.All.ToList());
		}

		public List<VacationRequestDTO> GetAllById(int Id)
		{
			return Mapper.Map<List<VacationRequestDTO>>(uOW.VacationRepo.All.Where(i => i.UserId == Id).ToList());
		}
		public List<VacationRequestDTO> GetAllApproved()
		{
			return Mapper.Map<List<VacationRequestDTO>>(uOW.VacationRepo.All.Where(i => i.Status==Status.Approved).ToList());
		}
	}
}
