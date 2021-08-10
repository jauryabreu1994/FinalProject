namespace PosLibrary.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class FirstMigration : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Customer",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CustomerId = c.String(nullable: false, maxLength: 25, unicode: false),
                        FirstName = c.String(nullable: false, maxLength: 55, unicode: false),
                        LastName = c.String(nullable: false, maxLength: 55, unicode: false),
                        Address = c.String(maxLength: 255, unicode: false),
                        VatNumber = c.String(maxLength: 25, unicode: false),
                        CompanyName = c.String(maxLength: 100),
                        Email = c.String(maxLength: 25, unicode: false),
                        Phone = c.String(maxLength: 25, unicode: false),
                        DateBorn = c.DateTime(nullable: false),
                        Deleted = c.Boolean(nullable: false),
                        Condition_Status = c.Boolean(nullable: false),
                        CreatedDate = c.DateTime(nullable: false),
                        UpdatedDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.TransactionHeader",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ReceiptId = c.String(nullable: false, maxLength: 25, unicode: false),
                        CustomerId = c.Int(nullable: false),
                        DiscountAmount = c.Decimal(nullable: false, precision: 18, scale: 2),
                        TotalAmount = c.Decimal(nullable: false, precision: 18, scale: 2),
                        TaxAmount = c.Decimal(nullable: false, precision: 18, scale: 2),
                        TotalPayment = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Deleted = c.Boolean(nullable: false),
                        Condition_Status = c.Boolean(nullable: false),
                        CreatedDate = c.DateTime(nullable: false),
                        UpdatedDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Customer", t => t.CustomerId, cascadeDelete: true)
                .Index(t => t.CustomerId);
            
            CreateTable(
                "dbo.TransactionLines",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ReceiptId = c.String(nullable: false, maxLength: 25, unicode: false),
                        ItemId = c.Int(nullable: false),
                        TransactionHeaderId = c.Int(nullable: false),
                        Description = c.String(nullable: false, maxLength: 100, unicode: false),
                        Quantity = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Price = c.Decimal(nullable: false, precision: 18, scale: 2),
                        TaxPercent = c.Decimal(nullable: false, precision: 18, scale: 2),
                        DiscountPercent = c.Decimal(nullable: false, precision: 18, scale: 2),
                        TotalAmount = c.Decimal(nullable: false, precision: 18, scale: 2),
                        TaxAmount = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Deleted = c.Boolean(nullable: false),
                        Condition_Status = c.Boolean(nullable: false),
                        CreatedDate = c.DateTime(nullable: false),
                        UpdatedDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Item", t => t.ItemId, cascadeDelete: true)
                .ForeignKey("dbo.TransactionHeader", t => t.TransactionHeaderId, cascadeDelete: true)
                .Index(t => t.ItemId)
                .Index(t => t.TransactionHeaderId);
            
            CreateTable(
                "dbo.Item",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Sku = c.String(nullable: false, maxLength: 25, unicode: false),
                        ItemTaxId = c.Int(nullable: false),
                        ItemDepartmentId = c.Int(nullable: false),
                        VendorId = c.Int(nullable: false),
                        ItemDiscountId = c.Int(nullable: false),
                        Name = c.String(nullable: false, maxLength: 55, unicode: false),
                        Price = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Deleted = c.Boolean(nullable: false),
                        Condition_Status = c.Boolean(nullable: false),
                        CreatedDate = c.DateTime(nullable: false),
                        UpdatedDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.ItemDepartment", t => t.ItemDepartmentId, cascadeDelete: true)
                .ForeignKey("dbo.ItemDiscount", t => t.ItemDiscountId, cascadeDelete: true)
                .ForeignKey("dbo.ItemTax", t => t.ItemTaxId, cascadeDelete: true)
                .ForeignKey("dbo.Vendor", t => t.VendorId, cascadeDelete: true)
                .Index(t => t.ItemTaxId)
                .Index(t => t.ItemDepartmentId)
                .Index(t => t.VendorId)
                .Index(t => t.ItemDiscountId);
            
            CreateTable(
                "dbo.ItemDepartment",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 55, unicode: false),
                        Deleted = c.Boolean(nullable: false),
                        Condition_Status = c.Boolean(nullable: false),
                        CreatedDate = c.DateTime(nullable: false),
                        UpdatedDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.ItemDiscount",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 55, unicode: false),
                        AmountPercent = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Deleted = c.Boolean(nullable: false),
                        Condition_Status = c.Boolean(nullable: false),
                        CreatedDate = c.DateTime(nullable: false),
                        UpdatedDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.ItemTax",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 55, unicode: false),
                        AmountPercent = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Deleted = c.Boolean(nullable: false),
                        Condition_Status = c.Boolean(nullable: false),
                        CreatedDate = c.DateTime(nullable: false),
                        UpdatedDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Vendor",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        VendorId = c.String(nullable: false, maxLength: 25, unicode: false),
                        FirstName = c.String(nullable: false, maxLength: 55, unicode: false),
                        LastName = c.String(nullable: false, maxLength: 55, unicode: false),
                        Address = c.String(maxLength: 255, unicode: false),
                        VatNumber = c.String(maxLength: 25, unicode: false),
                        CompanyName = c.String(maxLength: 100),
                        Phone = c.String(maxLength: 25, unicode: false),
                        Deleted = c.Boolean(nullable: false),
                        Condition_Status = c.Boolean(nullable: false),
                        CreatedDate = c.DateTime(nullable: false),
                        UpdatedDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.TransactionPayments",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ReceiptId = c.String(nullable: false, maxLength: 25, unicode: false),
                        PaymentMethodId = c.Int(nullable: false),
                        TransactionHeaderId = c.Int(nullable: false),
                        TotalAmount = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Deleted = c.Boolean(nullable: false),
                        Condition_Status = c.Boolean(nullable: false),
                        CreatedDate = c.DateTime(nullable: false),
                        UpdatedDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.PaymentMethod", t => t.PaymentMethodId, cascadeDelete: true)
                .ForeignKey("dbo.TransactionHeader", t => t.TransactionHeaderId, cascadeDelete: true)
                .Index(t => t.PaymentMethodId)
                .Index(t => t.TransactionHeaderId);
            
            CreateTable(
                "dbo.PaymentMethod",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 55, unicode: false),
                        Deleted = c.Boolean(nullable: false),
                        Condition_Status = c.Boolean(nullable: false),
                        CreatedDate = c.DateTime(nullable: false),
                        UpdatedDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.GroupPermission",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserGroupId = c.Int(nullable: false),
                        PermissionId = c.Int(nullable: false),
                        Deleted = c.Boolean(nullable: false),
                        Condition_Status = c.Boolean(nullable: false),
                        CreatedDate = c.DateTime(nullable: false),
                        UpdatedDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Permission", t => t.PermissionId, cascadeDelete: true)
                .ForeignKey("dbo.UserGroup", t => t.UserGroupId, cascadeDelete: true)
                .Index(t => t.UserGroupId)
                .Index(t => t.PermissionId);
            
            CreateTable(
                "dbo.Permission",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 100, unicode: false),
                        Code = c.String(),
                        Deleted = c.Boolean(nullable: false),
                        Condition_Status = c.Boolean(nullable: false),
                        CreatedDate = c.DateTime(nullable: false),
                        UpdatedDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.UserGroup",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 100, unicode: false),
                        Deleted = c.Boolean(nullable: false),
                        Condition_Status = c.Boolean(nullable: false),
                        CreatedDate = c.DateTime(nullable: false),
                        UpdatedDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.User",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(nullable: false, maxLength: 25, unicode: false),
                        Password = c.String(nullable: false, maxLength: 4000),
                        UserGroupId = c.Int(nullable: false),
                        FirstName = c.String(nullable: false, maxLength: 55, unicode: false),
                        LastName = c.String(nullable: false, maxLength: 55, unicode: false),
                        Address = c.String(maxLength: 255, unicode: false),
                        VatNumber = c.String(maxLength: 25, unicode: false),
                        Gender = c.Byte(nullable: false),
                        Email = c.String(maxLength: 100),
                        Phone = c.String(maxLength: 25, unicode: false),
                        Deleted = c.Boolean(nullable: false),
                        Condition_Status = c.Boolean(nullable: false),
                        CreatedDate = c.DateTime(nullable: false),
                        UpdatedDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.UserGroup", t => t.UserGroupId, cascadeDelete: true)
                .Index(t => t.UserGroupId);
            
            CreateTable(
                "dbo.NcfHistory",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ReceiptId = c.String(nullable: false, maxLength: 25, unicode: false),
                        NcfTypeId = c.Int(nullable: false),
                        NcfNumber = c.String(nullable: false, maxLength: 25, unicode: false),
                        ReturnReceiptId = c.String(nullable: false, maxLength: 25, unicode: false),
                        ReturnNcfNumber = c.String(nullable: false, maxLength: 25, unicode: false),
                        VatNumber = c.String(nullable: false, maxLength: 25, unicode: false),
                        Company = c.String(nullable: false, maxLength: 25, unicode: false),
                        TotalAmount = c.Decimal(nullable: false, precision: 18, scale: 2),
                        TotalAmountWithTax = c.Decimal(nullable: false, precision: 18, scale: 2),
                        TotalTax = c.Decimal(nullable: false, precision: 18, scale: 2),
                        TaxExempt = c.Boolean(nullable: false),
                        Deleted = c.Boolean(nullable: false),
                        Condition_Status = c.Boolean(nullable: false),
                        CreatedDate = c.DateTime(nullable: false),
                        UpdatedDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.NcfType", t => t.NcfTypeId, cascadeDelete: true)
                .Index(t => t.NcfTypeId);
            
            CreateTable(
                "dbo.NcfType",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        NcfId = c.Int(nullable: false),
                        IsDefaultSale = c.Boolean(nullable: false),
                        IsDefaultCreditMemo = c.Boolean(nullable: false),
                        Description = c.String(nullable: false, maxLength: 50, unicode: false),
                        Deleted = c.Boolean(nullable: false),
                        Condition_Status = c.Boolean(nullable: false),
                        CreatedDate = c.DateTime(nullable: false),
                        UpdatedDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.NcfSequenceDetail",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        NcfId = c.Int(nullable: false),
                        Serie = c.String(nullable: false, maxLength: 2, unicode: false),
                        SeqStart = c.Int(nullable: false),
                        SeqEnd = c.Int(nullable: false),
                        SeqNext = c.Int(nullable: false),
                        DateStart = c.DateTime(nullable: false),
                        DateEnd = c.DateTime(nullable: false),
                        SeqStatus = c.Int(nullable: false),
                        DGIIDescription = c.String(nullable: false, maxLength: 55, unicode: false),
                        Deleted = c.Boolean(nullable: false),
                        Condition_Status = c.Boolean(nullable: false),
                        CreatedDate = c.DateTime(nullable: false),
                        UpdatedDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.NcfType", t => t.NcfId, cascadeDelete: true)
                .Index(t => t.NcfId);
            
            CreateTable(
                "dbo.Store",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 55, unicode: false),
                        VatNumber = c.String(nullable: false, maxLength: 55, unicode: false),
                        CompanyName = c.String(nullable: false, maxLength: 55, unicode: false),
                        Address = c.String(maxLength: 255, unicode: false),
                        Email = c.String(maxLength: 100, unicode: false),
                        Phone = c.String(maxLength: 25, unicode: false),
                        ReceiptId = c.Int(nullable: false),
                        CustomerId = c.Int(nullable: false),
                        VendorId = c.Int(nullable: false),
                        Deleted = c.Boolean(nullable: false),
                        Condition_Status = c.Boolean(nullable: false),
                        CreatedDate = c.DateTime(nullable: false),
                        UpdatedDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.NcfSequenceDetail", "NcfId", "dbo.NcfType");
            DropForeignKey("dbo.NcfHistory", "NcfTypeId", "dbo.NcfType");
            DropForeignKey("dbo.User", "UserGroupId", "dbo.UserGroup");
            DropForeignKey("dbo.GroupPermission", "UserGroupId", "dbo.UserGroup");
            DropForeignKey("dbo.GroupPermission", "PermissionId", "dbo.Permission");
            DropForeignKey("dbo.TransactionHeader", "CustomerId", "dbo.Customer");
            DropForeignKey("dbo.TransactionPayments", "TransactionHeaderId", "dbo.TransactionHeader");
            DropForeignKey("dbo.TransactionPayments", "PaymentMethodId", "dbo.PaymentMethod");
            DropForeignKey("dbo.TransactionLines", "TransactionHeaderId", "dbo.TransactionHeader");
            DropForeignKey("dbo.Item", "VendorId", "dbo.Vendor");
            DropForeignKey("dbo.TransactionLines", "ItemId", "dbo.Item");
            DropForeignKey("dbo.Item", "ItemTaxId", "dbo.ItemTax");
            DropForeignKey("dbo.Item", "ItemDiscountId", "dbo.ItemDiscount");
            DropForeignKey("dbo.Item", "ItemDepartmentId", "dbo.ItemDepartment");
            DropIndex("dbo.NcfSequenceDetail", new[] { "NcfId" });
            DropIndex("dbo.NcfHistory", new[] { "NcfTypeId" });
            DropIndex("dbo.User", new[] { "UserGroupId" });
            DropIndex("dbo.GroupPermission", new[] { "PermissionId" });
            DropIndex("dbo.GroupPermission", new[] { "UserGroupId" });
            DropIndex("dbo.TransactionPayments", new[] { "TransactionHeaderId" });
            DropIndex("dbo.TransactionPayments", new[] { "PaymentMethodId" });
            DropIndex("dbo.Item", new[] { "ItemDiscountId" });
            DropIndex("dbo.Item", new[] { "VendorId" });
            DropIndex("dbo.Item", new[] { "ItemDepartmentId" });
            DropIndex("dbo.Item", new[] { "ItemTaxId" });
            DropIndex("dbo.TransactionLines", new[] { "TransactionHeaderId" });
            DropIndex("dbo.TransactionLines", new[] { "ItemId" });
            DropIndex("dbo.TransactionHeader", new[] { "CustomerId" });
            DropTable("dbo.Store");
            DropTable("dbo.NcfSequenceDetail");
            DropTable("dbo.NcfType");
            DropTable("dbo.NcfHistory");
            DropTable("dbo.User");
            DropTable("dbo.UserGroup");
            DropTable("dbo.Permission");
            DropTable("dbo.GroupPermission");
            DropTable("dbo.PaymentMethod");
            DropTable("dbo.TransactionPayments");
            DropTable("dbo.Vendor");
            DropTable("dbo.ItemTax");
            DropTable("dbo.ItemDiscount");
            DropTable("dbo.ItemDepartment");
            DropTable("dbo.Item");
            DropTable("dbo.TransactionLines");
            DropTable("dbo.TransactionHeader");
            DropTable("dbo.Customer");
        }
    }
}
