using DAL.Common.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.DTO
{
	public class VacationRequestDTO
	{
		public int Id { get; set; }
		public string VacationType { get; set; }
		public string FullName { get; set; }
		public DateTime StartDate { get; set; }
		public DateTime EndDate { get; set; }
		public int UserId { get; set; }
		public string Status { get; set; }
		public string ApprovedBy { get; set; }
	}
}
