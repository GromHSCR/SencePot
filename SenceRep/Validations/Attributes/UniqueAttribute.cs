using System;
using System.ComponentModel.DataAnnotations;

namespace SenceRep.GromHSCR.Validations.Attributes
{
	[AttributeUsage(AttributeTargets.Property, AllowMultiple = false,Inherited = true)]
	public class UniqueAttribute : ValidationAttribute
	{
		public override bool RequiresValidationContext
		{
			get { return true; }
		}

		public override bool IsValid(object value)
		{
			return true;
		}

		protected override ValidationResult IsValid(object value, ValidationContext validationContext)
		{
			if (validationContext == null) return ValidationResult.Success;
			var checker = validationContext.ObjectInstance as IUniquePropertyChecker;
			if (checker == null) return ValidationResult.Success;
			return checker.IsPropertyUnique(validationContext.MemberName)
				? ValidationResult.Success
				: new ValidationResult(ErrorMessage);
		}
	}
}