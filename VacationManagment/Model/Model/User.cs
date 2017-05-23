using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Model
{
	public class User
	{
		public User()
		{
			VacationList = new List<VacationRequest>();
		}
		[Key]
		public int Id { get; set; }
		[Required]
		public string FirstName { get; set; }
		[Required]
		public string LastName { get; set; }
		public int PaidDayOffs { get; set; }
		public int PaidSickness { get; set; }
		public int UnPaidDayOffs { get; set; }
		public int UnPaidSickness { get; set; }
		[DataType(DataType.EmailAddress)]
		[Required]
		public string UserName { get; set; }
		[Required]
		public string Email { get; set; }
		[DataType(DataType.Password)]
		[Required]
		public string Password { get; set; }
		[Required]
		public int YearsOfService { get; set; }
		[ForeignKey("RoleId")]
		public virtual Role Role { get; set; }
		public int RoleId { get; set; }
		public virtual List<VacationRequest> VacationList { get; set; }

	}
}
