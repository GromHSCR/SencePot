using AutoMapper;
using SenceRep.GromHSCR.Api.Interfaces;
using SenceRep.GromHSCR.Entities;

namespace SenceRep.GromHSCR.DataService
{
	public class MapInitializer
	{
		public static void Initialize()
		{
			Mapper.Reset();
			Mapper.CreateMap<PrintInfo, IPrintInfo>();
			

			Mapper.AssertConfigurationIsValid();
		}
	}
}
