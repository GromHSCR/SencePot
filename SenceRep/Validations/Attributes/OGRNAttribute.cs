namespace SenceRep.Validations.Attributes
{
	public class OGRNAttribute : NullValueBaseAttribute
	{
		public OGRNAttribute(bool isAllowNull = false)
			: base(isAllowNull)
		{


		}

		public override bool IsValid(object value)
		{
			var ogrn = value as string;

			if (IsAllowNull && string.IsNullOrEmpty(ogrn))
			{
				return true;
			}

			return OGRNorOGRNIPValidator.IsOGRNValid(ogrn);
		}

		public override string FormatErrorMessage(string name)
		{
			return "Некорректный ОГРН";
		}
	}
}