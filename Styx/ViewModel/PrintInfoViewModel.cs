using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Styx.GromHSCR.Api.Interfaces;
using Styx.GromHSCR.MvvmBase.Attributes;
using Styx.GromHSCR.MvvmBase.ViewModels;

namespace Styx.ViewModel
{
    [AffectIsModified]
    public class PrintInfoViewModel : ViewModelBase, IPrintInfo
    {
		public IPrintInfo Model { get; set; }

        public PrintInfoViewModel(IPrintInfo printInfo)
        {
	        Model = printInfo;
        }

	    public Guid Id { get; set; }
	    public int Number { get; set; }
	    public Guid AddressId { get; set; }
	    public IAddress Address { get; set; }
	    public Guid HeatCounterId { get; set; }
	    public IHeatCounter HeatCounter { get; set; }
	    public Guid OrganizationId { get; set; }
	    public IOrganization Organization { get; set; }
	    public Guid ContractId { get; set; }
	    public IContract Contract { get; set; }
	    public IEnumerable<IDailyData> DailyDatas { get; set; }
	    public decimal? Gmax { get; set; }
	    public decimal? Gmin { get; set; }
	    public decimal? Glinear { get; set; }
	    public decimal? Greturn { get; set; }
	    public decimal? Kv { get; set; }
	    public decimal? Fmax { get; set; }
	    public DateTime PrintStartDate { get; set; }
	    public DateTime PrintEndDate { get; set; }
    }
}
