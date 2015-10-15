using System;
using AutoMapper;
using Styx.GromHSCR.Api.Interfaces;
using Styx.GromHSCR.Entities;

namespace Styx
{
	public class MapInitializer
	{
		public static void Initialize()
		{
			Mapper.Reset();

			//forward
			Mapper.CreateMap<PrintInfo, IPrintInfo>();
			Mapper.CreateMap<Address, IAddress>();
			Mapper.CreateMap<City, ICity>();
			Mapper.CreateMap<Region, IRegion>();
			Mapper.CreateMap<Street, IStreet>();
			Mapper.CreateMap<House, IHouse>();
			Mapper.CreateMap<Building, IBuilding>();
			Mapper.CreateMap<Housing, IHousing>();
			Mapper.CreateMap<HousePrefix, IHousePrefix>();
			Mapper.CreateMap<HeatCounter, IHeatCounter>();
			Mapper.CreateMap<CounterModel, ICounterModel>();
			Mapper.CreateMap<DailyData, IDailyData>()
				.ForMember(dest => dest.WorkTime, opt => opt.MapFrom(p => TimeSpan.FromHours(p.WorkTime)))
				.ForMember(dest => dest.CurrentWorkTime, opt => opt.MapFrom(p => p.CurrentWorkTime.HasValue ? (TimeSpan?)TimeSpan.FromHours(p.CurrentWorkTime.Value) : null));
			Mapper.CreateMap<LegalEntity, ILegalEntity>();
			Mapper.CreateMap<Organization, IOrganization>();
            Mapper.CreateMap<Contract, IContract>();
            Mapper.CreateMap<ValidationResult, IValidationResult>();

			//back
			Mapper.CreateMap<IPrintInfo, PrintInfo>();
			Mapper.CreateMap<IAddress, Address>();
			Mapper.CreateMap<ICity, City>();
			Mapper.CreateMap<IRegion, Region>();
			Mapper.CreateMap<IStreet, Street>();
			Mapper.CreateMap<IHouse, House>();
			Mapper.CreateMap<IBuilding, Building>();
			Mapper.CreateMap<IHousing, Housing>();
			Mapper.CreateMap<IHousePrefix, HousePrefix>();
			Mapper.CreateMap<IHeatCounter, HeatCounter>();
			Mapper.CreateMap<ICounterModel, CounterModel>();
			Mapper.CreateMap<IDailyData, DailyData>()
				.ForMember(dest => dest.WorkTime, opt => opt.MapFrom(p => p.WorkTime.Hours))
				.ForMember(dest => dest.CurrentWorkTime, opt => opt.MapFrom(p => p.CurrentWorkTime.HasValue ? (int?)p.CurrentWorkTime.Value.Hours : null));
			Mapper.CreateMap<ILegalEntity, LegalEntity>();
			Mapper.CreateMap<IOrganization, Organization>();
            Mapper.CreateMap<IContract, Contract>();
            Mapper.CreateMap<IValidationResult, ValidationResult>();

			Mapper.AssertConfigurationIsValid();
		}
	}
}
