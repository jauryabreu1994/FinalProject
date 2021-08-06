namespace PosLibrary.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class FirstMigration : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.GroupPermissions",
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
                .ForeignKey("dbo.Permissions", t => t.PermissionId, cascadeDelete: true)
                .ForeignKey("dbo.UserGroup", t => t.UserGroupId, cascadeDelete: true)
                .Index(t => t.UserGroupId)
                .Index(t => t.PermissionId);
            
            CreateTable(
                "dbo.Permissions",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
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
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.User", "UserGroupId", "dbo.UserGroup");
            DropForeignKey("dbo.GroupPermissions", "UserGroupId", "dbo.UserGroup");
            DropForeignKey("dbo.GroupPermissions", "PermissionId", "dbo.Permissions");
            DropIndex("dbo.User", new[] { "UserGroupId" });
            DropIndex("dbo.GroupPermissions", new[] { "PermissionId" });
            DropIndex("dbo.GroupPermissions", new[] { "UserGroupId" });
            DropTable("dbo.User");
            DropTable("dbo.UserGroup");
            DropTable("dbo.Permissions");
            DropTable("dbo.GroupPermissions");
        }
    }
}
