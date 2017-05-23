using DAL.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAL.Interface
{
	public interface IUserManager
	{
		List<User> GetAll();
		User Find(string email, string password);
		User GetById(int id);
		void Update(User user);
		string GetRoleName(int id);
		string GetFullName(int id);
	}
}
