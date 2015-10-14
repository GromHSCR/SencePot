using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.RegularExpressions;

namespace Styx.Validations.Attributes
{
	[AttributeUsage(AttributeTargets.Property)]
	public sealed class INNAttribute : ValidationAttribute
	{
		private readonly bool _isIp;

		public INNAttribute(bool isIp = false)
		{
			_isIp = isIp;
		}

		#region Private members

		private static readonly Regex INNRegEx = new Regex(@"(^\d{10}$)|(^F\d{10}$)|(^\d{12}$)");
		private static readonly int[] WeightFactorsFor10DigitsINN = new[] { 2, 4, 10, 3, 5, 9, 4, 6, 8 };
		private static readonly int[] FirstWeightFactorsFor12DigitsINN = new[] { 7, 2, 4, 10, 3, 5, 9, 4, 6, 8 };
		private static readonly int[] SecondWeightFactorsFor12DigitsINN = new[] { 3, 7, 2, 4, 10, 3, 5, 9, 4, 6, 8 };

		#endregion Private members

		#region ValidationAttribute methods overrides

		public override bool IsValid(object value)
		{
			var inn = value as string;

			if (string.IsNullOrEmpty(inn))
			{
				return true;
			}

			// значение должно иметь вид: NNNNXXXXXC либо FNNNNXXXXXC либо NNNNXXXXXXCC
			if (!INNRegEx.IsMatch(inn))
			{
				return false;
			}

			if (inn.StartsWith("F"))
			{
				inn = inn.Substring(1);
			}

			if (inn.Any(c => !Char.IsDigit(c)))
			{
				return false;
			}

			if (!_isIp && inn.Length == 10)
			{
				return int.Parse(inn.Substring(inn.Length - 1, 1)) ==
					GetCheckNumber(GetCheckSum(inn, WeightFactorsFor10DigitsINN));
			}

			if (_isIp && inn.Length == 12)
			{
				return GetCheckNumber(GetCheckSum(inn, FirstWeightFactorsFor12DigitsINN)) == int.Parse(inn.Substring(inn.Length - 2, 1)) &&
						GetCheckNumber(GetCheckSum(inn, SecondWeightFactorsFor12DigitsINN)) == int.Parse(inn.Substring(inn.Length - 1, 1));
			}
			return false;
		}

		public override string FormatErrorMessage(string name)
		{
			return "Некорректный ИНН";
		}

		#endregion ValidationAttribute methods overrides

		#region Helper methods

		private static int GetCheckSum(string inn, int[] weightFactors)
		{
			return weightFactors.Select((t, i) => int.Parse(inn.Substring(i, 1)) * t).Sum();
		}

		private static int GetCheckNumber(int checkSum)
		{
			return (checkSum % 11) % 10;
		}

		#endregion Helper methods
	}
}
