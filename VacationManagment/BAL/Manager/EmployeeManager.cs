using AutoMapper;
using BAL.Interface;
using DAL.Common.Enum;
using DAL.Interface;
using DAL.Model;
using Model.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAL.Manager
{
	public class EmployeeManager : BaseManager, IEmployeeManager
	{
		public EmployeeManager(IUnitOfWork uOW) : base(uOW)
		{
		}
		/// <summary>
		/// Create vacation request
		/// </summary>
		/// <param name="vacation"></param>
		public void Create(CreateRequestDTO vacation)
		{
			if (vacation == null) return;
			var vacationDb = Mapper.Map<VacationRequest>(vacation);
			var isDublicate = uOW.VacationRepo.All.Where(i => i.EndDate == vacationDb.EndDate || i.StartDate == vacationDb.StartDate && i.UserId == vacationDb.UserId) != null ? true : false;
			var isOverlapse= uOW.VacationRepo.All.Where(i => vacationDb.EndDate >= i.StartDate && vacationDb.StartDate <= i.EndDate) != null ? true : false;

			if (vacationDb.Id != 0 || isDublicate)
			{
				vacationDb.Status = Status.InQueue;
				uOW.VacationRepo.Update(vacationDb);
			}
			else
			{
				vacationDb = CheckPolicies(vacationDb);
				if (vacationDb == null) return;
				vacationDb.Status = Status.InQueue;
				uOW.VacationRepo.Insert(vacationDb);
			}
			uOW.Save();
		}
		/// <summary>
		/// Remove request 
		/// </summary>
		/// <param name="Id"></param>
		public void Remove(int Id)
		{
			var request = uOW.VacationRepo.GetByID(Id);
			uOW.VacationRepo.Delete(request);
			uOW.Save();
		}


		#region Helpers
		/// <summary>
		/// Check policies of user for needed type of vacation
		/// </summary>
		/// <param name="vacation"></param>
		/// <returns></returns>
		VacationRequest CheckPolicies(VacationRequest vacation)
		{
			var user = uOW.UserRepo.GetByID(vacation.UserId);
			int vacationDays = (vacation.EndDate - vacation.StartDate).Days;
			if (vacationDays < 0) return null;

			var yearsOfOffice = user.YearsOfService;
			var policy = uOW.PolicyRepo.All.FirstOrDefault(p => p.MinYearsOfOffice <= yearsOfOffice && p.MaxYearsOfOffice >= yearsOfOffice);

			var vacationType = Enum.GetName(typeof(VacationType), vacation.VacationType);

			var remainDays = (int)GetPropValue(user, vacationType);

			if (remainDays < vacationDays) return null;
			if (vacationDays == 0) vacationDays = 1;
			int newRemainDays = remainDays - vacationDays;
			vacation.User = UpdateUserRemainDays(user, vacationType, newRemainDays);

			return vacation;
		}
		/// <summary>
		/// Update remain 
		/// </summary>
		/// <param name="user"></param>
		/// <param name="vacationType"></param>
		/// <param name="remaindDays"></param>
		/// <returns></returns>
		User UpdateUserRemainDays(User user, string vacationType, int remaindDays)
		{
			user.GetType().GetProperty(vacationType).SetValue(user, remaindDays);

			return user;
		}

		public static object GetPropValue(object src, string propName)
		{
			return src.GetType().GetProperty(propName).GetValue(src, null);
		}
		#endregion Helpers
	}
}
