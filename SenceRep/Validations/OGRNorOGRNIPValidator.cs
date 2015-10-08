using System;
using System.Linq;

namespace SenceRep.GromHSCR.Validations
{
	public static class OGRNorOGRNIPValidator
	{
		private static readonly int[] OldYears = { 90, 91, 92, 93, 94, 95, 96, 97, 98, 99 };

		private static bool GetIsCheckBase(string val)
		{
			if (string.IsNullOrWhiteSpace(val))
			{
				return false;
			}

			if (val.Any(p => !Char.IsDigit(p)))
			{
				return false;
			}

			if (val.Length != 13 && val.Length != 15) return false;

			#region проверка года регистрации
			var yearReg = Convert.ToInt32(val.Substring(1, 2));
			if (!(OldYears.Contains(yearReg) || (yearReg >= 0 && yearReg <= Convert.ToInt32(DateTime.Now.ToString("yy"))))) return false;
			#endregion

			#region проверка региона регистрации
			var regionCode = Convert.ToInt32(val.Substring(3, 2));
			if (!(regionCode >= 1 && regionCode <= 99)) return false;
			#endregion

			return true;
		}

		public static bool IsOGRNValid(string ogrn)
		{
			if (!GetIsCheckBase(ogrn)) return false;

			if (ogrn.Length != 13) return false;

			//проверка признака отнесения государственного регистрационного номера записи
			var regNumber = Convert.ToInt32(ogrn.Substring(0, 1));
			if (regNumber != 1 && regNumber != 2 && regNumber != 5) return false;

			//проверка контрольной суммы
			var number = Convert.ToInt64(ogrn.Substring(0, 12));
			var checkSum = Convert.ToInt64(ogrn.Substring(12, 1));
			return checkSum == ((number % 11) % 10);
		}

		public static bool IsOGRNIPValid(string ogrnp)
		{
			if (!GetIsCheckBase(ogrnp)) return false;

			if (ogrnp.Length != 15) return false;

			//проверка признака отнесения государственного регистрационного номера записи
			var regNumber = Convert.ToInt32(ogrnp.Substring(0, 1));
			if (regNumber != 3 && regNumber != 4) return false;

			//проверка контрольной суммы
			var number = Convert.ToInt64(ogrnp.Substring(0, 14));
			var checkSum = Convert.ToInt64(ogrnp.Substring(14, 1));
			return checkSum == ((number % 13) % 10);
		}
	}
}