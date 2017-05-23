using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAL.Interface
{
	public interface IManagerManager
	{
		void ApproveRequest(int id, int userId);
		void DeclineRequest(int id, int userId);
	}
}
