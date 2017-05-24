using AutoMapper;
using DAL.Common.Enum;
using DAL.Model;
using Model.DTO;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAL
{
	public class MappingProfile : Profile
	{
		public MappingProfile()
		{
			CreateMap<CreateRequestDTO, VacationRequest>()
				.ForMember(x => x.EndDate,y => y.MapFrom(t => DateTime.ParseExact(
				t.EndDate,
				"MM/dd/yyyy",
				CultureInfo.InvariantCulture,DateTimeStyles.None)))
				.ForMember(x => x.StartDate, y => y.MapFrom(t => DateTime.ParseExact(
				 t.StartDate,
				 "MM/dd/yyyy",
				 CultureInfo.InvariantCulture, DateTimeStyles.None)))
				.ForMember(x => x.VacationType, y => y.MapFrom(t => (VacationType)Enum.Parse(typeof(VacationType),t.VacationType)))
				.ForMember(x => x.Id, y => y.MapFrom(t => t.Id))
				;
			CreateMap<VacationRequest, CreateRequestDTO>();
			CreateMap<Holidays, HolidayLoadDTO>()
				.ForMember(x => x.StartDate, y => y.MapFrom(t => t.Date))
				.ForMember(x => x.EndDate, y => y.MapFrom(t => t.Date))
				;
			CreateMap<HolidayLoadDTO, Holidays>();
			CreateMap<SetHolidayDTO, Holidays>()
				.ForMember(x => x.Date, y => y.MapFrom(t => DateTime.ParseExact(
				 t.Date,
				 "MM/dd/yyyy",
				 CultureInfo.InvariantCulture, DateTimeStyles.None)));
			CreateMap<Holidays, SetHolidayDTO>();

		}
	}
}
