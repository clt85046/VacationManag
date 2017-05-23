﻿using Model.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAL.Interface
{
	public interface IEmployeeManager
	{
		void Create(CreateRequestDTO vacation);
		void Remove(int Id);
	}
}
