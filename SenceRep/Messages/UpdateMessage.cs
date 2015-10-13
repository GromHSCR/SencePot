using System;

namespace SenceRep.Messages
{
	public class UpdateMessage
	{
		public Type UpdateType { get; set; }

		public Guid Id { get; set; }
	}
}
