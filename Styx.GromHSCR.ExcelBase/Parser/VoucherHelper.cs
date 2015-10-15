using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text.RegularExpressions;

namespace Styx.GromHSCR.DocumentParserBase.Parser
{
	public static class VoucherHelper
	{
		static VoucherHelper()
		{

		}

		public static DateTime DateTime(string dateTime)
		{
			if (string.IsNullOrEmpty(dateTime)) return System.DateTime.Now;
			var dateTimes = dateTime.Split(' ');
			var date = new DateTime();
			var time = new TimeSpan();
			var isDateAssigned = false;
			foreach (var t in dateTimes)
			{
				if (!isDateAssigned)
				{
					if (System.DateTime.TryParse(t, out date))
					{
						isDateAssigned = true;
					}
				}
				else
				{
					if (!TimeSpan.TryParse(t, out time)) continue;
					break;
				}
			}
			var eventDateTime = date.Add(time);
			return eventDateTime;
		}

		public static string Event(string eventName)
		{
			if (string.IsNullOrEmpty(eventName)) return "";
			var eventNames = eventName.Split(':');
			return eventNames.Count() > 1 ? eventNames.Last() : eventNames.First();
		}

		public static string To(string to)
		{
			if (string.IsNullOrEmpty(to)) return "";
			var tos = to.Split(':');
			return tos.Count() > 1 ? tos.Last() : tos.First();
		}

		public static string From(string from)
		{
			if (string.IsNullOrEmpty(from)) return "";
			var froms = from.Split(':');
			return froms.Count() > 1 ? froms.Last() : froms.First();
		}

		public static decimal Price(string price)
		{
			if (string.IsNullOrEmpty(price)) return 0;
			decimal priceDecimal;
			if (decimal.TryParse(price, NumberStyles.Currency, CultureInfo.CurrentCulture.NumberFormat, out priceDecimal))
			{
				return priceDecimal;
			}
			if (decimal.TryParse(price, NumberStyles.Currency, CultureInfo.InvariantCulture.NumberFormat, out priceDecimal))
			{
				return priceDecimal;
			}
			return decimal.TryParse(price.Substring(0, price.Length - 1), NumberStyles.Currency, CultureInfo.CurrentCulture.NumberFormat, out priceDecimal) ? priceDecimal : 0;
		}

		public static decimal TotalPrice(string totalPrice)
		{
			if (string.IsNullOrEmpty(totalPrice)) return 0;
			decimal totalPriceDecimal;
			if (decimal.TryParse(totalPrice, NumberStyles.Currency, CultureInfo.CurrentCulture.NumberFormat, out totalPriceDecimal))
			{
				return totalPriceDecimal;
			}
			if (decimal.TryParse(totalPrice, NumberStyles.Currency, CultureInfo.InvariantCulture.NumberFormat, out totalPriceDecimal))
			{
				return totalPriceDecimal;
			}
			return decimal.TryParse(totalPrice.Substring(0, totalPrice.Length - 1), NumberStyles.Currency, CultureInfo.CurrentCulture.NumberFormat, out totalPriceDecimal) ? totalPriceDecimal : 0;
		}

		public static bool CheckTotalPrice(int count, decimal price, decimal totalPrice)
		{
			return count * price == totalPrice;
		}

		public static List<int> FromIntervalToNumbers(string intervals)
		{
			if (String.IsNullOrWhiteSpace(intervals))
			{
				return new List<int>();
			}
			var intervalsStr = new List<string>();
			if (intervals.Contains(","))
			{
				intervalsStr.AddRange(intervals.Split(','));
			}
			else if (intervals.Contains(";"))
			{
				intervalsStr.AddRange(intervals.Split(';'));
			}
			else
			{
				intervalsStr.Add(intervals);
			}
			var numbers = new List<int>();
			foreach (var interStr in intervalsStr)
			{
				var strFromAndTo = interStr.Split('-');
				if (strFromAndTo[0] == strFromAndTo[strFromAndTo.Length - 1])
				{
					strFromAndTo = strFromAndTo[0].Split('—');
				}
				string strFrom = strFromAndTo[0].Trim();
				string strTo = strFromAndTo[strFromAndTo.Length - 1].Trim();
				numbers.AddRange(FromIntervalToNumbers(strFrom, strTo));
			}

			return numbers;
		}

		public static List<int> FromIntervalToNumbers(string intervalFrom, string intervalTo)
		{
			if (string.IsNullOrWhiteSpace(intervalFrom) && string.IsNullOrWhiteSpace(intervalTo))
			{
				return new List<int>();
			}
			if (string.IsNullOrWhiteSpace(intervalFrom))
			{
				return new List<int>() { int.Parse(intervalTo) };
			}
			var numbers = new List<int>();
			var strFrom = intervalFrom;
			var strTo = intervalTo;
			int intFrom;
			if (strTo != "")
			{
				var strFromWithRegExp = FromIntervalToNumbersRegEx.Match(strFrom).Value;
				var strToWithRegExp = FromIntervalToNumbersRegEx.Match(strTo).Value;
				if (!string.IsNullOrWhiteSpace(strFromWithRegExp) && int.TryParse(strFromWithRegExp, out intFrom))
				{
					int intTo;
					if (!string.IsNullOrWhiteSpace(strToWithRegExp) && int.TryParse(strToWithRegExp, out intTo))
					{
						var query = from n in Enumerable.Range(intFrom, intTo - intFrom + 1)
									select n;
						numbers.AddRange(query);
					}
					else
					{
						throw new ArgumentException("Не найдены номера мест, либо они имеют неправельный формат");
					}
				}
				else
				{
						throw new ArgumentException("Не найдены номера мест, либо они имеют неправельный формат");
				}

			}
			else
			{
				if (FromIntervalToNumbersRegEx.IsMatch(strFrom) && int.TryParse(strFrom, out intFrom))
				{
					numbers.Add(intFrom);
				}
				else
				{
					throw new ArgumentException("Не найдены номера мест, либо они имеют неправельный формат");
				}
			}
			return numbers;
		}

		private static readonly Regex FromIntervalToNumbersRegEx = new Regex(@"^\d+$", RegexOptions.Compiled);
	}
}
