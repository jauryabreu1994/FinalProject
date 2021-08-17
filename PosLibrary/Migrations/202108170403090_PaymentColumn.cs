namespace PosLibrary.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PaymentColumn : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.PaymentMethod", "ToSales", c => c.Boolean(nullable: false));
            AddColumn("dbo.PaymentMethod", "ToReturn", c => c.Boolean(nullable: false));
            AddColumn("dbo.PaymentMethod", "OverTender", c => c.Boolean(nullable: false));
            AddColumn("dbo.PaymentMethod", "UnderTender", c => c.Boolean(nullable: false));
            AddColumn("dbo.PaymentMethod", "IsMainTender", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.PaymentMethod", "IsMainTender");
            DropColumn("dbo.PaymentMethod", "UnderTender");
            DropColumn("dbo.PaymentMethod", "OverTender");
            DropColumn("dbo.PaymentMethod", "ToReturn");
            DropColumn("dbo.PaymentMethod", "ToSales");
        }
    }
}
