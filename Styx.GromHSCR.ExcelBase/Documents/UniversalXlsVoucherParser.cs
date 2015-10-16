using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using NPOI.SS.UserModel;
using Styx.GromHSCR.DocumentParserBase.Models;
using Styx.GromHSCR.DocumentParserBase.Parser;

namespace Styx.GromHSCR.DocumentParserBase.Documents
{
	public class UniversalXlsVoucherParser : ExcelXlsProviderBase
	{
		public UniversalXlsVoucherParser(Stream excelStream)
			: base(excelStream)
		{
		}


		public string FromValue { get; set; }
		public string EventDateTimeValue { get; set; }
		public string EventValue { get; set; }
		public string DateTimeValue { get; set; }
		public string DateValue { get; set; }
		public string TimeValue { get; set; }
		public string PlaceValue { get; set; }

	    public override IDictionary<string, IDictionary<string, string>> Tables
		{
			get
			{
				var tables = new Dictionary<string, IDictionary<string, string>>();

				var table1 = new Dictionary<string, string>();

                //if (!string.IsNullOrEmpty(_voucherTemplate.SectorAnchor))
                //{
                //    table1.Add("Sector", _voucherTemplate.SectorAnchor);
                //}
                //if (!string.IsNullOrEmpty(_voucherTemplate.RowAnchor))
                //{
                //    table1.Add("Row", _voucherTemplate.RowAnchor);
                //}
                //if (!string.IsNullOrEmpty(_voucherTemplate.SeatsFromAnchor) &&
                //    !string.IsNullOrEmpty(_voucherTemplate.SeatsToAnchor))
                //{
                //    if (_voucherTemplate.SeatsFromAnchor == _voucherTemplate.SeatsToAnchor)
                //    {
                //        table1.Add("SeatNumbers", _voucherTemplate.SeatsFromAnchor);
                //    }
                //    else
                //    {
                //        table1.Add("SeatFrom", _voucherTemplate.SeatsFromAnchor);
                //        table1.Add("SeatTo", _voucherTemplate.SeatsToAnchor);
                //    }
                //}
                //else if (!string.IsNullOrEmpty(_voucherTemplate.SeatsFromAnchor) &&
                //    string.IsNullOrEmpty(_voucherTemplate.SeatsToAnchor))
                //{
                //    table1.Add("SeatNumbers", _voucherTemplate.SeatsFromAnchor);
                //}
                //else if (string.IsNullOrEmpty(_voucherTemplate.SeatsFromAnchor) &&
                //   !string.IsNullOrEmpty(_voucherTemplate.SeatsToAnchor))
                //{
                //    table1.Add("SeatNumbers", _voucherTemplate.SeatsToAnchor);
                //}
                //if (!string.IsNullOrEmpty(_voucherTemplate.CountAnchor))
                //{
                //    table1.Add("Count", _voucherTemplate.CountAnchor);
                //}
                //if (!string.IsNullOrEmpty(_voucherTemplate.SectorAnchor))
                //{
                //    table1.Add("Price", _voucherTemplate.PriceAnchor);
                //}
                //if (!string.IsNullOrEmpty(_voucherTemplate.SumPriceAnchor))
                //{
                //    table1.Add("SumPrice", _voucherTemplate.SumPriceAnchor);
                //}

                //tables.Add(_voucherTemplate.Agent.Name, table1);

				return tables;
			}
		}

