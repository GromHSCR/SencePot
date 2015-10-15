using System;

namespace Styx.GromHSCR.DocumentParserBase.Exceptions
{
	public class ParameterNotFoundException : Exception
	{
		public ParameterNotFoundException(string parameterName)
			: base(parameterName)
		{

		}
	}
}
