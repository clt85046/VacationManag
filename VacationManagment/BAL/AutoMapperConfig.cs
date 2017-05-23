using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAL
{
	public class AutoMapperConfig
	{
		public static void Configure()
		{
			Mapper.Initialize(
				cfg => cfg.AddProfile(new MappingProfile())
				);
		}
	}
}
