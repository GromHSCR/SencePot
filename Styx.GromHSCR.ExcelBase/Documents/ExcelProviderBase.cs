using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using OfficeOpenXml;
using Styx.GromHSCR.DocumentParserBase.Exceptions;
using Styx.GromHSCR.DocumentParserBase.Models;

namespace Styx.GromHSCR.DocumentParserBase.Documents
{
	public abstract class ExcelProviderBase
	{
		private readonly ExcelPackage _excelPackage;

		private readonly IDictionary<string, string> _parameters;

		private readonly IDictionary<string, List<IDictionary<string, string>>> _tablesParameters;
        
		private int _countColumn;

		private int _priceColumn;

		private int _sumPriceColumn;

		private int GetRow(string address)
		{
			var regex = new Regex("(?<Row>[0-9]+)");
			var match = regex.Match(address);

			var numStr = match.Groups["Row"].Value;

			int row;

			if (int.TryParse(numStr, out row))
				return row;

			return -1;
		}

		private T Cast<T>(object obj)
		{
			try
			{
				if (typeof(T) == typeof(Guid) && obj != null)
				{
					return (T)(object)new Guid(obj.ToString());
				}

				return (T)Convert.ChangeType(obj, typeof(T), CultureInfo.InvariantCulture);
			}
			catch (Exception exception)
			{
				throw new ParameterCastException(exception);
			}
		}

		private string GetCollumn(string address)
		{
			var regex = new Regex("(?<Collumn>[a-zA-Z]*)");
			var match = regex.Match(address);

			var col = match.Groups["Collumn"].Value;

			return col;
		}

		protected ExcelProviderBase(Stream excelStream)
		{
			if (excelStream == null) throw new ArgumentNullException("excelStream");
			_parameters = new Dictionary<string, string>();
			_tablesParameters = new Dictionary<string, List<IDictionary<string, string>>>();
			_excelPackage = new ExcelPackage();
			_excelPackage.Load(excelStream);
		}

		public abstract IDictionary<string, IDictionary<string, string>> Tables { get; }

		public abstract IDictionary<string, string> Arguments { get; }

		public ExcelWorksheet ExcelWorksheet { get; set; }

		protected virtual void FillArguments()
		{
			var worksheet = _excelPackage.Workbook.Worksheets.FirstOrDefault();

			if (worksheet == null) return;

			foreach (var argument in Arguments)
			{
				var arg = argument;
				if (arg.Key == "Date" || arg.Key == "DateTime")
				{
					var cell = worksheet.Cells.FirstOrDefault(p => Regex.IsMatch(p.Text, arg.Value, RegexOptions.IgnoreCase));
					if (cell == null || cell.Text == null)
					{ _parameters.Add(arg.Key, string.Empty); }
					else
					{
						DateTime date;
						var args = cell.Text.Split(' ');

						if (args.Any(p => DateTime.TryParse(p, out date)))
						{
							_parameters.Add(arg.Key, cell.Text);
						}
						else
						{
							var cellWithDate = ExcelWorksheet.Cells.FirstOrDefault(p => DateTime.TryParse(p.Text, out date));
							_parameters.Add(arg.Key, cellWithDate != null ? cellWithDate.Text : null);
						}
					}
				}
				else if (arg.Key == "Time")
				{
					var cell = worksheet.Cells.FirstOrDefault(p => Regex.IsMatch(p.Text, arg.Value, RegexOptions.IgnoreCase));
					if (cell == null || cell.Text == null) { _parameters.Add(arg.Key, string.Empty); }
					else
					{
						TimeSpan time;
						var args = cell.Text.Split(' ');
						if (args.Any(p => TimeSpan.TryParse(p, out time)))
						{
							_parameters.Add(arg.Key, cell.Text);
						}
						else
						{
							var cellWithDate = ExcelWorksheet.Cells.FirstOrDefault(p => TimeSpan.TryParse(p.Text, out time));
							_parameters.Add(arg.Key, cellWithDate != null ? cellWithDate.Text : null);
						}
					}
				}
				else if (arg.Key == "Place")
				{
					var cell = worksheet.Cells.FirstOrDefault(p => Regex.IsMatch(p.Text, arg.Value, RegexOptions.IgnoreCase));
					if (cell != null)
					{
						var cellUseful = worksheet.Cells[cell.Start.Row, cell.Start.Column + 1];

						_parameters.Add(arg.Key, cellUseful != null ? cellUseful.Text != null ? cellUseful.Text.Trim() : null : null);
					}
					else
					{
						_parameters.Add(arg.Key, null);
					}
				}
				else
				{
					var cell = worksheet.Cells.FirstOrDefault(p => Regex.IsMatch(p.Text, arg.Value, RegexOptions.IgnoreCase));
					if (!_parameters.ContainsKey(argument.Key))
						_parameters.Add(argument.Key, cell != null ? cell.Text != null ? cell.Text.Trim() : null : null);
					else if (_parameters[argument.Key] != null)
						_parameters[argument.Key] = cell != null ? cell.Text != null ? cell.Text.Trim() : null : null;
				}
			}
		}

