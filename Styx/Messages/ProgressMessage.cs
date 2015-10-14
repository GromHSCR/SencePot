namespace Styx.Messages
{
	public class ProgressMessage
	{
		public ProgressType ProgressType { get; set; }

		public double MinValue { get; set; }

		public double MaxValue { get; set; }

		public double CurrentValue { get; set; }

		public string Text { get; set; }
	}

	public enum ProgressType
	{
		Start,
		Progress,
		Stop,
		Indeterminate
	}
}