namespace Styx.Validations.Attributes
{
	public class OGRNIPAttribute : NullValueBaseAttribute
	{
		public OGRNIPAttribute(bool isAllowNull = false)
			: base(isAllowNull)
		{


		}

		public override bool IsValid(object value)
		{
			var ogrnip = value as string;

			if (IsAllowNull && string.IsNullOrEmpty(ogrnip))
			{
				return true;
			}

			return OGRNorOGRNIPValidator.IsOGRNIPValid(ogrnip);
		}

		public override string FormatErrorMessage(string name)
		{
			return "Некорректный ОГРНИП";
		}
	}
}