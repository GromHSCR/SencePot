using System;
using System.Linq;

namespace SenceRep.GromHSCR.Repositories.Api
{
	public interface IRepository<T> : IDisposable
	{
		void Delete(T entity);

		T Add(T entity);

		void Update(T entity);

		IQueryable<T> GetAll();

		T GetById(Guid id);
	}
}
