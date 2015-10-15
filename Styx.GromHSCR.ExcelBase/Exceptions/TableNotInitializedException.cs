using System;

namespace Styx.GromHSCR.DocumentParserBase.Exceptions
{
	public class TableNotInitializedException: Exception
	{
		public TableNotInitializedException(string message)
			: base(message)
		{
		}
	}
}
