using Styx.GromHSCR.DocumentBase.Documents;

namespace Styx.GromHSCR.DocumentBase.Messages
{
	public class CloseDocumentMessage : BaseDocumentMessage
	{
		public CloseDocumentMessage(IDocument document)
			: base(document)
		{
		}
	}
}