		protected virtual int GetStartRow(ExcelWorksheet worksheet, KeyValuePair<string, IDictionary<string, string>> table)
		{
			if (worksheet == null) throw new ArgumentNullException("worksheet");

			var startRow = 1;
			var firstOrDefault = worksheet.Cells.FirstOrDefault(
				p => Regex.IsMatch(p.Text.Replace("\n", " ").Trim(), table.Value["Count"].Trim(), RegexOptions.IgnoreCase));
			if (firstOrDefault != null)
			{
				_countColumn =
					firstOrDefault.Start.Column;
				var excelRangeBase = worksheet.Cells.FirstOrDefault(
					p => Regex.IsMatch(p.Text.Replace("\n", " ").Trim(), table.Value["Price"].Trim(), RegexOptions.IgnoreCase));
				if (excelRangeBase != null)
				{
					_priceColumn = excelRangeBase.Start.Column;
					var orDefault = worksheet.Cells.FirstOrDefault(
						p => Regex.IsMatch(p.Text.Replace("\n", " ").Trim(), table.Value["SumPrice"].Trim(), RegexOptions.IgnoreCase));
					if (orDefault != null)
					{
						_sumPriceColumn = orDefault.Start.Column;
						if (firstOrDefault.Start.Row == excelRangeBase.Start.Row && excelRangeBase.Start.Row == orDefault.Start.Row)
							startRow = firstOrDefault.Start.Row + 1;
					}
				}
			}
			if (_countColumn == 0 || _priceColumn == 0 || _sumPriceColumn == 0)
				throw new ExcelFormatException("Не найдена таблица с местами");

			while (true)
			{
				int count;
				decimal sumPrice;
				decimal price;
				if ((!string.IsNullOrEmpty(worksheet.Cells[startRow, _countColumn].Text) &&
					int.TryParse(worksheet.Cells[startRow, _countColumn].Text.Trim(), NumberStyles.Any, CultureInfo.InvariantCulture, out count)) ||
					(!string.IsNullOrEmpty(worksheet.Cells[startRow, _priceColumn].Text) &&
					decimal.TryParse(worksheet.Cells[startRow, _priceColumn].Text.Replace(",", ".").Trim(), NumberStyles.Number, CultureInfo.InvariantCulture, out price)) ||
					(!string.IsNullOrEmpty(worksheet.Cells[startRow, _sumPriceColumn].Text) &&
					decimal.TryParse(Regex.Match(worksheet.Cells[startRow, _sumPriceColumn].Text.Replace(",", ".").Trim(), @"\d", RegexOptions.CultureInvariant).Value, NumberStyles.Number, CultureInfo.InvariantCulture, out sumPrice))) return startRow;
				startRow++;
			}
		}