		public override IDictionary<string, string> Arguments
		{
			get
			{
				var ret = new Dictionary<string, string>();
                //if (!string.IsNullOrEmpty(_voucherTemplate.FromAnchor))
                //{
                //    ret.Add("From", _voucherTemplate.FromAnchor);
                //}
                //if (!string.IsNullOrEmpty(_voucherTemplate.EventAnchor) && !string.IsNullOrEmpty(_voucherTemplate.DateAnchor) && !string.IsNullOrEmpty(_voucherTemplate.TimeAnchor))
                //{
                //    if (_voucherTemplate.EventAnchor == _voucherTemplate.DateAnchor && _voucherTemplate.DateAnchor == _voucherTemplate.TimeAnchor)
                //    { ret.Add("EventDateTime", _voucherTemplate.EventAnchor); }
                //    else if (_voucherTemplate.EventAnchor != _voucherTemplate.DateAnchor && _voucherTemplate.DateAnchor == _voucherTemplate.TimeAnchor)
                //    {
                //        ret.Add("Event", _voucherTemplate.EventAnchor);
                //        ret.Add("DateTime", _voucherTemplate.DateAnchor);
                //    }
                //    else if (_voucherTemplate.EventAnchor == _voucherTemplate.DateAnchor && _voucherTemplate.DateAnchor != _voucherTemplate.TimeAnchor)
                //    {
                //        ret.Add("EventDate", _voucherTemplate.EventAnchor);
                //        ret.Add("Time", _voucherTemplate.TimeAnchor);
                //    }
                //    else
                //    {
                //        ret.Add("Event", _voucherTemplate.EventAnchor);
                //        ret.Add("Date", _voucherTemplate.DateAnchor);
                //        ret.Add("Time", _voucherTemplate.TimeAnchor);
                //    }
                //}
                //else if (string.IsNullOrEmpty(_voucherTemplate.EventAnchor) && !string.IsNullOrEmpty(_voucherTemplate.DateAnchor) && !string.IsNullOrEmpty(_voucherTemplate.TimeAnchor))
                //{
                //    if (_voucherTemplate.DateAnchor == _voucherTemplate.TimeAnchor)
                //    {
                //        ret.Add("DateTime", _voucherTemplate.DateAnchor);
                //    }
                //    else
                //    {
                //        ret.Add("Date", _voucherTemplate.DateAnchor);
                //        ret.Add("Time", _voucherTemplate.TimeAnchor);
                //    }
                //}
                //else if (!string.IsNullOrEmpty(_voucherTemplate.EventAnchor) && string.IsNullOrEmpty(_voucherTemplate.DateAnchor) && !string.IsNullOrEmpty(_voucherTemplate.TimeAnchor))
                //{
                //    if (_voucherTemplate.EventAnchor == _voucherTemplate.TimeAnchor)
                //    {
                //        ret.Add("EventTime", _voucherTemplate.EventAnchor);
                //    }
                //    else
                //    {
                //        ret.Add("Event", _voucherTemplate.EventAnchor);
                //        ret.Add("Time", _voucherTemplate.TimeAnchor);
                //    }
                //}
                //else if (!string.IsNullOrEmpty(_voucherTemplate.EventAnchor) && !string.IsNullOrEmpty(_voucherTemplate.DateAnchor) && string.IsNullOrEmpty(_voucherTemplate.TimeAnchor))
                //{
                //    if (_voucherTemplate.EventAnchor == _voucherTemplate.DateAnchor)
                //    {
                //        ret.Add("EventDate", _voucherTemplate.EventAnchor);
                //    }
                //    else
                //    {
                //        ret.Add("Event", _voucherTemplate.EventAnchor);
                //        ret.Add("Date", _voucherTemplate.DateAnchor);
                //    }
                //}
                //else
                //{
                //    if (!string.IsNullOrEmpty(_voucherTemplate.EventAnchor)) ret.Add("Event", _voucherTemplate.EventAnchor);
                //    if (!string.IsNullOrEmpty(_voucherTemplate.DateAnchor)) ret.Add("Date", _voucherTemplate.DateAnchor);
                //    if (!string.IsNullOrEmpty(_voucherTemplate.TimeAnchor)) ret.Add("Time", _voucherTemplate.TimeAnchor);
                //}
                //if (!string.IsNullOrEmpty(_voucherTemplate.PlaceAnchor))
                //{
                //    ret.Add("Place", _voucherTemplate.PlaceAnchor);
                //}

				return ret;

			}
		}

		public override List<ICell> Cells { get; set; }
		public override List<IRow> Rows { get; set; }

