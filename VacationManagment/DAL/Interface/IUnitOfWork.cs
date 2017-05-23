using DAL.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Interface
{
	public interface IUnitOfWork
	{
		IGenericRepository<User> UserRepo { get; }
		IGenericRepository<Holidays> HolidayRepo { get; }
		IGenericRepository<Role> RoleRepo { get; }
		IGenericRepository<VacationRequest> VacationRepo { get; }
		IGenericRepository<Policy> PolicyRepo { get; }

		void Dispose();

		int Save();
	}
}
