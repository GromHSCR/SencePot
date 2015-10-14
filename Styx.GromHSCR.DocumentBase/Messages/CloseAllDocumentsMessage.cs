using Styx.GromHSCR.DocumentBase.Documents;

namespace Styx.GromHSCR.DocumentBase.Messages
{
	public class CloseAllDocumentsMessage : BaseDocumentMessage
	{
		public CloseAllDocumentsMessage(IDocument document)
			: base(document)
		{
		}
	}
}
