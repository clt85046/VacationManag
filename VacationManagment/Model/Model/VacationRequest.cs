using DAL.Common.Enum;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Model
{
	public class VacationRequest
	{
		[Key]
		public int Id { get; set; }
		[Required]
		public VacationType VacationType { get; set; }
		[Required]
		public DateTime StartDate { get; set; }
		[Required]
		public DateTime EndDate { get; set; }
		[ForeignKey("UserId")]
		public virtual User User { get; set; }
		public int UserId { get; set; }
		public Status? Status { get; set; }
		public string ApprovedBy { get; set; }

	}
}
