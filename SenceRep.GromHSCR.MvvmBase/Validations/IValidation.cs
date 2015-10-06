using System.Collections.Generic;

namespace SenceRep.GromHSCR.MvvmBase.Validations
{
	public interface IValidation
	{
		Dictionary<string, string> ValidationErrors { get; }

		bool IsValid { get; }
	}
}