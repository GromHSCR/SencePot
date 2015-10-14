using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using Styx.GromHSCR.Entities;
using Styx.GromHSCR.Repositories.Context;

namespace Styx.GromHSCR.Repositories.Repositories
{
	public class Repository<T> : IRepository<T> where T : BaseEntity
	{
		//private readonly IContext<T> _context;

		public DefaultContext DefaultContext { get; private set; }
		public DbSet<T> DbSet { get; private set; }

		public Repository()
		{
			//_context = new Context<T>();

			DefaultContext = new DefaultContext();
			DbSet = DefaultContext.Set<T>();
		}
		public Repository(DefaultContext context)
		{
			DefaultContext = context;
			DbSet = DefaultContext.Set<T>();
		}
		public void Delete(T entity)
		{
			DbSet.Remove(entity);
			DefaultContext.SaveChanges();
		}

		public void Delete(List<T> list)
		{
			DbSet.RemoveRange(list);
			DefaultContext.SaveChanges();
		}

		public IEnumerable<T> Add(List<T> list)
		{
			var newList = DbSet.AddRange(list);
			DefaultContext.SaveChanges();
			return newList;
		}
		public T Add(T entity)
		{
			var newEntry = DbSet.Add(entity);
			DefaultContext.SaveChanges();
			return newEntry;
		}

		public void Update(T entity)
		{
			var entry = DefaultContext.Entry(entity);
			DbSet.Attach(entity);
			entry.State = EntityState.Modified;
			DefaultContext.SaveChanges();
		}

		public IQueryable<T> Find(Expression<Func<T, bool>> @where)
		{
			return DbSet.Where(where);
		}

		public T Single(Expression<Func<T, bool>> @where)
		{
			return DbSet.SingleOrDefault(where);
		}

		public T First(Expression<Func<T, bool>> @where)
		{
			return DbSet.FirstOrDefault(where);
		}

		public IQueryable<T> GetAll()
		{
			return DbSet;
		}

		public T GetById(Guid id)
		{
			return DbSet.FirstOrDefault(c => c.Id == id);
		}

		public void Dispose()
		{
			DefaultContext.Dispose();
		}
	}
}
