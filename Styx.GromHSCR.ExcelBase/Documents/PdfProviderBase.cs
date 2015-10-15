using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using iTextSharp.text.pdf;
using Styx.GromHSCR.DocumentParserBase.Models;

namespace Styx.GromHSCR.DocumentParserBase.Documents
{
	public abstract class PdfProviderBase
	{
		protected PdfProviderBase(Stream stream)
		{
			Stream = stream;
			Read(stream);
		}
		public void Read(Stream stream)
		{
			var pdfReader = new PdfReader(stream);

			for (int i = 0; i < pdfReader.NumberOfPages; i++)
			{
				var pagecontent = pdfReader.GetPageContent(i + 1);
				var textFromPage = Encoding.UTF8.GetString(Encoding.Convert(Encoding.Default, Encoding.UTF8, pagecontent));
				GetPdfItems(textFromPage);
				GetColumnValues();
				if (SeatNumbers == null || SeatNumbers.Any()) continue;
				if (PlaceFroms != null && PlaceTos != null && PlaceTos.Count != PlaceFroms.Count) continue;
				var pdfItems = (from placeFrom in PlaceFroms
								let placeTo = PlaceFroms.SingleOrDefault(p => p.StartPosY == placeFrom.StartPosY)
								where placeTo != null
								select new PdfItem
								{
									StartPosX = placeFrom.StartPosX,
									StartPosY = placeFrom.StartPosY,
									FontSize = placeFrom.FontSize,
									Text = placeFrom.Text + "-" + placeTo.Text
								}).ToList();
				SeatNumbers = pdfItems;
			}
		}

		static byte[] GetBytes(string str)
		{
			var bytes = new byte[str.Length * sizeof(char)];
			System.Buffer.BlockCopy(str.ToCharArray(), 0, bytes, 0, bytes.Length);
			return bytes;
		}

		public List<PdfItem> Sectors { get; set; }

		public List<PdfItem> Rows { get; set; }

		public List<PdfItem> SeatNumbers { get; set; }

		public List<PdfItem> PlaceFroms { get; set; }

		public List<PdfItem> PlaceTos { get; set; }

		public List<PdfItem> SeatCounts { get; set; }

		public List<PdfItem> Prices { get; set; }

		public List<PdfItem> SumPrices { get; set; }

		public Stream Stream { get; set; }

		public List<PdfItem> PdfItems { get; set; }

		public void GetPdfItems(string textFromPage)
		{
			var dataStrings = Regex.Split(textFromPage, "BT");
			var pdfItems = new List<PdfItem>();
			for (var i = 0; i < dataStrings.Count(); i++)
			{
				var dataString = dataStrings[i];
				var pdfItem = new PdfItem();
				var linesOfDataString = dataString.Split(new[] { "\n" }, StringSplitOptions.None).Where(p => !string.IsNullOrWhiteSpace(p)).ToList();
				for (var j = 0; j < linesOfDataString.Count(); j++)
				{
					var lineDataString = linesOfDataString[j];
					if (j == 0)
					{
						decimal posx;
						decimal posy;
						var lineValues = lineDataString
							.Split(' ');
						var k = 0;
						k = lineValues.Count() == 7 ? 4 : 0;
						if ((!decimal.TryParse(lineValues[k], NumberStyles.Number, CultureInfo.InvariantCulture, out posx) &&
							 !decimal.TryParse(lineValues[k], NumberStyles.Number, CultureInfo.CurrentCulture, out posx)) ||
							(!decimal.TryParse(lineValues[k + 1], NumberStyles.Number, CultureInfo.InvariantCulture, out posy) &&
							 !decimal.TryParse(lineValues[k + 1], NumberStyles.Number, CultureInfo.CurrentCulture, out posy)))
						{
							continue;
						}

						pdfItem.StartPosX = posx;
						pdfItem.StartPosY = posy;
					}
					else if (lineDataString.Contains("/F"))
					{
						if (pdfItem.FontSize == 0)
						{
							int fontSize;
							var lineValues = lineDataString.Split(' ');
							if (!int.TryParse(lineValues[1], out fontSize))
							{
								continue;
							}
							pdfItem.FontSize = fontSize;
						}
					}
					else if (Regex.IsMatch(lineDataString, "tj", RegexOptions.IgnoreCase))
					{
						if (!string.IsNullOrWhiteSpace(pdfItem.Text))
						{
							if (!string.IsNullOrWhiteSpace(lineDataString))
								pdfItem.AdditionalText += " " + lineDataString.TrimEnd('\r')
									.TrimEnd('j')
									.TrimEnd('J')
									.TrimEnd('T')
									.TrimEnd('t')
									.TrimEnd(')')
									.TrimStart('(');
						}
						else
						{
							var text = lineDataString.TrimEnd('\r')
								.TrimEnd('j')
								.TrimEnd('J')
								.TrimEnd('T')
								.TrimEnd('t');
							pdfItem.Text = text.TrimStart('(')
								.TrimEnd(')');
						}
					}
				}
				if (pdfItem.Text != null)
					pdfItems.Add(pdfItem);
			}
			PdfItems = pdfItems;
		}

