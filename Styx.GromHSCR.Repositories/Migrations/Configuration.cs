using System;
using System.Data.Entity.Migrations;
using System.Diagnostics;
using System.Net.Mime;
using System.Threading;
using Styx.GromHSCR.Entities;
using Styx.GromHSCR.Repositories.Context;

namespace Styx.GromHSCR.Repositories.Migrations
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
#if DEBUG
			var city = new City() { Id = Guid.NewGuid(), Name = "Город" };
			context.Cities.Add(city);
			var region = new Region() { Id = Guid.NewGuid(), CityId = city.Id, Name = "Район" };
			context.Regions.Add(region);
			var street = new Street() { Id = Guid.NewGuid(), RegionId = region.Id, CityId = city.Id, Name = "Улица" };
			context.Streets.Add(street);
			var house = new House() { Id = Guid.NewGuid(), StreetId = street.Id, Number = 1 };
			context.Houses.Add(house);
			var building = new Building()
			{
				Id = Guid.NewGuid(),
				HouseId = house.Id,
				Number = 2
			};
			context.Buildings.Add(building);
			var housing = new Housing()
			{
				Id = Guid.NewGuid(),
				HouseId = house.Id,
				Number = 3
			};
			context.Housings.Add(housing);
			var housePrefix = new HousePrefix()
			{
				Id = Guid.NewGuid(),
				HouseId = house.Id,
				Prefix = "А"
			};
			context.HousePrefixes.Add(housePrefix);
			var address = new Address()
			{
				CityId = city.Id,
				RegionId = region.Id,
				StreetId = street.Id,
				HouseId = house.Id,
				BuildingId = building.Id,
				HousingId = housing.Id,
				HousePrefixId = housePrefix.Id
			};
			context.Addresses.Add(address);
			var heatCounterModel = new CounterModel()
			{
				Id = Guid.NewGuid(),
				Name = "КМ-5-4",
				Version = "v2.1",
				LastCheckDate = DateTime.Now
			};
			context.CounterModels.Add(heatCounterModel);
			var heatCounter = new HeatCounter()
			{
				AddressId = address.Id,
				CounterModelId = heatCounterModel.Id,
				Id = Guid.NewGuid(),
				Number = 666666
			};
			context.HeatCounters.Add(heatCounter);
			var printInfo = new PrintInfo()
			{
				AddressId = address.Id,
				PrintStartDate = DateTime.Today,
				PrintEndDate = DateTime.Today + TimeSpan.FromDays(1),
				HeatCounterId = heatCounter.Id
			};
			context.PrintInfos.Add(printInfo);
			var dailyData = new DailyData()
			{
				Id = Guid.NewGuid(),
				PrintInfoId = printInfo.Id,
				CurrentDateTime = DateTime.Now,
				IsValid = false,
				ErrorCode = "U",
				WorkTime = TimeSpan.FromHours(14)
			};
			context.DailyDatas.Add(dailyData);



#endif
		}

	}
}