		public override void Fill()
		{
			try
			{
				base.Fill();

				if (Arguments.ContainsKey("From"))
				{
					FromValue = GetArgument<string>("From");
				}
				if (Arguments.ContainsKey("EventDateTime"))
				{
					EventDateTimeValue = GetArgument<string>("EventDateTime");
				}
				if (Arguments.ContainsKey("Event"))
				{
					EventValue = GetArgument<string>("Event");
				}
				if (Arguments.ContainsKey("DateTime"))
				{
					DateTimeValue = GetArgument<string>("DateTime");
				}
				if (Arguments.ContainsKey("Date"))
				{
					DateValue = GetArgument<string>("Date");
				}
				if (Arguments.ContainsKey("Time"))
				{
					TimeValue = GetArgument<string>("Time");
				}
				if (Arguments.ContainsKey("Place"))
				{
					PlaceValue = GetArgument<string>("Place");
				}
				foreach (var table in Tables)
				{
					var rowsCount = GetTableRowsCount(table.Key);

                    //for (var i = 0; i < rowsCount; i++)
                    //{
                    //    var sector = GetTableArgument<string>(table.Key, i, "Sector");
                    //    var row = GetTableArgument<string>(table.Key, i, "Row");
                    //    var seatNumbers = GetTableArgument<string>(table.Key, i, "SeatNumbers");
                    //    var seatGroup = GetTableArgument<string>(table.Key, i, "SeatGroup");
                    //    var seatFrom = GetTableArgument<string>(table.Key, i, "SeatFrom");
                    //    var seatTo = GetTableArgument<string>(table.Key, i, "SeatTo");
                    //    var count = GetTableArgument<string>(table.Key, i, "Count");
                    //    var price = GetTableArgument<string>(table.Key, i, "Price");
                    //    var sumPrice = GetTableArgument<string>(table.Key, i, "SumPrice");
                    //    int seatCount;
                    //    int.TryParse(count, out seatCount);
                    //    var seatPrice = VoucherHelper.Price(price);
                    //    var seatTotalPrice = VoucherHelper.TotalPrice(sumPrice);
                    //    if (!VoucherHelper.CheckTotalPrice(seatCount, seatPrice, seatTotalPrice))
                    //    {
                    //        MessageBox.Show("В накладной произведение цены: " + seatPrice + " и количества билетов: " + seatCount + " не равно сумме: " + seatTotalPrice + " (" + (i + 1) + " строка)", "Предупреждение");
                    //    }
                    //    if (string.IsNullOrWhiteSpace(row)
                    //        || (row != null && string.IsNullOrWhiteSpace(Regex.Match(row, @"[0-9]", RegexOptions.IgnoreCase).Value)) ||
                    //        ((string.IsNullOrWhiteSpace(seatNumbers) || (seatNumbers != null && string.IsNullOrWhiteSpace(Regex.Match(seatNumbers, @"[0-9]", RegexOptions.IgnoreCase).Value)))
                    //        && (string.IsNullOrWhiteSpace(seatFrom) || (seatFrom != null && string.IsNullOrWhiteSpace(Regex.Match(seatFrom, @"[0-9]", RegexOptions.IgnoreCase).Value)))
                    //        && (string.IsNullOrWhiteSpace(seatTo) || (seatTo != null && string.IsNullOrWhiteSpace(Regex.Match(seatTo, @"[0-9]", RegexOptions.IgnoreCase).Value)))))
                    //    {
                    //        returnSeats.Add(
                    //                new Seat
                    //                {
                    //                    Sector = sector,
                    //                    Row = "withoutrow",
                    //                    SeatName = seatCount,
                    //                    SeatGroup = "",
                    //                    Price = seatPrice
                    //                });
                    //    }
                    //    else if (!string.IsNullOrEmpty(seatNumbers))
                    //    {
                    //        var seatNumber = VoucherHelper.FromIntervalToNumbers(seatNumbers);
                    //        if (!seatNumber.Any()) continue;
                    //        for (var j = 0; j < seatCount; j++)
                    //        {
                    //            returnSeats.Add(
                    //                new Seat
                    //                {
                    //                    Sector = sector,
                    //                    Row = row,
                    //                    SeatName = seatNumber[j],
                    //                    SeatGroup = seatGroup,
                    //                    Price = seatPrice
                    //                });
                    //        }
                    //    }
                    //    else if (!string.IsNullOrEmpty(seatFrom) && !string.IsNullOrEmpty(seatTo))
                    //    {
                    //        var seatNumber = VoucherHelper.FromIntervalToNumbers(seatFrom, seatTo);
                    //        if (!seatNumber.Any()) continue;
                    //        for (var j = 0; j < seatCount; j++)
                    //        {
                    //            returnSeats.Add(
                    //                new Seat
                    //                {
                    //                    Sector = sector,
                    //                    Row = row,
                    //                    SeatName = seatNumber[j],
                    //                    SeatGroup = "",
                    //                    Price = seatPrice
                    //                });
                    //        }
                    //    }

                    //}
					var dateTime = DateTime.Now;
					if (!string.IsNullOrEmpty(DateTimeValue))
					{
						DateTime.TryParse(DateTimeValue, out dateTime);
					}
					else if (!string.IsNullOrEmpty(DateValue) && !string.IsNullOrEmpty(TimeValue))
					{
						DateTime.TryParse(DateValue + " " + TimeValue, out dateTime);
					}
					else if (!string.IsNullOrEmpty(EventDateTimeValue))
					{
						try
						{
							var args = EventDateTimeValue.Split(',');
							var dateplusTime = args[0];
							dateTime = VoucherHelper.DateTime(dateplusTime);
							EventValue = args[1];
						}
						catch (Exception)
						{
							EventValue = "";
						}

					}
                    //ReturnEvent = new ReturnEvent
                    //{
                    //    ReturnSeats = returnSeats,
                    //    Agent = VoucherHelper.From(FromValue),
                    //    Event = VoucherHelper.Event(EventValue),
                    //    DateTime = dateTime
                    //};
				}
			}
			catch (Exception e)
			{
				MessageBox.Show(e != null ? e.Message : "ошибка форматирования");
			}
		}
	}
}
