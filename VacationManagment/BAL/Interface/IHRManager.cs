using DAL.Model;
using Model.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAL.Interface
{
	public interface IHRManager
	{
		void Create(User user);
		void SetCompanyHoliday(SetHolidayDTO holiday);
		void SetVacationPolicy(Policy policy);

	}
}
