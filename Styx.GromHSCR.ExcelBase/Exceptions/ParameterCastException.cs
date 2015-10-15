using System;

namespace Styx.GromHSCR.DocumentParserBase.Exceptions
{
	public class ParameterCastException: Exception
	{
		public ParameterCastException(Exception innerException)
			: base("ParameterCastException", innerException)
		{

		}

		public ParameterCastException(string parameterName, Exception innerException)
			: base(parameterName, innerException)
		{
			
		}

		public ParameterCastException(string parameterName)
			: base(parameterName)
		{

		}
	}
}
