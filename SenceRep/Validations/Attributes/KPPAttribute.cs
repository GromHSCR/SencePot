using System;

namespace SenceRep.GromHSCR.Validations.Attributes
{
	public class KPPAttribute : NullValueBaseAttribute
	{
		public KPPAttribute(bool isAllowNull = false)
			: base(isAllowNull)
		{


		}

		public override bool IsValid(object value)
		{
			var kpp = value as string;

			if (IsAllowNull && string.IsNullOrEmpty(kpp))
			{
				return true;
			}

			if (string.IsNullOrWhiteSpace(kpp)) return false;

			if (kpp.Length != 9) return false;

			Int64 number;
			return Int64.TryParse(kpp, out number);
		}

		public override string FormatErrorMessage(string name)
		{
			return "Некорректный КПП";
		}
	}
}