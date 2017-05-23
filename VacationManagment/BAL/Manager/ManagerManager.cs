using BAL.Interface;
using DAL.Common.Enum;
using DAL.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAL.Manager
{
	public class ManagerManager : BaseManager, IManagerManager
	{
		public ManagerManager(IUnitOfWork uOW) : base(uOW)
		{
		}
		public void ApproveRequest(int id, int userId)
		{
			if (id == 0) return;
			var request = uOW.VacationRepo.GetByID(id);
			request.Status = Status.Approved;
			var manager = uOW.UserRepo.GetByID(userId);
			var managerFullName = manager.LastName +" " + manager.FirstName;
			request.ApprovedBy = managerFullName;
			uOW.VacationRepo.Update(request);
			uOW.Save();

		}
		public void DeclineRequest(int id, int userId)
		{
			if (id == 0) return;
			var request = uOW.VacationRepo.GetByID(id);
			request.Status = Status.Declined;
			var manager = uOW.UserRepo.GetByID(userId);
			var managerFullName = manager.LastName + manager.FirstName;
			request.ApprovedBy = managerFullName;
			uOW.VacationRepo.Update(request);
			uOW.Save();
		}

	}
}