		protected virtual int GetEndRow(ExcelWorksheet worksheet, KeyValuePair<string, IDictionary<string, string>> table)
		{
			if (worksheet == null) throw new ArgumentNullException("worksheet");

			var startRow = GetStartRow(worksheet, table);

			var endIndex = startRow;

			while (true)
			{
				//if (string.IsNullOrEmpty(worksheet.Cells[endIndex, _countColumn].Text) ||
				//	!int.TryParse(worksheet.Cells[endIndex, _countColumn].Text, NumberStyles.Any, CultureInfo.InvariantCulture, out count) ||
				//	string.IsNullOrEmpty(worksheet.Cells[endIndex, _priceColumn].Text) ||
				//	!decimal.TryParse(worksheet.Cells[endIndex, _priceColumn].Text.Replace(",", "."), NumberStyles.Number, CultureInfo.InvariantCulture, out price) ||
				//	string.IsNullOrEmpty(worksheet.Cells[endIndex, _sumPriceColumn].Text) ||
				//	!decimal.TryParse(Regex.Match(worksheet.Cells[endIndex, _sumPriceColumn].Text.Replace(",", "."), @"\d", RegexOptions.CultureInvariant).Value, NumberStyles.Number, CultureInfo.InvariantCulture, out sumPrice)) return endIndex - 1;
				var countText = worksheet.Cells[endIndex, _countColumn].Text;
				var priceText = worksheet.Cells[endIndex, _priceColumn].Text;
				var sumPriceText = worksheet.Cells[endIndex, _sumPriceColumn].Text;

				if (string.IsNullOrEmpty(countText) ||
					Regex.Match(countText, @"\d", RegexOptions.CultureInvariant).Value.Length < 1 ||
					string.IsNullOrEmpty(priceText) ||
					(Regex.Match(priceText, @"\d", RegexOptions.CultureInvariant).Value.Length < 1 && !Regex.IsMatch(sumPriceText, "приглашение", RegexOptions.IgnoreCase)) ||
					string.IsNullOrEmpty(sumPriceText) ||
					Regex.Match(sumPriceText, @"\d", RegexOptions.CultureInvariant).Value.Length < 1)
				{
					var end = true;
					for (var i = 0; i < 9; i++)
					{
						var newCountCell = worksheet.Cells[endIndex + i, _countColumn];
						var newPriceCell = worksheet.Cells[endIndex + i, _priceColumn];
						var newSumPriceCell = worksheet.Cells[endIndex + i, _sumPriceColumn];
						if (newCountCell == null || newPriceCell == null || newSumPriceCell == null)
							return endIndex - 1;
						var newCountText = newCountCell.Text;
						var newPriceText = newPriceCell.Text;
						var newSumPriceText = newSumPriceCell.Text;

						if (!(string.IsNullOrEmpty(newCountText) ||
							  Regex.Match(newCountText, @"\d", RegexOptions.CultureInvariant).Value.Length < 1 ||
							  string.IsNullOrEmpty(newPriceText) ||
							  (Regex.Match(newPriceText, @"\d", RegexOptions.CultureInvariant).Value.Length < 1 &&
							   !Regex.IsMatch(newSumPriceText, "приглашение", RegexOptions.IgnoreCase)) ||
							  string.IsNullOrEmpty(newSumPriceText) ||
							  Regex.Match(newSumPriceText, @"\d", RegexOptions.CultureInvariant).Value.Length < 1))
							end = false;
					}
					if (end)
						return endIndex - 1;
				}
				endIndex++;
			}
		}


