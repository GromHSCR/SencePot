namespace RedKassa.Promoter.Migrations
{
	using System;
	using System.Data.Entity.Migrations;

	public partial class v_1_3_1 : DbMigration
	{
		public override void Up()
		{
			CreateTable(
				"dbo.VoucherTemplates",
				c => new
				{
					Id = c.Guid(nullable: false),
					Name = c.String(maxLength: 4000),
					FromPosition = c.String(maxLength: 4000),
					ToPosition = c.String(maxLength: 4000),
					EventPosition = c.String(maxLength: 4000),
					DatePosition = c.String(maxLength: 4000),
					TimePosition = c.String(maxLength: 4000),
					PlacePosition = c.String(maxLength: 4000),
					SectorPosition = c.String(maxLength: 4000),
					RowPosition = c.String(maxLength: 4000),
					SeatFromPosition = c.String(maxLength: 4000),
					SeatToPosition = c.String(maxLength: 4000),
					CountPosition = c.String(maxLength: 4000),
					PricePosition = c.String(maxLength: 4000),
					SumPricePosition = c.String(maxLength: 4000),
					SeatGroupPosition = c.String(maxLength: 4000)
				})
				.PrimaryKey(t => t.Id);

			AddColumn("dbo.VoucherTemplates", "AgentId", c => c.Guid());
			CreateIndex("dbo.VoucherTemplates", "AgentId");
			AddForeignKey("dbo.VoucherTemplates", "AgentId", "dbo.Agents", "Id");
		}

		public override void Down()
		{
			DropForeignKey("dbo.VoucherTemplates", "AgentId", "dbo.Agents");
			DropIndex("dbo.VoucherTemplates", new[] { "AgentId" });
			DropColumn("dbo.VoucherTemplates", "AgentId");
			DropTable("dbo.VoucherTemplates");
		}
	}
}
