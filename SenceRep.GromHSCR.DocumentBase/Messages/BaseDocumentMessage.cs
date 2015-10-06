using SenceRep.GromHSCR.DocumentBase.Documents;

namespace SenceRep.GromHSCR.DocumentBase.Messages
{
	public abstract class BaseDocumentMessage
	{
		public IDocument Document { get; private set; }

		protected BaseDocumentMessage(IDocument document)
		{
			Document = document;
		}
	}
}
