using System;
using System.Data.Entity;

namespace Styx.GromHSCR.Repositories.Context
{
	public interface IContext<T> : IDisposable where T : class 
	{
		DefaultContext DefaultContext { get; }
		DbSet<T> DbSet { get; }
	}
}
