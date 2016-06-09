using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Styx.GromHSCR.DocumentParserBase.Models;
using Styx.GromHSCR.Helpers;

namespace Styx.GromHSCR.DocumentParserBase.Documents
{
	public class TxtParser
	{
		private string _filePath;

		public TxtParser(string filePath)
		{
			_filePath = filePath;
		}

		public ParseResult Parse()
		{
			try
			{
				var printInfo = new PrintInfo();
				var lines = File.ReadLines(_filePath).ToList();
				var addressLine = lines.FirstOrDefault(p => Regex.IsMatch(p, "адрес", RegexOptions.IgnoreCase));
				if (addressLine != null)
				{
					var splitlines = addressLine.Split(';');
					var addressString = "";
					var addressLines = "";
					for (var i = 1; i < splitlines.Count(); i++)
						{
							if (Regex.IsMatch(splitlines[i - 1], "адрес", RegexOptions.IgnoreCase))
							{
								addressLines = splitlines[i];
							}
						}
					
					if (!string.IsNullOrWhiteSpace(addressLines))
					{
						var splitLines3 = addressLines.Split(' ');
						for (var i = 1; i < splitLines3.Count(); i++)
						{
							if (Regex.IsMatch(splitLines3[i - 1], "адрес", RegexOptions.IgnoreCase))
							{
								addressString = splitLines3[i];
							}
							if (!string.IsNullOrWhiteSpace(addressString) && (i + 1 <= splitLines3.Count()) &&
								!Regex.IsMatch(splitLines3[i + 1], "тип", RegexOptions.IgnoreCase))
							{
								addressString += " " + splitLines3[i];
							}
							if (!string.IsNullOrWhiteSpace(addressString) && (i + 1 <= splitLines3.Count()) &&
								Regex.IsMatch(splitLines3[i + 1], "тип", RegexOptions.IgnoreCase))
							{
								addressString += " " + splitLines3[i];
								break;
							}
						}
					}
					if (string.IsNullOrWhiteSpace(addressString))
						throw new ArgumentNullException("Не найден адрес");
					printInfo.Address = AddressHelper.RecognizeAddress(addressString);
					int entry;
					var entryLine = lines.FirstOrDefault(p => Regex.IsMatch(p, "ВВОД", RegexOptions.IgnoreCase));
					if (entryLine != null)
					{
						var splitLines2 = entryLine.Split(' ');
						for (var i = 1; i < splitLines2.Count(); i++)
						{
							if (Regex.IsMatch(splitLines2[i - 1], "ВВОД", RegexOptions.IgnoreCase))
							{
								int.TryParse(splitLines2[i].Trim(), out entry);
							}
						}
					}

					var contractString = "";
					var counterTypeString = "";
					var counterVersionString = "";
					var counterModelString = "";
					var counterNumberString = "";
					var temperatureColdString = "";
					decimal g1max;
					decimal g2max;
					decimal g3max;
					decimal g1min;
					decimal g2min;
					decimal g3min;

					var contractLine = lines.FirstOrDefault(p => Regex.IsMatch(p, "договор", RegexOptions.IgnoreCase));
					if (contractLine != null)
					{
						var splitLines2 = contractLine.Split(';');
						for (var i = 1; i < splitLines2.Count(); i++)
						{
							if (Regex.IsMatch(splitLines2[i - 1], "договор", RegexOptions.IgnoreCase))
							{
								contractString = splitLines2[i].Replace("_", "").Trim();
							}
						}
					}
					var counterLine =
						lines.FirstOrDefault(
							p =>
								Regex.IsMatch(p, "тепловычислитель", RegexOptions.IgnoreCase) ||
								Regex.IsMatch(p, "теплосчетчик", RegexOptions.IgnoreCase));
					if (counterLine != null)
					{
						var splitLines3 = counterLine.Split(' ');
						for (var i = 1; i < splitLines3.Count(); i++)
						{
							if (Regex.IsMatch(splitLines3[i - 1], "тепловычислитель", RegexOptions.IgnoreCase) || Regex.IsMatch(splitLines3[i - 1], "теплосчетчик", RegexOptions.IgnoreCase))
							{
								counterTypeString = splitLines3[i];
							}
							if (!string.IsNullOrWhiteSpace(counterTypeString) && (i + 1 <= splitLines3.Count()) &&
								!Regex.IsMatch(splitLines3[i + 1], "пределы", RegexOptions.IgnoreCase))
							{
								counterTypeString += " " + splitLines3[i];
							}
							if (!string.IsNullOrWhiteSpace(counterTypeString) && (i + 1 <= splitLines3.Count()) &&
								Regex.IsMatch(splitLines3[i + 1], "пределы", RegexOptions.IgnoreCase))
							{
								counterTypeString += " " + splitLines3[i];
								break;
							}
						}
					}
					var temperatureColdLine =
						 lines.FirstOrDefault(
							 p =>
								 Regex.IsMatch(p, "тхв", RegexOptions.IgnoreCase));
					if (temperatureColdLine != null)
					{
						var splitLines4 = temperatureColdLine.Split('=');
						for (var i = 1; i < splitLines4.Count(); i++)
						{
							if (Regex.IsMatch(splitLines4[i - 1], "тхв", RegexOptions.IgnoreCase))
							{
								counterTypeString = splitLines4[i].Replace(" ", "").Replace("°C", "");
							}
						}
					}
					var G1MaxLine =
						 lines.FirstOrDefault(
							 p =>
								 Regex.IsMatch(p, "G под max", RegexOptions.IgnoreCase));
					if (G1MaxLine != null)
					{
						var splitLines4 = G1MaxLine.Split('=');
						for (var i = 1; i < splitLines4.Count(); i++)
						{
							if (Regex.IsMatch(splitLines4[i - 1], "G под max", RegexOptions.IgnoreCase))
							{
								decimal.TryParse(Regex.Match(splitLines4[i], @"\d", RegexOptions.IgnorePatternWhitespace).Value, out g1max);
							}
						}
					}
					var G1MinLine =
						 lines.FirstOrDefault(
							 p =>
								 Regex.IsMatch(p, "G под min", RegexOptions.IgnoreCase));
					if (G1MinLine != null)
					{
						var splitLines4 = G1MinLine.Split('=');
						for (var i = 1; i < splitLines4.Count(); i++)
						{
							if (Regex.IsMatch(splitLines4[i - 1], "G под min", RegexOptions.IgnoreCase))
							{
								decimal.TryParse(Regex.Match(splitLines4[i], @"\d", RegexOptions.IgnorePatternWhitespace).Value, out g1min);
							}
						}
					}
					var G2MaxLine =
						 lines.FirstOrDefault(
							 p =>
								 Regex.IsMatch(p, "G обр max", RegexOptions.IgnoreCase));
					if (G2MaxLine != null)
					{
						var splitLines4 = G2MaxLine.Split('=');
						for (var i = 1; i < splitLines4.Count(); i++)
						{
							if (Regex.IsMatch(splitLines4[i - 1], "G обр max", RegexOptions.IgnoreCase))
							{
								decimal.TryParse(Regex.Match(splitLines4[i], @"\d", RegexOptions.IgnorePatternWhitespace).Value, out g2max);
							}
						}
					}
					var G2MinLine =
						 lines.FirstOrDefault(
							 p =>
								 Regex.IsMatch(p, "G обр min", RegexOptions.IgnoreCase));
					if (G2MinLine != null)
					{
						var splitLines4 = G2MinLine.Split('=');
						for (var i = 1; i < splitLines4.Count(); i++)
						{
							if (Regex.IsMatch(splitLines4[i - 1], "G обр min", RegexOptions.IgnoreCase))
							{
								decimal.TryParse(Regex.Match(splitLines4[i], @"\d", RegexOptions.IgnorePatternWhitespace).Value, out g2min);
							}
						}
					}
					var G3MaxLine =
						 lines.FirstOrDefault(
							 p =>
								 Regex.IsMatch(p, "G3 max", RegexOptions.IgnoreCase));
					if (G3MaxLine != null)
					{
						var splitLines4 = G3MaxLine.Split('=');
						for (var i = 1; i < splitLines4.Count(); i++)
						{
							if (Regex.IsMatch(splitLines4[i - 1], "G3 max", RegexOptions.IgnoreCase))
							{
								decimal.TryParse(Regex.Match(splitLines4[i], @"\d", RegexOptions.IgnorePatternWhitespace).Value, out g3max);
							}
						}
					}
					var G3MinLine =
						 lines.FirstOrDefault(
							 p =>
								 Regex.IsMatch(p, "G3 min", RegexOptions.IgnoreCase));
					if (G3MinLine != null)
					{
						var splitLines4 = G3MinLine.Split('=');
						for (var i = 1; i < splitLines4.Count(); i++)
						{
							if (Regex.IsMatch(splitLines4[i - 1], "G3 min", RegexOptions.IgnoreCase))
							{
								decimal.TryParse(Regex.Match(splitLines4[i], @"\d", RegexOptions.IgnorePatternWhitespace).Value, out g3min);
							}
						}
					}


				}
				else
				{
					throw new ArgumentNullException("Пустая строка адреса");
				}




			}
			catch (Exception e)
			{
				return new ParseResult { IsOk = false, ErrorMessage = e.Message, PrintInfo = null };
			}
			return null;
		}

	}
}
