using System;

namespace Styx.GromHSCR.DocumentParserBase.Exceptions
{
	public class ParameterNotInitializedException: Exception
	{
		public ParameterNotInitializedException(string message)
			: base(message)
		{
		}
	}
}
