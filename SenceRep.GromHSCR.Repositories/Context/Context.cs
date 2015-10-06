using System.Data.Entity;

namespace SenceRep.GromHSCR.Repositories.Context
{
	public class Context<T> : IContext<T> where T : class 
	{
		public Context()
		{
			DefaultContext = new DefaultContext();
			DbSet = DefaultContext.Set<T>();
		} 
		
		public void Dispose()
		{
			DefaultContext.Dispose();
		}

		public DefaultContext DefaultContext { get; private set; }
		public DbSet<T> DbSet { get; private set; }
	}
}
