using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Styx.GromHSCR.DocumentParserBase.Models
{
	public class ParseResult
	{
		public bool IsOk { get; set; }

		public PrintInfo PrintInfo { get; set; }

		public string ErrorMessage { get; set; }

	}
}
