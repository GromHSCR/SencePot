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

		public Guid Id
		{
			get
			{
				return Model.Id;
			}
			set
			{
				if (Model.Id == value) return;

				RaisePropertyChanging("Id");
				Model.Id = value;
				RaisePropertyChanged("Id");

				IsModified = true;
			}
		}
		public int Number
		{
			get
			{
				return Model.Number;
			}
			set
			{
				if (Model.Number == value) return;

				RaisePropertyChanging("Number");
				Model.Number = value;
				RaisePropertyChanged("Number");

				IsModified = true;
			}
		}

		public Guid AddressId { get; set; }

		public IAddress Address
		{
			get
			{
				return Model.Address;
			}
			set
			{
				if (Model.Address == value) return;

				RaisePropertyChanging("Address");
				Model.Address = value;
				RaisePropertyChanged("Address");

				IsModified = true;
			}
		}

		public Guid HeatCounterId { get; set; }


		public string FullAddress
		{
			get
			{
				return (Address.Street != null ? Address.Street.Name : "") +
					   (Address.House != null ? " д." + Address.House.Number.ToString() : "") +
					   (Address.Housing != null ? " к." + Address.Housing.Number.ToString() : "") +
					   (Address.Housing != null ? " с." + Address.Housing.Number.ToString() : "");
			}
		}

		public IHeatCounter HeatCounter { get; set; }
		public Guid? OrganizationId { get; set; }
		public IOrganization Organization { get; set; }
		public Guid? ContractId { get; set; }
		public IContract Contract { get; set; }
		public IEnumerable<IDailyData> DailyDatas { get; set; }
	    public IEnumerable<IValidationResult> ValidationResults { get; set; }

	    public decimal? Gmax
		{
			get
			{
				return Model.Gmax;
			}
			set
			{
				if (Model.Gmax == value) return;

				RaisePropertyChanging("Gmax");
				Model.Gmax = value;
				RaisePropertyChanged("Gmax");

				IsModified = true;
			}
		}
		public decimal? Gmin
		{
			get
			{
				return Model.Gmin;
			}
			set
			{
				if (Model.Gmin == value) return;

				RaisePropertyChanging("Gmin");
				Model.Gmin = value;
				RaisePropertyChanged("Gmin");

				IsModified = true;
			}
		}
		public decimal? Glinear
		{
			get
			{
				return Model.Glinear;
			}
			set
			{
				if (Model.Glinear == value) return;

				RaisePropertyChanging("Glinear");
				Model.Glinear = value;
				RaisePropertyChanged("Glinear");

				IsModified = true;
			}
		}
		public decimal? Greturn
		{
			get
			{
				return Model.Greturn;
			}
			set
			{
				if (Model.Greturn == value) return;

				RaisePropertyChanging("Greturn");
				Model.Greturn = value;
				RaisePropertyChanged("Greturn");

				IsModified = true;
			}
		}
		public decimal? Kv
		{
			get
			{
				return Model.Kv;
			}
			set
			{
				if (Model.Kv == value) return;

				RaisePropertyChanging("Kv");
				Model.Kv = value;
				RaisePropertyChanged("Kv");

				IsModified = true;
			}
		}
		public decimal? Fmax
		{
			get
			{
				return Model.Fmax;
			}
			set
			{
				if (Model.Fmax == value) return;

				RaisePropertyChanging("Fmax");
				Model.Fmax = value;
				RaisePropertyChanged("Fmax");

				IsModified = true;
			}
		}
		public DateTime PrintStartDate
		{
			get
			{
				return Model.PrintStartDate;
			}
			set
			{
				if (Model.PrintStartDate == value) return;

				RaisePropertyChanging("PrintStartDate");
				Model.PrintStartDate = value;
				RaisePropertyChanged("PrintStartDate");

				IsModified = true;
			}
		}
		public DateTime PrintEndDate
		{
			get
			{
				return Model.PrintEndDate;
			}
			set
			{
				if (Model.PrintEndDate == value) return;

				RaisePropertyChanging("PrintEndDate");
				Model.PrintEndDate = value;
				RaisePropertyChanged("PrintEndDate");

				IsModified = true;
			}
		}

        public DateTime LoadDateTime
        {
            get
            {
                return Model.LoadDateTime;
            }
            set
            {
                if (Model.LoadDateTime == value) return;

                RaisePropertyChanging("LoadDateTime");
                Model.LoadDateTime = value;
                RaisePropertyChanged("LoadDateTime");

                IsModified = true;
            }
        }
	}
}
