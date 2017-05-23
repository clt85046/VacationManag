using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Model
{
		public class Policy
		{
			[Key]
			public int Id { get; set; }

			public int MinYearsOfOffice { get; set; }

			public int MaxYearsOfOffice { get; set; }

			public int PaidDayOffs { get; set; }

			public int PaidSickness { get; set; }

			public int UnPaidDayOffs { get; set; }

			public int UnPaidSickness { get; set; }
		}
}
