using System;
using System.Collections.Generic;
using System.IO;

namespace RedKassa.Promoter.ExcelBase.Models
{
	public class KassirRu : ExcelProviderBase
	{
		public KassirRu(Stream excelStream)
			: base(excelStream)
		{
		}

		public override string EndTableMarker
		{
			get { return "Итого"; }
		}

		public override IDictionary<string, IDictionary<string, string>> Tables
		{
			get
			{
				var tables = new Dictionary<string, IDictionary<string, string>>();

				var table1 = new Dictionary<string, string>
				{
					{"Id", "A21"},
					{"Sector", "E21"},
					{"Row", "G21"},
					{"SeatNumbers", "J21"},
					{"Count", "K21"},
					{"Price", "M21"},
					{"SumPrice", "O21"},
				};

				tables.Add("KassirRU", table1);

				return tables;
			}
		}

		public override IDictionary<string, string> Arguments
		{
			get
			{
				var ret = new Dictionary<string, string>
				{
					{"From", "I4"},
					{"To", "I5"},
					{"Event", "I9"},
					{"Date", "I15"},
					{"Time", "I17"},
					{"Place", "I19"},
				};

				return ret;

			}
		}

		public override void Fill()
		{
			base.Fill();

			var from = GetArgument<string>("from");
			var to = GetArgument<string>("To");
			var @event = GetArgument<string>("Event");
			var date = GetArgument<string>("Date");
			var time = GetArgument<string>("Time");
			var place = GetArgument<string>("Place");

			foreach (var table in Tables)
			{
				var rowsCount = GetTableRowsCount(table.Key);

				for (var i = 0; i < rowsCount; i++)
				{
					var id = GetTableArgument<string>(table.Key, i, "Id");
					var sector = GetTableArgument<string>(table.Key, i, "Sector");
					var row = GetTableArgument<string>(table.Key, i, "Row");
					var seatNumbers = GetTableArgument<string>(table.Key, i, "SeatNumbers");
					var count = GetTableArgument<string>(table.Key, i, "Count");
					var price = GetTableArgument<string>(table.Key, i, "Price");
					var sumPrice = GetTableArgument<string>(table.Key, i, "SumPrice");
				}
			}
		}
	}
}
