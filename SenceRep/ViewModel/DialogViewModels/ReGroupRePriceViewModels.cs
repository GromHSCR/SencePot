using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using RedKassa.Promoter.Helpers;
using RedKassa.Promoter.MvvmBase.ViewModels;
using RedKassa.Promoter.ViewModel.Schemas;

namespace RedKassa.Promoter.ViewModel.DialogViewModels
{
	public class ReGroupRePriceViewModels : ViewModelBase
	{
		public RePriceGroupSeatViewModel FirstModel { get; set; }

		public RePriceGroupSeatViewModel SecondModel { get; set; }

		public ReGroupRePriceViewModels(RePriceGroupSeatViewModel model)
		{
			if (model == null) throw new ArgumentNullException("model");
			FirstModel = model;
			AllSeats = FirstModel.Seats;
			SecondModel = new RePriceGroupSeatViewModel(new List<SeatViewModel>(), FirstModel.OldPrice, FirstModel.Sector);
			if (FirstModel != null && FirstModel.Seats != null && FirstModel.Sector != null)
			{
				if (!FirstModel.Sector.IsWithoutSeat)
				{
					var rows =
						FirstModel.Sector.Rows.Where(
							s =>
								s.Seats.Where(x => x.SeanceSchemaSeat != null)
									.Any(
										x =>
											FirstModel.Seats.Where(p => p.SeanceSchemaSeat != null)
												.Select(p => p.SeanceSchemaSeat.Id)
												.Contains(x.SeanceSchemaSeat.Id)))
							.OrderBy(p => p.Order);
					var rowNames = rows.ToDictionary(p => p.Id, x => "Ряд " + x.Name).Distinct();
					var seats =
						rows.SelectMany(
							p =>
								p.Seats.Where(x => x.SeanceSchemaSeat != null)
									.Where(
										r =>
											FirstModel.Seats.Where(x => x.SeanceSchemaSeat != null)
												.Select(s => s.SeanceSchemaSeat.Id)
												.Contains(r.SeanceSchemaSeat.Id))).ToList();
					_rows = (from rowName in rowNames
							 let name = rowName
							 select new ReGroupRowViewModel(seats.Where(p => p.RowId == name.Key), rowName)).ToList();
				}
				else
				{
					_rows = new List<ReGroupRowViewModel> { new ReGroupRowViewModel(AllSeats) };
				}

			}
		}

		public IEnumerable<SeatViewModel> AllSeats { get; set; }

		private IEnumerable<ReGroupRowViewModel> _rows;
		public IEnumerable<ReGroupRowViewModel> Rows
		{
			get { return _rows; }
			set
			{
				if (Equals(_rows, value)) return;
				RaisePropertyChanging("Rows");
				_rows = value;
				RaisePropertyChanged("Rows");
			}
		}
	}

	public class ReGroupRowViewModel : ViewModelBase
	{
		public ReGroupRowViewModel(IEnumerable<SeatViewModel> seats, KeyValuePair<Guid, string> rowName = default(KeyValuePair<Guid, string>))
		{
			_rowName = rowName;
			SeatViewModels = seats.ToList();
			if (SeatViewModels != null)
			{
				_firstGroupSeats = SeatViewModels;
				_firstGroupSeatNumbers = _rowName.Value == null ? _firstGroupSeats.Count().ToString() : _firstGroupSeats.Select(p => p.Name).ToIntervalString();
				_secondGroupSeats = new List<SeatViewModel>();
			}
		}

		private KeyValuePair<Guid, string> _rowName;
		public KeyValuePair<Guid, string> RowName
		{
			get { return _rowName.Key != Guid.Empty && _rowName.Value != "" ? _rowName : new KeyValuePair<Guid, string>(Guid.Empty, Properties.Resources.SeatCount); }
			set
			{
				if (Equals(_rowName, value)) return;
				RaisePropertyChanging("RowName");
				_rowName = value;
				RaisePropertyChanged("RowName");
			}
		}

		private IEnumerable<SeatViewModel> _secondGroupSeats;
		public IEnumerable<SeatViewModel> SecondGroupSeats
		{
			get { return _secondGroupSeats; }
			set
			{
				if (Equals(_secondGroupSeats, value)) return;
				RaisePropertyChanging("SecondGroupSeats");
				_secondGroupSeats = value;
				RaisePropertyChanged("SecondGroupSeats");
			}
		}

		private IEnumerable<SeatViewModel> _firstGroupSeats;
		public IEnumerable<SeatViewModel> FirstGroupSeats
		{
			get { return _firstGroupSeats; }
			set
			{
				if (Equals(_firstGroupSeats, value)) return;
				RaisePropertyChanging("FirstGroupSeats");
				_firstGroupSeats = value;
				RaisePropertyChanged("FirstGroupSeats");
			}
		}

