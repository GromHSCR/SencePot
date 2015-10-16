using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using Styx.GromHSCR.DocumentParserBase.Exceptions;
using Styx.GromHSCR.DocumentParserBase.Models;

namespace Styx.GromHSCR.DocumentParserBase.Documents
{
	public abstract class ExcelXlsProviderBase
	{
		private readonly HSSFWorkbook _excelWorkbook;

		private readonly IDictionary<string, string> _parameters;

		private readonly IDictionary<string, List<IDictionary<string, string>>> _tablesParameters;


		private int _countColumn;

		private int _priceColumn;

		private int _sumPriceColumn;


		protected ExcelXlsProviderBase(Stream excelStream)
		{
			if (excelStream == null) throw new ArgumentNullException("excelStream");
			_parameters = new Dictionary<string, string>();
			_tablesParameters = new Dictionary<string, List<IDictionary<string, string>>>();
			try
			{
				_excelWorkbook = new HSSFWorkbook(excelStream);
			}
			catch (Exception)
			{
				MessageBox.Show("Файл повреждён и его невозможно прочитать. Если файл открывается в Excel - сохраните его заново с другим именем.", "Файл повреждён", MessageBoxButtons.OK,
					MessageBoxIcon.Error);
			}
		}

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

		public abstract IDictionary<string, IDictionary<string, string>> Tables { get; }

		public abstract IDictionary<string, string> Arguments { get; }

		public abstract List<ICell> Cells { get; set; }

		public abstract List<NPOI.SS.UserModel.IRow> Rows { get; set; }

		public ISheet Sheet { get; set; }

		protected virtual void FillArguments()
		{
			var worksheet = _excelWorkbook.GetSheetAt(0);

			if (worksheet == null) return;

			foreach (var argument in Arguments)
			{
				var arg = argument;
				if (arg.Key == "Date" || arg.Key == "DateTime")
				{
					DateTime date;
					var cell = Cells.Where(p => p.CellType == CellType.String).FirstOrDefault(p => Regex.IsMatch(p.StringCellValue, arg.Value, RegexOptions.IgnoreCase));
					if (cell == null)
					{
						cell = Cells.Where(p => p.CellType == CellType.Numeric).FirstOrDefault(p => DateTime.TryParse(p.NumericCellValue.ToString(CultureInfo.InvariantCulture), out date));
						if (cell != null)
							_parameters.Add(arg.Key, cell.NumericCellValue.ToString(CultureInfo.InvariantCulture));
						continue;
					}
					if (cell.StringCellValue == null) { _parameters.Add(arg.Key, string.Empty); }
					else
					{
						var args = cell.StringCellValue.Split(' ');
						if (args.Any(p => DateTime.TryParse(p, out date)))
						{
							_parameters.Add(arg.Key, cell.StringCellValue);
						}
						else
						{
							var cellWithDate = Cells.FirstOrDefault(p => DateTime.TryParse(p.StringCellValue, out date));
							_parameters.Add(arg.Key, cellWithDate != null ? cellWithDate.StringCellValue : null);
						}
					}
				}
				else if (arg.Key == "Time")
				{
					TimeSpan time;
					var cell = Cells.Where(p => p.CellType == CellType.String).FirstOrDefault(p => Regex.IsMatch(p.StringCellValue, arg.Value, RegexOptions.IgnoreCase));
					if (cell == null)
					{
						cell = Cells.Where(p => p.CellType == CellType.Numeric).FirstOrDefault(p => TimeSpan.TryParse(p.NumericCellValue.ToString(CultureInfo.InvariantCulture), out time));
						if (cell != null)
							_parameters.Add(arg.Key, cell.NumericCellValue.ToString(CultureInfo.InvariantCulture));
						continue;
					}
					if (cell.StringCellValue == null)
					{
						_parameters.Add(arg.Key, string.Empty);
					}
					else
					{
						var args = cell.StringCellValue.Split(' ');
						if (args.Any(p => TimeSpan.TryParse(p, out time)))
						{
							_parameters.Add(arg.Key, cell.StringCellValue);
						}
						else
						{
							var cellWithDate = Cells.FirstOrDefault(p => TimeSpan.TryParse(p.StringCellValue, out time));
							_parameters.Add(arg.Key, cellWithDate != null ? cellWithDate.StringCellValue : null);
						}
					}
				}
				else if (arg.Key == "Place")
				{
					var cell = Cells.Where(p => p.CellType == CellType.String).FirstOrDefault(p => Regex.IsMatch(p.StringCellValue, arg.Value, RegexOptions.IgnoreCase));
					if (cell != null)
					{
						var cellUseful = Cells.Single(p => p.RowIndex == cell.RowIndex && p.ColumnIndex == cell.ColumnIndex + 1);

						_parameters.Add(arg.Key, cellUseful != null ? cellUseful.StringCellValue != null ? cellUseful.StringCellValue.Trim() : null : null);
					}
					else
					{
						_parameters.Add(arg.Key, null);
					}
				}
				else
				{
					var cell = Cells.Where(p => p.CellType == CellType.String).FirstOrDefault(p => Regex.IsMatch(p.StringCellValue, arg.Value, RegexOptions.IgnoreCase));
					if (!_parameters.ContainsKey(argument.Key))
						_parameters.Add(argument.Key, cell != null ? cell.StringCellValue != null ? cell.StringCellValue.Trim() : null : null);
					else if (_parameters[argument.Key] != null)
						_parameters[argument.Key] = cell != null ? cell.StringCellValue != null ? cell.StringCellValue.Trim() : null : null;
				}
			}
		}

