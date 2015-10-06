using System;
using System.Data.Entity;

namespace SenceRep.GromHSCR.Repositories.Context
{
	public interface IContext<T> : IDisposable where T : class 
	{
		DefaultContext DefaultContext { get; }
		DbSet<T> DbSet { get; }
	}
}
