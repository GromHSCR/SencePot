using System;
using System.Collections.Generic;
using System.Linq;

namespace Styx.GromHSCR.DocumentParserBase.Exceptions
{
	public class RequiredParametersNotFoundException: Exception
	{
		public RequiredParametersNotFoundException(string parameter)
			: base(parameter)
		{
			Parameters = new List<string> { parameter };
		}

		public RequiredParametersNotFoundException(IEnumerable<string> parameters)
			: base(string.Join(", ", parameters))
		{
			Parameters = parameters.ToList();
		}

		public IEnumerable<string> Parameters { get; private set; }
	}
}
