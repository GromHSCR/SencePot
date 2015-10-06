using System.Data.Entity.Migrations;
using SenceRep.GromHSCR.Repositories.Context;

namespace SenceRep.GromHSCR.Repositories.Migrations
{
    internal sealed class Configuration : DbMigrationsConfiguration<DefaultContext>
	{
		public Configuration()
		{
			AutomaticMigrationsEnabled = true;
			AutomaticMigrationDataLossAllowed = true;
		}

		protected override void Seed(DefaultContext context)
		{
			//AddTestData(context);
			//AddMelnitsaData(context);
			//AddMsmData2(context);
			if (DataBaseInfo.IsAddData)
			{
				AddFirstData(context);
				//AddMelnitsaData(context);

				DataBaseInfo.IsAddData = false;
			}
		}


	    private void AddFirstData(DefaultContext context)
	    {

	    }

	}
}
