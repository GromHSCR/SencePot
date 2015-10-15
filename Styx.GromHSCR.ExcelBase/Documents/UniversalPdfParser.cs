using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using Styx.GromHSCR.DocumentParserBase.Models;
using Styx.GromHSCR.DocumentParserBase.Parser;

namespace Styx.GromHSCR.DocumentParserBase.Documents
{
	public class UniversalPdfParser : PdfProviderBase
	{
		public UniversalPdfParser(Stream stream)
			: base(stream)
		{
			var returnSeats = new List<Seat>();
			if (Sectors != null && Sectors.Count > 1)
			{
				for (var i = 1; i < Sectors.Count; i++)
				{
					var sector = Sectors[i];
					var row = Rows.SingleOrDefault(p => p.StartPosY == sector.StartPosY);
					var seatNumbers = SeatNumbers.SingleOrDefault(p => p.StartPosY == sector.StartPosY);
					var count = SeatCounts.SingleOrDefault(p => p.StartPosY == sector.StartPosY);
					var price = Prices.SingleOrDefault(p => p.StartPosY == sector.StartPosY);
					var sumPrice = SumPrices.SingleOrDefault(p => p.StartPosY == sector.StartPosY);
					var seatCount = 0;
					if (count != null) int.TryParse(count.Text, out seatCount);
					if (price != null)
					{
						var seatPrice = VoucherHelper.Price(price.Text);
						if (sumPrice != null)
						{
							var seatTotalPrice = VoucherHelper.TotalPrice(sumPrice.Text);
							if (!VoucherHelper.CheckTotalPrice(seatCount, seatPrice, seatTotalPrice))
							{
								MessageBox.Show("В накладной произведение цены: " + seatPrice + " и количества билетов: " + seatCount + " не равно сумме: " + seatTotalPrice + " (" + (i + 1) + " строка)", "Предупреждение");
							}
						}
						if (row == null || string.IsNullOrWhiteSpace(row.Text))
						{
							returnSeats.Add(
								new Seat
								{
									Sector = sector.FullText,
									Row = "withoutrow",
									SeatName = seatCount,
									SeatGroup = "",
									Price = seatPrice
								});
						}else
						if (seatNumbers != null && !string.IsNullOrEmpty(seatNumbers.Text))
						{
							var seatNumber = VoucherHelper.FromIntervalToNumbers(seatNumbers.Text);
							if (!seatNumber.Any()) continue;
							for (var j = 0; j < seatCount; j++)
							{
								if (row != null)
									returnSeats.Add(
										new Seat
										{
											Sector = sector.FullText,
											Row = row.Text,
											SeatName = seatNumber[j],
											SeatGroup = "",
											Price = seatPrice
										});
							}
						}
					}
				}
				var dateTime = DateTime.Now;
				ReturnEvent = new ReturnEvent
				{
					ReturnSeats = returnSeats,
					Agent = VoucherHelper.From(""),
					Event = VoucherHelper.Event(""),
					DateTime = dateTime
				};
			}
		}

		public ReturnEvent ReturnEvent { get; set; }
	}
}
