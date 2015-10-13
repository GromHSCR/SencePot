using System.ComponentModel.DataAnnotations;

namespace SenceRep.Validations.Attributes
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