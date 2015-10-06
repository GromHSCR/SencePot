using System.Data.Entity;
using SenceRep.GromHSCR.Repositories.Context;
using SenceRep.GromHSCR.Repositories.Migrations;

namespace SenceRep.GromHSCR.Repositories
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
