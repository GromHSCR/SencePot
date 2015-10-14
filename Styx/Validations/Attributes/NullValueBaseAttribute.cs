using System.ComponentModel.DataAnnotations;

namespace Styx.Validations.Attributes
{
	public abstract class NullValueBaseAttribute : ValidationAttribute
	{
		private readonly bool _isAllowNull;

		protected virtual bool IsAllowNull
		{
			get
			{
				return _isAllowNull;
			}
		}

		protected NullValueBaseAttribute(bool isAllowNull = false)
		{
			_isAllowNull = isAllowNull;
		}
	}
}