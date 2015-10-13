using System;
using System.Collections.Generic;
using System.Windows.Controls;
using SenceRep.GromHSCR.MvvmBase.Validations;

namespace SenceRep.Validations
{
	internal class EmptyFieldValidationRule : ValidationRule, IValidation
	{
		public override ValidationResult Validate(object value, System.Globalization.CultureInfo cultureInfo)
		{
			var @string = value as string;
			if (@string != null && !String.IsNullOrEmpty(@string.Trim()))
			{
				IsValid = true;
				return new ValidationResult(true, null);
			}
			return new ValidationResult(false, Properties.Resources.EmptyFieldValidationRule);
		}

		public Dictionary<string, string> ValidationErrors { get; private set; }
		public bool IsValid { get; private set; }
	}
}