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

		public DbSet<Entities.PrintInfo> PrintInfos { get; set; }

		public DbSet<Entities.Address> Addresses { get; set; }

		public DbSet<Entities.City> Cities { get; set; }

		public DbSet<Entities.Region> Regions { get; set; }

		public DbSet<Entities.Street> Streets { get; set; }

		public DbSet<Entities.House> Houses { get; set; }

		public DbSet<Entities.Building> Buildings { get; set; }

		public DbSet<Entities.Housing> Housings { get; set; }

		public DbSet<Entities.HousePrefix> HousePrefixes { get; set; }

		public DbSet<Entities.HeatCounter> HeatCounters { get; set; }

		public DbSet<Entities.CounterModel> CounterModels { get; set; }

		public DbSet<Entities.DailyData> DailyDatas { get; set; }

		public DbSet<Entities.LegalEntity> LegalEntities { get; set; }

		public DbSet<Entities.Organization> Organizations { get; set; }

		public DbSet<Entities.Contract> Contracts { get; set; }


		protected override void OnModelCreating(DbModelBuilder modelBuilder)
		{
			base.OnModelCreating(modelBuilder);
			modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();
			modelBuilder.Conventions.Remove<ManyToManyCascadeDeleteConvention>();
		}
	}
}
