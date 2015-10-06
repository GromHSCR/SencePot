using SenceRep.GromHSCR.DocumentBase.Documents;

namespace SenceRep.GromHSCR.DocumentBase.Messages
{
	public class CloseAllDocumentsMessage : BaseDocumentMessage
	{
		public CloseAllDocumentsMessage(IDocument document)
			: base(document)
		{
		}
	}
}
