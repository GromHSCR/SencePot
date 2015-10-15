namespace Styx.GromHSCR.DocumentParserBase.Models
{
	public class PdfItem
	{
		public decimal StartPosX { get; set; }

		public decimal StartPosY { get; set; }

		public decimal CenterPosX
		{
			get
			{
				return StartPosX + StringWidth / 2;
			}
		}

		public decimal StringWidth
		{
			get
			{
				return Text.Length * DigitWidth;
			}
		}

		public decimal DigitWidth
		{
			get { return FontSize / (decimal) 2; }
		}

		public decimal StringWidthStep
		{
			get
			{
				return (decimal)(Text.Length * (FontSize / 10) + 0.5);
			}
		}

		public string Text { get; set; }

		public string AdditionalText { get; set; }

		public int FontSize { get; set; }

		public string FullText
		{
			get
			{
				return Text + AdditionalText;
			}
		}

	}
}
