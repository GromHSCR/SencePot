using AutoMapper;
using Styx.GromHSCR.Api.Interfaces;
using Styx.GromHSCR.Entities;

namespace Styx.GromHSCR.DataService
{
	public class MapInitializer
	{
		public static void Initialize()
		{
			Mapper.Reset();
			Mapper.CreateMap<PrintInfo, IPrintInfo>();
			Mapper.CreateMap<PrintInfo, IPrintInfo>();
			Mapper.CreateMap<PrintInfo, IPrintInfo>();
			Mapper.CreateMap<PrintInfo, IPrintInfo>();
			Mapper.CreateMap<PrintInfo, IPrintInfo>();
			Mapper.CreateMap<PrintInfo, IPrintInfo>();
			Mapper.CreateMap<PrintInfo, IPrintInfo>();
			Mapper.CreateMap<PrintInfo, IPrintInfo>();
			Mapper.CreateMap<PrintInfo, IPrintInfo>();

			Mapper.CreateMap<PrintInfo, IPrintInfo>();

			Mapper.AssertConfigurationIsValid();
		}
	}
}
