using DAL.Model;
using Model.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAL.Interface
{
	public interface IVacationManager
	{
		List<VacationRequest> GetAll();
		List<VacationRequest> GetAllById(int Id);
	}
}
