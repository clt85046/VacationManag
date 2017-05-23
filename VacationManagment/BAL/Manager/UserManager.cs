using BAL.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Model;
using DAL.Interface;
using DAL.Common.Enum;

namespace BAL.Manager
{
	public class UserManager : BaseManager, IUserManager
	{
		public UserManager(IUnitOfWork uOW) : base(uOW)
		{
		}

		/// <summary>
		/// Find user
		/// </summary>
		/// <param name="username"></param>
		/// <param name="password"></param>
		/// <returns></returns>
		public User Find(string username, string password)
		{
			var user = uOW.UserRepo.All.FirstOrDefault(i => i.UserName == username && i.Password == password);
			return user;
		}
		public List<User> GetAll()
		{
			var users = new List<User>();
			foreach (var user in uOW.UserRepo.All)
			{
				var User = uOW.UserRepo.GetByID(user.Id);
				users.Add(User);
			}
			return users;
		}

		public User GetById(int id)
		{
			return uOW.UserRepo.GetByID(id);
		}

		public string GetRoleName(int id)
		{
			return uOW.UserRepo.GetByID(id).Role.Name;
		}

		public string GetFullName(int id)
		{
			return uOW.UserRepo.GetByID(id).LastName + " " + uOW.UserRepo.GetByID(id).FirstName;
		}

		public void Update(User user)
		{
			uOW.UserRepo.Update(user);
			uOW.Save();
		}
	}
}
