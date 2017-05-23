using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.DTO
{
	public class SetHolidayDTO
	{
		public int Id { get; set; }
		public string Date { get; set; }
		public string Name { get; set; }
		public int HRId { get; set; }
	}
}
