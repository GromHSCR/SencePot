using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace Styx.GromHSCR.Repositories.Context
{
	public class DefaultContext : DbContext
	{
		public DefaultContext() : base("Redkassa.Promoter.Database")
		{
			Configuration.LazyLoadingEnabled = true;
		}
        
		//public DbSet<Entities.VoucherTemplate> VoucherTemplates { get; set; }


		protected override void OnModelCreating(DbModelBuilder modelBuilder)
		{
			base.OnModelCreating(modelBuilder);
			modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();
			modelBuilder.Conventions.Remove<ManyToManyCascadeDeleteConvention>();
		}
	}
}