		protected virtual void FillTablesArguments()
		{
			var worksheet = _excelPackage.Workbook.Worksheets.First();

			foreach (var table in Tables)
			{

				List<IDictionary<string, string>> rows;

				if (_tablesParameters.ContainsKey(table.Key))
				{
					rows = _tablesParameters[table.Key];
				}
				else
				{
					rows = new List<IDictionary<string, string>>();
					_tablesParameters.Add(table.Key, rows);
				}


				var endRow = GetEndRow(worksheet, table);

				var rowIndex = GetStartRow(worksheet, table);

				var startRow = rowIndex - 2;

				do
				{
					var countText = worksheet.Cells[rowIndex, _countColumn].Text;
					var priceText = worksheet.Cells[rowIndex, _priceColumn].Text;
					var sumPriceText = worksheet.Cells[rowIndex, _sumPriceColumn].Text;

					if (string.IsNullOrEmpty(countText) ||
						Regex.Match(countText, @"\d", RegexOptions.CultureInvariant).Value.Length < 1 ||
						string.IsNullOrEmpty(priceText) ||
						(Regex.Match(priceText, @"\d", RegexOptions.CultureInvariant).Value.Length < 1 &&
						 !Regex.IsMatch(sumPriceText, "приглашение", RegexOptions.IgnoreCase)) ||
						string.IsNullOrEmpty(sumPriceText) ||
						Regex.Match(sumPriceText, @"\d", RegexOptions.CultureInvariant).Value.Length < 1)
					{
						rowIndex++;
						continue;
					}

					var row = new Dictionary<string, string>();

					foreach (var properties in table.Value)
					{
						var columnHolder = worksheet.Cells[startRow, worksheet.Cells.Start.Column, endRow, worksheet.Cells.End.Column].FirstOrDefault(
							p => p.Text.Replace("\n", " ").Trim().ToUpper() == properties.Value.Trim().ToUpper());
						if (columnHolder == null)
						{
							columnHolder = worksheet.Cells[startRow, worksheet.Cells.Start.Column, endRow, worksheet.Cells.End.Column]
								.FirstOrDefault(
									p => Regex.IsMatch(p.Text.Replace("\n", " ").Trim(), properties.Value.Trim(), RegexOptions.IgnoreCase));
							if (columnHolder == null)
							{
								throw new ExcelFormatException("Колонка с именем " + properties.Value + " не найдена");
							}
						}
						var cell = worksheet.Cells[rowIndex, columnHolder.Start.Column].FirstOrDefault();
						row.Add(properties.Key, cell != null ? cell.Text != null ? cell.Text.Trim() : null : null);
					}

					rows.Add(row);
					rowIndex++;

				} while (rowIndex <= endRow);
			}
		}

		public virtual void Fill()
		{
			if (!_excelPackage.Workbook.Worksheets.Any())
				throw new WorksheetsNotFoundException();

			ExcelWorksheet = _excelPackage.Workbook.Worksheets.FirstOrDefault();
			FillArguments();
			FillTablesArguments();


		}

		protected int GetTableRowsCount(string tableName)
		{
			if (tableName == null) throw new ArgumentNullException("tableName");

			if (!_tablesParameters.ContainsKey(tableName))
				throw new TableNotInitializedException(string.Format("Table {0} is not initialized or not initialized correctly", tableName));

			return _tablesParameters[tableName].Count;
		}

		protected T GetTableArgument<T>(string tableName, int row, string parameterName)
		{
			if (tableName == null) throw new ArgumentNullException("tableName");

			var value = GetTableArgument(tableName, row, parameterName);

			return Cast<T>(value);
		}

		protected object GetTableArgument(string tableName, int row, string parameterName)
		{
			if (tableName == null) throw new ArgumentNullException("tableName");

			if (!_tablesParameters.ContainsKey(tableName))
				throw new TableNotInitializedException(string.Format("Table {0} is not initialized or not initialized correctly", tableName));

			var rowCount = GetTableRowsCount(tableName);

			if (row < 0 || row > rowCount - 1)
				throw new ArgumentOutOfRangeException("row");

			if (!_tablesParameters[tableName][row].ContainsKey(parameterName))
				return null;
			//throw new ParameterNotFoundException(parameterName);

			return _tablesParameters[tableName][row][parameterName];
		}

		protected object GetArgument(string parameterName)
		{
			if (parameterName == null) throw new ArgumentNullException("parameterName");
			if (_parameters == null)
				throw new ParameterNotInitializedException(
					string.Format("Parameter {0} is not initialized or not initialized correctly", parameterName));

			if (!_parameters.ContainsKey(parameterName))
				throw new ParameterNotFoundException(parameterName);

			return _parameters[parameterName];
		}

		protected T GetArgument<T>(string parameterName)
		{
			if (parameterName == null) throw new ArgumentNullException("parameterName");

			if (_parameters == null) throw new ParameterNotInitializedException(string.Format("Parameter {0} is not initialized or not initialized correctly", parameterName));

			var value = GetArgument(parameterName);

			return Cast<T>(value);
		}
	}
}
