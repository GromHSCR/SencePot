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
			var city = new City() { Id = Guid.NewGuid(), Name = "������" };
			context.Cities.Add(city);
			var region = new Region() { Id = Guid.NewGuid(), CityId = city.Id, Name = "������-�������� (����)" };
			context.Regions.Add(region);
			var region1 = new Region() { Id = Guid.NewGuid(), CityId = city.Id, Name = "�������� (���)" };
			context.Regions.Add(region1);
			var region2 = new Region() { Id = Guid.NewGuid(), CityId = city.Id, Name = "������-��������� (����)" };
			context.Regions.Add(region2);
			var region3 = new Region() { Id = Guid.NewGuid(), CityId = city.Id, Name = "�������� (���)" };
			context.Regions.Add(region3);
			var region4 = new Region() { Id = Guid.NewGuid(), CityId = city.Id, Name = "����������� (���)" };
			context.Regions.Add(region4);
			var region5 = new Region() { Id = Guid.NewGuid(), CityId = city.Id, Name = "��������� (���)" };
			context.Regions.Add(region5);
			var region6 = new Region() { Id = Guid.NewGuid(), CityId = city.Id, Name = "���-�������� (����)" };
			context.Regions.Add(region6);
			var region7 = new Region() { Id = Guid.NewGuid(), CityId = city.Id, Name = "����� (���)" };
			context.Regions.Add(region7);
			var region8 = new Region() { Id = Guid.NewGuid(), CityId = city.Id, Name = "���-��������� (����)" };
			context.Regions.Add(region8);
			var region9 = new Region() { Id = Guid.NewGuid(), CityId = city.Id, Name = "�������������� (�����)" };
			context.Regions.Add(region9);
			var region10 = new Region() { Id = Guid.NewGuid(), CityId = city.Id, Name = "�������������� (���)" };
			context.Regions.Add(region10);
			var region11 = new Region() { Id = Guid.NewGuid(), CityId = city.Id, Name = "�������� (���)" };
			context.Regions.Add(region11);
#if DEBUG
			
			var street = new Street() { Id = Guid.NewGuid(), RegionId = region.Id, CityId = city.Id, Name = "�����" };
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
				Prefix = "�"
			};
			context.HousePrefixes.Add(housePrefix);
			var address = new Address()
			{
				Id = Guid.NewGuid(),
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
				Name = "��-5-4",
				Version = "v2.1",
				LastCheckDate = DateTime.Now
			};
			context.CounterModels.Add(heatCounterModel);
			var heatCounter = new HeatCounter()
			{
				Id = Guid.NewGuid(),
				AddressId = address.Id,
				CounterModelId = heatCounterModel.Id,
				Number = 666666
			};
			context.HeatCounters.Add(heatCounter);
			var printInfo = new PrintInfo()
			{
				Id = Guid.NewGuid(),
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
				WorkTime = 14
			};
			context.DailyDatas.Add(dailyData);



#endif
		}

	}
}
