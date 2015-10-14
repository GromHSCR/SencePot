﻿using System.Collections.Generic;
using System.Windows.Controls;
using Styx.GromHSCR.MvvmBase.Validations;

namespace Styx.Validations
{
	internal class ComboBoxValidationRule : ValidationRule, IValidation
	{
		public override ValidationResult Validate(object value, System.Globalization.CultureInfo cultureInfo)
		{
			if (value != null)
			{
				IsValid = true;
				return new ValidationResult(true, null);
			}
			return new ValidationResult(false, "combo box");
		}

		public Dictionary<string, string> ValidationErrors { get; private set; }
		public bool IsValid { get; private set; }
	}
}