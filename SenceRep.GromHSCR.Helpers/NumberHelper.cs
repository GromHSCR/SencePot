using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text.RegularExpressions;

namespace SenceRep.GromHSCR.Helpers
{
	public static class NumberHelper
	{
		public static string ToIntervalString(this Dictionary<int, string> orderedSeats)
		{
			return ToIntervalString(orderedSeats.Select(p => new KeyValuePair<int, string>(p.Key, p.Value)).ToList());
		}

		public static string ToIntervalString(this IEnumerable<string> numbers)
		{
			var fakeSort = 0;

			return numbers.ToDictionary(p => ++fakeSort, p => p).ToIntervalString();
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

		private static readonly Regex FromIntervalToNumbersRegEx = new Regex(@"^\d+$", RegexOptions.Compiled);

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
				if (FromIntervalToNumbersRegEx.IsMatch(strFrom) && int.TryParse(strFrom, out intFrom))
				{
					int intTo;
					if (FromIntervalToNumbersRegEx.IsMatch(strTo) && int.TryParse(strTo, out intTo))
					{
						if (intTo < intFrom) return new List<int>();
						var query = from n in Enumerable.Range(intFrom, intTo - intFrom + 1)
									select n;
						numbers.AddRange(query);
					}
					else
					{
						return new List<int>();
					}
				}
				else
				{
					return new List<int>();
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
					return new List<int>();
				}
			}
			return numbers;
		}

		private static readonly Regex ToIntervalStringRegEx = new Regex(@"^\d+$", RegexOptions.Compiled);

		public static string ToIntervalString(this List<KeyValuePair<int, string>> numbers)
		{
			var seats = numbers.Select(x => x.Value).ToList();

			var numberSeats = new List<int>();
			var nameSeats = new List<string>();

			foreach (var seat in seats)
			{
				int num;
				if (ToIntervalStringRegEx.IsMatch(seat) && int.TryParse(seat, out num))
				{
					var number = num;
					numberSeats.Add(number);
				}
				else
				{
					nameSeats.Add(seat);
				}
			}

			nameSeats = nameSeats.SortNumberString().ToList();

			var numberStr = numberSeats.ToIntervalString();
			var simbolStr = string.Join(", ", nameSeats);

			return string.Format("{0}{1}{2}", numberStr, ((numberSeats.Count > 0 && nameSeats.Count > 0) ? ", " : string.Empty), simbolStr);
		}


		const string REG_EXPR = @"(?<=(\w*\s*))(\d+)"; // create regular expression with lookbehind 
		private static readonly Regex SortNumberStringRegEx = new Regex(REG_EXPR, RegexOptions.Compiled | RegexOptions.CultureInvariant | RegexOptions.Singleline | RegexOptions.IgnoreCase);
		public static IEnumerable<string> SortNumberString(this IEnumerable<string> source)
		{
			var sortedString = (from s in source
								let match = SortNumberStringRegEx.Match(s)
								orderby s
								orderby match.Success ? int.Parse(match.Groups[2].Value) : 0
								orderby match.Success descending
								orderby match.Groups[1].Value
								select s);
			return sortedString.ToList();
		}

		public static string ToIntervalString(this IEnumerable<int> numbers)
		{
			if (numbers == null) throw new ArgumentNullException("numbers");

			var numbersList = numbers.Distinct().OrderBy(p => p).ToList();

			var retStr = string.Empty;
			var oldIndex = int.MinValue;

			for (var index = 0; index < numbersList.Count; index++)
			{
				var number = numbersList[index];

				if (index >= 1)
				{
					var previousIndex = index - 1;
					var difference = number - numbersList[previousIndex];

					if (difference > 1 || index == numbersList.Count - 1)
					{
						var indexDifference = previousIndex - oldIndex;

						if (indexDifference == 0)
						{
							if (difference == 1)
								retStr += string.Format("-{0}", number);
							else
								retStr += string.Format(", {0}", number);
						}
						else if (difference > 1)
							retStr += string.Format("-{0}, {1}", numbersList[previousIndex], number);
						else
							retStr += string.Format("-{0}", number);

						oldIndex = index;
					}
					else if (difference < 1)
					{
						throw new Exception("Ошибка, два одинаковых числа");
					}
					continue;
				}
				retStr = number.ToString(CultureInfo.InvariantCulture);
				oldIndex = index;
			}

			return retStr;
		}
	}
}
