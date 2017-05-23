using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Model
{
	public class Holidays
	{
		[Key]
		public int Id { get; set; }
		[Required]
		public DateTime Date { get; set; }
		public string Name { get; set; }

		[ForeignKey("HRId")]
		public virtual User HR { get; set; }

		public int HRId { get; set; }

	}
}
