using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Styx.GromHSCR.Api.Interfaces;

namespace Styx.GromHSCR.DocumentParserBase.Models
{
    public class PrintInfo : IPrintInfo
    {
        public PrintInfo()
        { }

        public Guid Id { get; set; }
        public int Number { get; set; }
        public Guid AddressId { get; set; }
        public IAddress Address { get; set; }
        public Guid HeatCounterId { get; set; }
        public IHeatCounter HeatCounter { get; set; }
        public Guid? OrganizationId { get; set; }
        public IOrganization Organization { get; set; }
        public Guid? ContractId { get; set; }
        public IContract Contract { get; set; }
        public IEnumerable<IDailyData> DailyDatas { get; set; }
        public IEnumerable<IValidationResult> ValidationResults { get; set; }
	    public IEnumerable<IEntry> Entries { get; set; }
	    public decimal? Gmax { get; set; }
        public decimal? Gmin { get; set; }
        public decimal? Glinear { get; set; }
        public decimal? Greturn { get; set; }
        public decimal? Kv { get; set; }
        public decimal? Fmax { get; set; }
	    public TimeSpan? CurrentWorkTime { get; set; }
	    public decimal? CurrentDayEndTotalEnergy { get; set; }
	    public decimal? CurrentV1 { get; set; }
	    public decimal? CurrentV2 { get; set; }
	    public DateTime PrintStartDate { get; set; }
        public DateTime PrintEndDate { get; set; }
        public DateTime LoadDateTime { get; set; }
    }
}
