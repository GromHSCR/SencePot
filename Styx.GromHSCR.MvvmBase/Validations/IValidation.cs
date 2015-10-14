using System.Collections.Generic;

namespace Styx.GromHSCR.MvvmBase.Validations
{
	public interface IValidation
	{
		Dictionary<string, string> ValidationErrors { get; }

		bool IsValid { get; }
	}
}