		protected virtual int GetStartRow(ISheet worksheet, KeyValuePair<string, IDictionary<string, string>> table)
		{
			if (worksheet == null) throw new ArgumentNullException("worksheet");

			var startRow = 1;
			var firstOrDefault = Cells.Where(p => p.CellType == CellType.String).FirstOrDefault(
				p => Regex.IsMatch(p.StringCellValue.Replace("\n", " ").Trim(), table.Value["Count"].Trim(), RegexOptions.IgnoreCase));
			if (firstOrDefault != null)
			{
				_countColumn =
					firstOrDefault.ColumnIndex;
				var excelRangeBase = Cells.Where(p => p.CellType == CellType.String).FirstOrDefault(
					p => Regex.IsMatch(p.StringCellValue.Replace("\n", " ").Trim(), table.Value["Price"].Trim(), RegexOptions.IgnoreCase));
				if (excelRangeBase != null)
				{
					_priceColumn = excelRangeBase.ColumnIndex;
					var orDefault = Cells.Where(p => p.CellType == CellType.String).FirstOrDefault(
						p => Regex.IsMatch(p.StringCellValue.Replace("\n", " ").Trim(), table.Value["SumPrice"].Trim(), RegexOptions.IgnoreCase));
					if (orDefault != null)
					{
						_sumPriceColumn = orDefault.ColumnIndex;
						if (firstOrDefault.RowIndex == excelRangeBase.RowIndex && excelRangeBase.RowIndex == orDefault.RowIndex)
							startRow = firstOrDefault.RowIndex + 1;
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
				var countCell =
					Rows.Where(p => p.RowNum == startRow).SelectMany(p => p.Cells).Single(p => p.ColumnIndex == _countColumn);
				var priceCell =
					Rows.Where(p => p.RowNum == startRow).SelectMany(p => p.Cells).Single(p => p.ColumnIndex == _priceColumn);
				var sumPriceCell =
					Rows.Where(p => p.RowNum == startRow).SelectMany(p => p.Cells).Single(p => p.ColumnIndex == _sumPriceColumn);
				if ((countCell.CellType == CellType.String && !string.IsNullOrEmpty(countCell.StringCellValue) &&
					int.TryParse(countCell.StringCellValue.Trim(), NumberStyles.Any, CultureInfo.InvariantCulture, out count)) ||
					(priceCell.CellType == CellType.String && !string.IsNullOrEmpty(priceCell.StringCellValue) &&
					decimal.TryParse(priceCell.StringCellValue.Replace(",", ".").Trim(), NumberStyles.Number, CultureInfo.InvariantCulture, out price)) ||
					(sumPriceCell.CellType == CellType.String && !string.IsNullOrEmpty(sumPriceCell.StringCellValue) &&
					decimal.TryParse(Regex.Match(sumPriceCell.StringCellValue.Replace(",", ".").Trim(), @"\d", RegexOptions.CultureInvariant).Value, NumberStyles.Number, CultureInfo.InvariantCulture, out sumPrice)))
					return startRow;
				if (countCell.CellType == CellType.Numeric ||
					priceCell.CellType == CellType.Numeric ||
					sumPriceCell.CellType == CellType.Numeric)
					return startRow;
				startRow++;
			}
		}

		protected virtual int GetEndRow(ISheet worksheet, KeyValuePair<string, IDictionary<string, string>> table, int startRow)
		{
			if (worksheet == null) throw new ArgumentNullException("worksheet");

			var endIndex = startRow;

			while (true)
			{
				var countCell =
					Rows.Where(p => p.RowNum == endIndex).SelectMany(p => p.Cells).Single(p => p.ColumnIndex == _countColumn);
				var priceCell =
					Rows.Where(p => p.RowNum == endIndex).SelectMany(p => p.Cells).Single(p => p.ColumnIndex == _priceColumn);
				var sumPriceCell =
					Rows.Where(p => p.RowNum == endIndex).SelectMany(p => p.Cells).Single(p => p.ColumnIndex == _sumPriceColumn);
				if ((countCell.CellType == CellType.String && (string.IsNullOrEmpty(countCell.StringCellValue) ||
					Regex.Match(countCell.StringCellValue.Trim(), @"\d", RegexOptions.CultureInvariant).Value.Length < 1)) ||
					countCell.CellType == CellType.Blank ||
					(priceCell.CellType == CellType.String && (string.IsNullOrEmpty(priceCell.StringCellValue) ||
					Regex.Match(priceCell.StringCellValue.Trim(), @"\d", RegexOptions.CultureInvariant).Value.Length < 1) && !Regex.IsMatch(priceCell.StringCellValue, "приглашение", RegexOptions.IgnoreCase)) ||
					priceCell.CellType == CellType.Blank ||
					(sumPriceCell.CellType == CellType.String && (string.IsNullOrEmpty(sumPriceCell.StringCellValue) ||
					Regex.Match(sumPriceCell.StringCellValue.Trim(), @"\d", RegexOptions.CultureInvariant).Value.Length < 1)) ||
					sumPriceCell.CellType == CellType.Blank)
				{
					var end = true;
					for (var i = 0; i < 9; i++)
					{
						var row = Rows.Where(p => p.RowNum == endIndex + i).ToList();
						if (!row.SelectMany(p => p.Cells).Any())
							return endIndex - 1;
						var newCountCell = row.SelectMany(p => p.Cells).Single(p => p.ColumnIndex == _countColumn);
						var newPriceCell =
							row.SelectMany(p => p.Cells).Single(p => p.ColumnIndex == _priceColumn);
						var newSumPriceCell =
							row.SelectMany(p => p.Cells).Single(p => p.ColumnIndex == _sumPriceColumn);

						if (!((newCountCell.CellType == CellType.String && (string.IsNullOrEmpty(newCountCell.StringCellValue) ||
					Regex.Match(newCountCell.StringCellValue.Trim(), @"\d", RegexOptions.CultureInvariant).Value.Length < 1)) ||
					newCountCell.CellType == CellType.Blank ||
					(newPriceCell.CellType == CellType.String && (string.IsNullOrEmpty(newPriceCell.StringCellValue) ||
					Regex.Match(newPriceCell.StringCellValue.Trim(), @"\d", RegexOptions.CultureInvariant).Value.Length < 1) && !Regex.IsMatch(newPriceCell.StringCellValue, "приглашение", RegexOptions.IgnoreCase)) ||
					newPriceCell.CellType == CellType.Blank ||
					(newSumPriceCell.CellType == CellType.String && (string.IsNullOrEmpty(newSumPriceCell.StringCellValue) ||
					Regex.Match(newSumPriceCell.StringCellValue.Trim(), @"\d", RegexOptions.CultureInvariant).Value.Length < 1)) ||
					newSumPriceCell.CellType == CellType.Blank))
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
			var worksheet = _excelWorkbook.GetSheetAt(0);

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

				var rowIndex = GetStartRow(worksheet, table);

				var endRow = GetEndRow(worksheet, table, rowIndex);

				do
				{
					var countCell =
					Rows.Where(p => p.RowNum == rowIndex).SelectMany(p => p.Cells).Single(p => p.ColumnIndex == _countColumn);
					var priceCell =
						Rows.Where(p => p.RowNum == rowIndex).SelectMany(p => p.Cells).Single(p => p.ColumnIndex == _priceColumn);
					var sumPriceCell =
						Rows.Where(p => p.RowNum == rowIndex).SelectMany(p => p.Cells).Single(p => p.ColumnIndex == _sumPriceColumn);
					if ((countCell.CellType == CellType.String && (string.IsNullOrEmpty(countCell.StringCellValue) ||
						Regex.Match(countCell.StringCellValue.Trim(), @"\d", RegexOptions.CultureInvariant).Value.Length < 1)) ||
						countCell.CellType == CellType.Blank ||
						(priceCell.CellType == CellType.String && (string.IsNullOrEmpty(priceCell.StringCellValue) ||
						Regex.Match(priceCell.StringCellValue.Trim(), @"\d", RegexOptions.CultureInvariant).Value.Length < 1) && !Regex.IsMatch(priceCell.StringCellValue, "приглашение", RegexOptions.IgnoreCase)) ||
						priceCell.CellType == CellType.Blank ||
						(sumPriceCell.CellType == CellType.String && (string.IsNullOrEmpty(sumPriceCell.StringCellValue) ||
						Regex.Match(sumPriceCell.StringCellValue.Trim(), @"\d", RegexOptions.CultureInvariant).Value.Length < 1)) ||
						sumPriceCell.CellType == CellType.Blank)
					{
						rowIndex++;
						continue;
					}

					var row = new Dictionary<string, string>();

					foreach (var properties in table.Value)
					{
						var columnHolder = Cells.Where(p => p.CellType == CellType.String).FirstOrDefault(
							p => String.Equals(p.StringCellValue.Replace("\n", " ").Trim(), properties.Value.Trim(), StringComparison.CurrentCultureIgnoreCase));
						if (columnHolder == null)
						{
							columnHolder = Cells.Where(p => p.CellType == CellType.String).FirstOrDefault(
									p => Regex.IsMatch(p.StringCellValue.Replace("\n", " ").Trim(), properties.Value, RegexOptions.IgnoreCase));
							if (columnHolder == null)
							{
								throw new ExcelFormatException("Колонка с именем " + properties.Value + " не найдена");
							}
						}
						var cell = Rows.Where(p => p.RowNum == rowIndex).SelectMany(p => p.Cells).Single(p => p.ColumnIndex == columnHolder.ColumnIndex);
						if (cell != null && cell.CellType == CellType.String)
						{
							row.Add(properties.Key, cell.StringCellValue);
						}
						else if (cell != null && cell.CellType == CellType.Numeric)
						{
							row.Add(properties.Key, cell.NumericCellValue.ToString(CultureInfo.InvariantCulture));
						}
						else
						{

							row.Add(properties.Key, null);
						}
					}

					rows.Add(row);
					rowIndex++;

				} while (rowIndex <= endRow);
			}
		}

		public virtual void Fill()
		{
			if (_excelWorkbook.NumberOfSheets <= 0)
				throw new WorksheetsNotFoundException();

			Sheet = _excelWorkbook.GetSheetAt(0);
			Cells = new List<ICell>();
			Rows = new List<IRow>();
			for (var rowNum = Sheet.FirstRowNum - 1; rowNum <= Sheet.LastRowNum; rowNum++)
			{
				var row = Sheet.GetRow(rowNum);

				if (row == null) continue;

				Rows.Add(row);
				Cells.AddRange(row.Cells);
			}
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
