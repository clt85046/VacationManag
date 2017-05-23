using AutoMapper;
using BAL.Interface;
using DAL.Interface;
using DAL.Model;
using Model.DTO;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAL.Manager
{
	public class HRManager : BaseManager, IHRManager
	{
		public HRManager(IUnitOfWork uOW) : base(uOW)
		{
		}
		public void Create(User user)
		{
			if (user == null) return;
			var policy = uOW.PolicyRepo.All.FirstOrDefault(p => p.MinYearsOfOffice <= user.YearsOfService && p.MaxYearsOfOffice >= user.YearsOfService);
			if (policy == null)
			{
				//badRequest
				return;
			}
			var employeeRoleId = uOW.RoleRepo.All.FirstOrDefault(r => r.Name == "Employee").Id;
			user.RoleId = employeeRoleId;
			user = SetVacationDays(user, policy);

			uOW.UserRepo.Insert(user);
			uOW.Save();
		}
		public void SetCompanyHoliday(SetHolidayDTO holiday)
		{
			if (holiday == null) return;
			uOW.HolidayRepo.Insert(Mapper.Map<Holidays>(holiday));
			uOW.Save();
		}
		public void SetVacationPolicy(Policy policy)
		{
			var existPolicy = uOW.PolicyRepo.All.FirstOrDefault(p => p.MinYearsOfOffice == policy.MinYearsOfOffice && p.MaxYearsOfOffice == policy.MaxYearsOfOffice);
			if (existPolicy != null)
			{
				existPolicy = policy;
			}
			else
			{
				uOW.PolicyRepo.Insert(policy);
			}
			uOW.Save();
		}

		#region Helpers
		User SetVacationDays(User user, Policy policy)
		{
			user.PaidDayOffs = policy.PaidDayOffs;
			user.PaidSickness = policy.PaidSickness;
			user.UnPaidDayOffs = policy.UnPaidDayOffs;
			user.UnPaidSickness = policy.UnPaidSickness;

			return user;
		}
		#endregion Helpers

	}
}
