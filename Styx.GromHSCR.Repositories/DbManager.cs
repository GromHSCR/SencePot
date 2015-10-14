using System.Data.Entity;
using Styx.GromHSCR.Repositories.Context;
using Styx.GromHSCR.Repositories.Migrations;

namespace Styx.GromHSCR.Repositories
{
	public class DbManager
	{
		public static void Initialize()
		{
			using (var db = new DefaultContext())
			{
				DataBaseInfo.IsAddData = !db.Database.Exists();

				Database.SetInitializer(new MigrateDatabaseToLatestVersion<DefaultContext, Configuration>());

				db.Database.Initialize(false);
			}
		}
	}
}