		public void DecodeStrangePdfItems()
		{
			PdfItems.ForEach(p =>
			{
				p.Text = p.Text
					.Replace('A', 'А')
					.Replace('B', 'В')
					.Replace('C', 'Г')
					.Replace('E', 'И')
					.Replace('F', 'К')
					.Replace('G', 'М')
					.Replace('H', 'Н')
					.Replace('I', 'О')
					.Replace('J', 'П')
					.Replace('K', 'Р')
					.Replace('L', 'С')
					.Replace('M', 'У')
					.Replace('P', 'а')
					.Replace('Q', 'б')
					.Replace('R', 'в')
					.Replace('S', 'г')
					.Replace('T', 'д')
					.Replace('U', 'е')
					.Replace('V', 'з')
					.Replace('W', 'и')
					.Replace('X', 'к')
					.Replace('Y', 'л')
					.Replace('[', 'н')
					.Replace(']', 'о')
					.Replace('_', 'р')
					.Replace('`', 'с')
					.Replace('a', 'т')
					.Replace('b', 'у')
					.Replace('d', 'ч')
					.Replace('e', 'ы')
					.Replace('h', 'я');
			});
		}

		public void GetColumnValues()
		{
            //var sectorColumn = PdfItems.SingleOrDefault(p => string.Equals(p.Text, VoucherTemplate.SectorAnchor.Trim(), StringComparison.CurrentCultureIgnoreCase));
            ////if (sectorColumn == null)
            ////{
            ////	DecodeStrangePdfItems();
            ////	sectorColumn = PdfItems.SingleOrDefault(p => string.Equals(p.Text, VoucherTemplate.SectorAnchor.Trim(), StringComparison.CurrentCultureIgnoreCase));
            ////}
            //if (sectorColumn != null)
            //{
            //    var sectors =
            //        PdfItems.Where(
            //            p =>
            //                sectorColumn.CenterPosX - p.StringWidthStep <= p.CenterPosX &&
            //                p.CenterPosX <= sectorColumn.CenterPosX + p.StringWidthStep).ToList();
            //    Sectors = sectors.Where(p => PdfItems.Count(x => x.StartPosY == p.StartPosY) > 3).ToList();
            //}
            //var rowColumn = PdfItems.SingleOrDefault(p => string.Equals(p.Text, VoucherTemplate.RowAnchor.Trim(), StringComparison.CurrentCultureIgnoreCase));
            //if (rowColumn != null)
            //{
            //    var rows =
            //        PdfItems.Where(p => rowColumn.CenterPosX - p.StringWidthStep <= p.CenterPosX && p.CenterPosX <= rowColumn.CenterPosX + p.StringWidthStep).ToList();
            //    Rows = rows.Where(p => PdfItems.Count(x => x.StartPosY == p.StartPosY) > 3).ToList();
            //}
            //var countColumn = PdfItems.SingleOrDefault(p => string.Equals(p.Text, VoucherTemplate.CountAnchor.Trim(), StringComparison.CurrentCultureIgnoreCase));
            //if (countColumn != null)
            //{
            //    var counts =
            //        PdfItems.Where(p => countColumn.CenterPosX - p.StringWidthStep <= p.CenterPosX && p.CenterPosX <= countColumn.CenterPosX + p.StringWidthStep).ToList();
            //    SeatCounts = counts.Where(p => PdfItems.Count(x => x.StartPosY == p.StartPosY) > 3).ToList();
            //}
            //var priceColumn = PdfItems.SingleOrDefault(p => string.Equals(p.Text, VoucherTemplate.PriceAnchor.Trim(), StringComparison.CurrentCultureIgnoreCase));
            //if (priceColumn != null)
            //{
            //    var prices =
            //        PdfItems.Where(p => priceColumn.CenterPosX - p.StringWidthStep <= p.CenterPosX && p.CenterPosX <= priceColumn.CenterPosX + p.StringWidthStep).ToList();
            //    Prices = prices.Where(p => PdfItems.Count(x => x.StartPosY == p.StartPosY) > 3).ToList();
            //}
            //var sumPriceColumn = PdfItems.SingleOrDefault(p => string.Equals(p.Text, VoucherTemplate.SumPriceAnchor.Trim(), StringComparison.CurrentCultureIgnoreCase));
            //if (sumPriceColumn != null)
            //{
            //    var sumPrices =
            //        PdfItems.Where(p => sumPriceColumn.CenterPosX - p.StringWidthStep <= p.CenterPosX && p.CenterPosX <= sumPriceColumn.CenterPosX + p.StringWidthStep).ToList();
            //    SumPrices = sumPrices.Where(p => PdfItems.Count(x => x.StartPosY == p.StartPosY) > 3).ToList();
            //}
            //if ((VoucherTemplate.SeatsFromAnchor.Trim() == VoucherTemplate.SeatsToAnchor.Trim() && !string.IsNullOrWhiteSpace(VoucherTemplate.SeatsToAnchor)) || string.IsNullOrWhiteSpace(VoucherTemplate.SeatsToAnchor.Trim()))
            //{
            //    var placesColumn = PdfItems.SingleOrDefault(p => string.Equals(p.Text, VoucherTemplate.SeatsFromAnchor.Trim(), StringComparison.CurrentCultureIgnoreCase)) ??
            //                       PdfItems.SingleOrDefault(p => string.Equals(p.Text, VoucherTemplate.SeatsFromAnchor.Trim(), StringComparison.InvariantCultureIgnoreCase));
            //    if (placesColumn == null) return;
            //    var places =
            //        PdfItems.Where(p => placesColumn.CenterPosX - p.StringWidthStep <= p.CenterPosX && p.CenterPosX <= placesColumn.CenterPosX + p.StringWidthStep).ToList();
            //    SeatNumbers = places.Where(p => PdfItems.Count(x => x.StartPosY == p.StartPosY) > 3).ToList();
            //}
            //else if (string.IsNullOrWhiteSpace(VoucherTemplate.SeatsFromAnchor.Trim()))
            //{
            //    var placesColumn = PdfItems.SingleOrDefault(p => string.Equals(p.Text, VoucherTemplate.SeatsToAnchor.Trim(), StringComparison.CurrentCultureIgnoreCase)) ??
            //                       PdfItems.SingleOrDefault(p => string.Equals(p.Text, VoucherTemplate.SeatsToAnchor.Trim(), StringComparison.InvariantCultureIgnoreCase));
            //    if (placesColumn == null) return;
            //    var places =
            //        PdfItems.Where(p => placesColumn.CenterPosX - p.StringWidthStep <= p.CenterPosX && p.CenterPosX <= placesColumn.CenterPosX + p.StringWidthStep).ToList();
            //    SeatNumbers = places.Where(p => PdfItems.Count(x => x.StartPosY == p.StartPosY) > 3).ToList();
            //}
            //else
            //{
            //    var placesFromColumn = PdfItems.SingleOrDefault(p => string.Equals(p.Text, VoucherTemplate.SeatsFromAnchor.Trim(), StringComparison.CurrentCultureIgnoreCase)) ??
            //                       PdfItems.SingleOrDefault(p => string.Equals(p.Text, VoucherTemplate.SeatsFromAnchor.Trim(), StringComparison.InvariantCultureIgnoreCase));
            //    if (placesFromColumn == null) return;
            //    var placesFrom =
            //        PdfItems.Where(p => placesFromColumn.CenterPosX - p.StringWidthStep <= p.CenterPosX && p.CenterPosX <= placesFromColumn.CenterPosX + p.StringWidthStep).ToList();
            //    PlaceFroms = placesFrom.Where(p => PdfItems.Count(x => x.StartPosY == p.StartPosY) > 3).ToList();
            //    var placesToColumn = PdfItems.SingleOrDefault(p => string.Equals(p.Text, VoucherTemplate.SeatsToAnchor.Trim(), StringComparison.CurrentCultureIgnoreCase)) ??
            //                       PdfItems.SingleOrDefault(p => string.Equals(p.Text, VoucherTemplate.SeatsToAnchor.Trim(), StringComparison.InvariantCultureIgnoreCase));
            //    if (placesToColumn == null) return;
            //    var placesTo =
            //        PdfItems.Where(p => placesToColumn.CenterPosX - p.StringWidthStep <= p.CenterPosX && p.CenterPosX <= placesToColumn.CenterPosX + p.StringWidthStep).ToList();
            //    PlaceTos = placesTo.Where(p => PdfItems.Count(x => x.StartPosY == p.StartPosY) > 3).ToList();
            //}
		}
	}
}
