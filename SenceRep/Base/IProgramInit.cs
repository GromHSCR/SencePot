using System;

namespace SenceRep.Base
{
	public interface IProgramInit
	{
		bool IsInit { get; set; }

		void OnInitExec(Action action);

		event EventHandler<InitEventArgs> InitEvent;
	}
}
