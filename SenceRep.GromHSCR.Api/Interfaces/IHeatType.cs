using System.ComponentModel;
using System.Runtime.Serialization;

namespace SenceRep.GromHSCR.Api.Interfaces
{
    public enum HeatType
	{
		/// <summary>
		/// ГВС
		/// </summary>
		[Description("ГВС")]
		[EnumMember]
		HotWaterSupply = 1,
		/// <summary>
		/// ЦО
		/// </summary>
		[Description("ЦО")]
		[EnumMember]
		CentralHeating = 2,
		/// <summary>
		/// ГВС с циркуляцией
		/// </summary>
		[Description("ГВС с циркуляцией")]
		[EnumMember]
		HotWaterSypplyWithCycle = 4,
		/// <summary>
		/// ЦО с циркуляцией
		/// </summary>
		[Description("ЦО с циркуляцией")]
		[EnumMember]
		CentralHeatingWithCycle = 8
    }
}
