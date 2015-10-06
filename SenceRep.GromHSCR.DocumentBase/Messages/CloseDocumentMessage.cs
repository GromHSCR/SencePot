using SenceRep.GromHSCR.DocumentBase.Documents;

namespace SenceRep.GromHSCR.DocumentBase.Messages
{
	public class CloseDocumentMessage : BaseDocumentMessage
	{
		public CloseDocumentMessage(IDocument document)
			: base(document)
		{
		}
	}
}
