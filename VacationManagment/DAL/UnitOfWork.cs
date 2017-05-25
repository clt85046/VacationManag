using DAL.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Model;
using DAL.Repositories;
using System.Data.Entity.Validation;

namespace DAL
{
	public class UnitOfWork : IUnitOfWork, IDisposable
	{
		private MainContext context;

		private IGenericRepository<User> userRepo;
		private IGenericRepository<Holidays> holidayRepo;
		private IGenericRepository<Role> roleRepo;
		private IGenericRepository<VacationRequest> vacationRepo;
		private IGenericRepository<Policy> policyRepo;
		public UnitOfWork()
		{
			context = new MainContext();
		}
		public IGenericRepository<Policy> PolicyRepo
		{
			get
			{
				if (policyRepo == null) policyRepo = new GenericRepository<Policy>(context);
				return policyRepo;
			}
		}

		public IGenericRepository<User> UserRepo
		{
			get
			{
				if (userRepo==null) userRepo = new GenericRepository<User>(context);
				return userRepo;
			}
		}
		public IGenericRepository<Holidays> HolidayRepo
		{
			get
			{
				if (holidayRepo==null) holidayRepo = new GenericRepository<Holidays>(context);
				return holidayRepo;
			}
		}
		public IGenericRepository<Role> RoleRepo
		{
			get
			{
				if (roleRepo==null) roleRepo = new GenericRepository<Role>(context);
				return roleRepo;
			}
		}
		public IGenericRepository<VacationRequest> VacationRepo
		{
			get
			{
				if (vacationRepo==null) vacationRepo = new GenericRepository<VacationRequest>(context);
				return vacationRepo;
			}
		}

		private bool disposed = false;
		protected virtual void Dispose(bool disposing)
		{
			if (!this.disposed)
			{
				if (disposing)
				{
					context.Dispose();
				}
			}
			this.disposed = true;
		}

		public void Dispose()
		{
			Dispose(true);
			GC.SuppressFinalize(this);
		}

		public int Save()
		{
			try
			{
				return context.SaveChanges();
			}
			catch (DbEntityValidationException ex)
			{
				return 0;
			}
		}
	}
}
