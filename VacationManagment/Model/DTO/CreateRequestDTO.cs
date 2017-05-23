using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.DTO
{
	public class CreateRequestDTO
	{
		public int Id { get; set; }
		public string VacationType { get; set; }
		public string StartDate { get; set; }
		public string EndDate { get; set; }
		public int UserId { get; set; }
	}
}