		private string _firstGroupSeatNumbers;
		public string FirstGroupSeatNumbers
		{
			get { return _firstGroupSeatNumbers; }
			set
			{
				if (_firstGroupSeatNumbers == value) return;
				RaisePropertyChanging("FirstGroupSeatNumbers");
				_firstGroupSeatNumbers = value;
				if (_rowName.Value != null)
				{
					var seatNumbers = NumberHelper.FromIntervalToNumbers(_firstGroupSeatNumbers);
					FirstGroupSeats = seatNumbers.Select(seatNumber => SeatViewModels.FirstOrDefault(p => p.Name == seatNumber.ToString())).Where(seat => seat != null).ToList();
					SecondGroupSeats = SeatViewModels.Except(FirstGroupSeats);
					_secondGroupSeatNumbers = _secondGroupSeats.Select(p => p.Name).ToIntervalString();
					RaisePropertyChanged("SecondGroupSeatNumbers");
					_firstGroupSeatNumbers = _firstGroupSeats.Select(p => p.Name).ToIntervalString();
					RaisePropertyChanged("FirstGroupSeatNumbers");
				}
				else
				{
					int firstGroupSeatCount;
					if (int.TryParse(_firstGroupSeatNumbers, out firstGroupSeatCount))
					{
						FirstGroupSeats = SeatViewModels.Count() > firstGroupSeatCount
							? SeatViewModels.Take(firstGroupSeatCount)
							: SeatViewModels;
						SecondGroupSeats = SeatViewModels.Except(FirstGroupSeats);
						_secondGroupSeatNumbers = _secondGroupSeats.Count().ToString();
						RaisePropertyChanged("SecondGroupSeatNumbers");
						_firstGroupSeatNumbers = _firstGroupSeats.Count().ToString();
						RaisePropertyChanged("FirstGroupSeatNumbers");
					}
					else
					{
						SecondGroupSeats = SeatViewModels;
						FirstGroupSeats = SeatViewModels.Except(_secondGroupSeats);
						_secondGroupSeatNumbers = _secondGroupSeats.Count().ToString();
						RaisePropertyChanged("SecondGroupSeatNumbers");
						_firstGroupSeatNumbers = _firstGroupSeats.Count().ToString();
						RaisePropertyChanged("FirstGroupSeatNumbers");
					}
				}
			}
		}

		private string _secondGroupSeatNumbers;
		public string SecondGroupSeatNumbers
		{
			get { return _secondGroupSeatNumbers; }
			set
			{
				if (_secondGroupSeatNumbers == value) return;
				RaisePropertyChanging("SecondGroupSeatNumbers");
				_secondGroupSeatNumbers = value;
				if (_rowName.Value != null)
				{
					var seatNumbers = NumberHelper.FromIntervalToNumbers(_secondGroupSeatNumbers);
					SecondGroupSeats = seatNumbers.Select(seatNumber => SeatViewModels.FirstOrDefault(p => p.Name == seatNumber.ToString())).Where(seat => seat != null).ToList();
					FirstGroupSeats = SeatViewModels.Except(SecondGroupSeats);
					_firstGroupSeatNumbers = _firstGroupSeats.Select(p => p.Name).ToIntervalString();
					RaisePropertyChanged("FirstGroupSeatNumbers");
					_secondGroupSeatNumbers = _secondGroupSeats.Select(p => p.Name).ToIntervalString();
					RaisePropertyChanged("SecondGroupSeatNumbers");
				}
				else
				{
					int secondGroupSeatCount;
					if (int.TryParse(_secondGroupSeatNumbers, out secondGroupSeatCount))
					{
						SecondGroupSeats = SeatViewModels.Count() > secondGroupSeatCount
							? SeatViewModels.Take(secondGroupSeatCount)
							: SeatViewModels;
						FirstGroupSeats = SeatViewModels.Except(SecondGroupSeats);
						_firstGroupSeatNumbers = _firstGroupSeats.Count().ToString();
						RaisePropertyChanged("FirstGroupSeatNumbers");
						_secondGroupSeatNumbers = _secondGroupSeats.Count().ToString();
						RaisePropertyChanged("SecondGroupSeatNumbers");
					}
					else
					{
						FirstGroupSeats = SeatViewModels;
						SecondGroupSeats = SeatViewModels.Except(FirstGroupSeats);
						_firstGroupSeatNumbers = _firstGroupSeats.Count().ToString();
						RaisePropertyChanged("FirstGroupSeatNumbers");
						_secondGroupSeatNumbers = _secondGroupSeats.Count().ToString();
						RaisePropertyChanged("SecondGroupSeatNumbers");
					}
				}
			}
		}

		private IEnumerable<SeatViewModel> _seatViewModels;
		public IEnumerable<SeatViewModel> SeatViewModels
		{
			get { return _seatViewModels; }
			set
			{
				if (Equals(_seatViewModels, value)) return;
				RaisePropertyChanging("SeatViewModels");
				_seatViewModels = value;
				RaisePropertyChanged("SeatViewModels");
			}
		}

	}
}
