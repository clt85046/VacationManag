using DAL.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
	public class MainContext : DbContext
	{
		public MainContext()
			: base("VacationManagmentCon")
		{
			this.Configuration.LazyLoadingEnabled = true;
			var ensureDLLIsCopied =
		System.Data.Entity.SqlServer.SqlProviderServices.Instance;
		}

		public MainContext(string connString)
			: base(connString)
		{
			this.Configuration.LazyLoadingEnabled = true;
		}
		public DbSet<User> Users { get; set; }
		public DbSet<Holidays> Holidays { get; set; }
		public DbSet<Role> Roles { get; set; }
		public DbSet<VacationRequest> VacationRequests { get; set; }
		public DbSet<Policy> Policies { get; set; }


	}
}
