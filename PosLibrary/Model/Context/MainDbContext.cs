using PosLibrary.Model.Entities.Users;
using System;
using System.Data.Entity;
using System.ComponentModel.DataAnnotations.Schema;
using PosLibrary.Model.Entities.Customers;
using PosLibrary.Model.Entities.Fiscal;
using PosLibrary.Model.Entities.Items;
using PosLibrary.Model.Entities.StoreSetting;
using PosLibrary.Model.Entities.Transactions;
using PosLibrary.Model.Entities.Vendors;
using PosLibrary.Model.Entities.Payments;

namespace PosLibrary.Model.Context
{
    public class MainDbContext : DbContext
    {

        public MainDbContext() : base("name=mssql")
        { }

        public DbSet<Customer> Customer { get; set; }

        public DbSet<NcfHistory> NcfHistory { get; set; }
        public DbSet<NcfSequenceDetail> NcfSequenceDetail { get; set; }
        public DbSet<NcfType> NcfType { get; set; }


        public DbSet<Item> Item { get; set; }
        public DbSet<ItemDepartment> ItemDepartment { get; set; }
        public DbSet<ItemDiscount> ItemDiscount { get; set; }
        public DbSet<ItemTax> ItemTax { get; set; }

        public DbSet<Entities.Payments.PaymentMethod> PaymentMethod { get; set; }

        public DbSet<Store> Store { get; set; }

        public DbSet<TransactionHeader> TransactionHeader { get; set; }
        public DbSet<TransactionLines> TransactionLines { get; set; }
        public DbSet<TransactionPayments> TransactionPayments { get; set; }

        public DbSet<GroupPermission> GroupPermission { get; set; }
        public DbSet<Permission> Permission { get; set; }
        public DbSet<User> User { get; set; }
        public DbSet<UserGroup> UserGroup { get; set; }

        public DbSet<Vendor> Vendor { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            #region Common Entities
            modelBuilder.Properties<int>()
                .Where(a => a.Name == "Id")
                .Configure(c => c.HasColumnType("int").HasColumnName("Id")
                .IsKey()
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity));
            #endregion

            #region Store

            #region Store
            modelBuilder.Entity<Store>()
                .ToTable("Store", "dbo")
                .HasKey(a => a.Id);

            modelBuilder.Entity<Store>()
                .Property(a => a.Name)
                .HasMaxLength(55)
                .HasColumnType("varchar")
                .IsRequired();
            modelBuilder.Entity<Store>()
                .Property(a => a.VatNumber)
                .HasMaxLength(55)
                .HasColumnType("varchar")
                .IsRequired();
            modelBuilder.Entity<Store>()
                .Property(a => a.CompanyName)
                .HasMaxLength(55)
                .HasColumnType("varchar")
                .IsRequired();
            modelBuilder.Entity<Store>()
                .Property(a => a.Address)
                .HasMaxLength(255)
                .HasColumnType("varchar");
            modelBuilder.Entity<Store>()
                .Property(a => a.Email)
                .HasMaxLength(100)
                .HasColumnType("varchar");
            modelBuilder.Entity<Store>()
                .Property(a => a.Phone)
                .HasMaxLength(25)
                .HasColumnType("varchar");

            modelBuilder.Entity<Store>()
                .Property(a => a.ReceiptId)
                .HasColumnType("int");
            modelBuilder.Entity<Store>()
                .Property(a => a.CustomerId)
                .HasColumnType("int");
            modelBuilder.Entity<Store>()
                .Property(a => a.VendorId)
                .HasColumnType("int");
            #endregion

            #endregion

            #region Users Entities

            #region User
            modelBuilder.Entity<User>()
                .ToTable("User", "dbo")
                .HasKey(a => a.Id);
            modelBuilder.Entity<User>()
                .Property(a => a.UserId)
                .HasMaxLength(25)
                .HasColumnType("varchar")
                .IsRequired();
            modelBuilder.Entity<User>()
                .Property(a => a.Password)
                .HasColumnType("nvarchar")
                .IsRequired();
            modelBuilder.Entity<User>()
                .Property(a => a.FirstName)
                .HasMaxLength(55)
                .HasColumnType("varchar")
                .IsRequired();
            modelBuilder.Entity<User>()
                .Property(a => a.LastName)
                .HasMaxLength(55)
                .HasColumnType("varchar")
                .IsRequired();
            modelBuilder.Entity<User>()
                .Property(a => a.Address)
                .HasMaxLength(255)
                .HasColumnType("varchar");
            modelBuilder.Entity<User>()
                .Property(a => a.VatNumber)
                .HasMaxLength(25)
                .HasColumnType("varchar");
            modelBuilder.Entity<User>()
                .Property(a => a.Email)
                .HasMaxLength(100)
                .HasColumnType("nvarchar");
            modelBuilder.Entity<User>()
                .Property(a => a.Phone)
                .HasMaxLength(25)
                .HasColumnType("varchar");
            modelBuilder.Entity<User>()
                .Property(a => a.Gender)
                .HasColumnType("tinyint");
            modelBuilder.Entity<User>()
                .Property(a => a.UserGroupId)
                .HasColumnType("int");
            #endregion

            #region User Groups
            modelBuilder.Entity<UserGroup>()
                .ToTable("UserGroup", "dbo")
                .HasKey(a => a.Id);

            modelBuilder.Entity<UserGroup>()
                .Property(a => a.Name)
                .HasMaxLength(100)
                .HasColumnType("varchar")
                .IsRequired();

            modelBuilder.Entity<UserGroup>()
                .HasMany(a => a.Users)
                .WithRequired(b => b.UserGroup)
                .HasForeignKey(c => c.UserGroupId);

            modelBuilder.Entity<UserGroup>()
               .HasMany(a => a.GroupPermissions)
               .WithRequired(b => b.UserGroup)
               .HasForeignKey(c => c.UserGroupId);
            #endregion

            #region Permission
            modelBuilder.Entity<Permission>()
                .ToTable("Permission", "dbo")
                .HasKey(a => a.Id);

            modelBuilder.Entity<Permission>()
                .Property(a => a.Name)
                .HasMaxLength(100)
                .HasColumnType("varchar")
                .IsRequired();
            #endregion

            #region Group Permission
            modelBuilder.Entity<GroupPermission>()
                .ToTable("GroupPermission", "dbo")
                .HasKey(a => a.Id);

            modelBuilder.Entity<GroupPermission>()
                .Property(a => a.PermissionCode)
                .HasMaxLength(50)
                .HasColumnType("nvarchar")
                .IsRequired();
            #endregion

            #endregion

            #region Vendor

            #region Vendor
            modelBuilder.Entity<Vendor>()
                .ToTable("Vendor", "dbo")
                .HasKey(a => a.Id);

            modelBuilder.Entity<Vendor>()
                .Property(a => a.VendorId)
                .HasMaxLength(25)
                .HasColumnType("varchar")
                .IsRequired();
            modelBuilder.Entity<Vendor>()
                .Property(a => a.FirstName)
                .HasMaxLength(55)
                .HasColumnType("varchar")
                .IsRequired();
            modelBuilder.Entity<Vendor>()
                .Property(a => a.LastName)
                .HasMaxLength(55)
                .HasColumnType("varchar")
                .IsRequired();
            modelBuilder.Entity<Vendor>()
                .Property(a => a.Address)
                .HasMaxLength(255)
                .HasColumnType("varchar");
            modelBuilder.Entity<Vendor>()
                .Property(a => a.VatNumber)
                .HasMaxLength(25)
                .HasColumnType("varchar");
            modelBuilder.Entity<Vendor>()
                .Property(a => a.CompanyName)
                .HasMaxLength(100)
                .HasColumnType("nvarchar");
            modelBuilder.Entity<Vendor>()
                .Property(a => a.Phone)
                .HasMaxLength(25)
                .HasColumnType("varchar");

            modelBuilder.Entity<Vendor>()
                .HasMany(a => a.Items)
                .WithRequired(b => b.Vendor)
                .HasForeignKey(c => c.VendorId);
            #endregion

            #endregion

            #region Customer

            #region Customer
            modelBuilder.Entity<Customer>()
                .ToTable("Customer", "dbo")
                .HasKey(a => a.Id);

            modelBuilder.Entity<Customer>()
                .Property(a => a.CustomerId)
                .HasMaxLength(25)
                .HasColumnType("varchar")
                .IsRequired();
            modelBuilder.Entity<Customer>()
                .Property(a => a.FirstName)
                .HasMaxLength(55)
                .HasColumnType("varchar")
                .IsRequired();
            modelBuilder.Entity<Customer>()
                .Property(a => a.LastName)
                .HasMaxLength(55)
                .HasColumnType("varchar")
                .IsRequired();
            modelBuilder.Entity<Customer>()
                .Property(a => a.Address)
                .HasMaxLength(255)
                .HasColumnType("varchar");
            modelBuilder.Entity<Customer>()
                .Property(a => a.VatNumber)
                .HasMaxLength(25)
                .HasColumnType("varchar");
            modelBuilder.Entity<Customer>()
                .Property(a => a.CompanyName)
                .HasMaxLength(100)
                .HasColumnType("nvarchar"); 
            modelBuilder.Entity<Customer>()
                 .Property(a => a.Email)
                 .HasMaxLength(25)
                 .HasColumnType("varchar");
            modelBuilder.Entity<Customer>()
                .Property(a => a.Phone)
                .HasMaxLength(25)
                .HasColumnType("varchar");
            
            modelBuilder.Entity<Customer>()
                .HasMany(a => a.TransactionHeaders)
                .WithRequired(b => b.Customer)
                .HasForeignKey(c => c.CustomerId);
            #endregion

            #endregion

            #region Fiscal

            #region NcfHistory

            modelBuilder.Entity<NcfHistory>()
                .ToTable("NcfHistory", "dbo")
                .HasKey(a => a.Id);

            modelBuilder.Entity<NcfHistory>()
                .Property(a => a.ReceiptId)
                .HasMaxLength(25)
                .HasColumnType("varchar")
                .IsRequired();

            modelBuilder.Entity<NcfHistory>()
                .Property(a => a.NcfTypeId)
                .HasColumnType("int");

            modelBuilder.Entity<NcfHistory>()
                .Property(a => a.NcfNumber)
                .HasMaxLength(25)
                .HasColumnType("varchar")
                .IsRequired();
            modelBuilder.Entity<NcfHistory>()
                .Property(a => a.ReturnReceiptId)
                .HasMaxLength(25)
                .HasColumnType("varchar")
                .IsRequired();
            modelBuilder.Entity<NcfHistory>()
                .Property(a => a.ReturnNcfNumber)
                .HasMaxLength(25)
                .HasColumnType("varchar")
                .IsRequired();
            modelBuilder.Entity<NcfHistory>()
                .Property(a => a.VatNumber)
                .HasMaxLength(25)
                .HasColumnType("varchar")
                .IsRequired();
            modelBuilder.Entity<NcfHistory>()
                .Property(a => a.Company)
                .HasMaxLength(25)
                .HasColumnType("varchar")
                .IsRequired();
            modelBuilder.Entity<NcfHistory>()
                .Property(a => a.TotalAmount)
                .HasColumnType("decimal")
                .IsRequired();
            modelBuilder.Entity<NcfHistory>()
                .Property(a => a.TotalAmountWithTax)
                .HasColumnType("decimal")
                .IsRequired();
            modelBuilder.Entity<NcfHistory>()
                .Property(a => a.TotalTax)
                .HasColumnType("decimal")
                .IsRequired();
            modelBuilder.Entity<NcfHistory>()
                .Property(a => a.TaxExempt)
                .HasColumnType("bit")
                .IsRequired();

            #endregion

            #region NcfSequenceDetail

            modelBuilder.Entity<NcfSequenceDetail>()
                .ToTable("NcfSequenceDetail", "dbo")
                .HasKey(a => a.Id);

            modelBuilder.Entity<NcfSequenceDetail>()
                .Property(a => a.NcfId)
                .HasColumnType("int")
                .IsRequired();

            modelBuilder.Entity<NcfSequenceDetail>()
                .Property(a => a.Serie)
                .HasMaxLength(2)
                .HasColumnType("varchar")
                .IsRequired();

            modelBuilder.Entity<NcfSequenceDetail>()
                .Property(a => a.SeqStart)
                .HasColumnType("int")
                .IsRequired();
            modelBuilder.Entity<NcfSequenceDetail>()
                .Property(a => a.SeqNext)
                .HasColumnType("int")
                .IsRequired();
            modelBuilder.Entity<NcfSequenceDetail>()
                .Property(a => a.SeqEnd)
                .HasColumnType("int")
                .IsRequired();

            modelBuilder.Entity<NcfSequenceDetail>()
                .Property(a => a.DateStart)
                .HasColumnType("datetime")
                .IsRequired();
            modelBuilder.Entity<NcfSequenceDetail>()
                .Property(a => a.DateEnd)
                .HasColumnType("datetime")
                .IsRequired();

            modelBuilder.Entity<NcfSequenceDetail>()
                .Property(a => a.SeqStatus)
                .HasColumnType("int")
                .IsRequired();

            modelBuilder.Entity<NcfSequenceDetail>()
                .Property(a => a.DGIIDescription)
                .HasMaxLength(55)
                .HasColumnType("varchar")
                .IsRequired();

            #endregion

            #region NcfType

            modelBuilder.Entity<NcfType>()
                .ToTable("NcfType", "dbo")
                .HasKey(a => a.Id);

            modelBuilder.Entity<NcfType>()
                .Property(a => a.NcfId)
                .HasColumnType("int")
                .IsRequired();


            modelBuilder.Entity<NcfType>()
                .Property(a => a.IsDefaultSale)
                .HasColumnType("bit")
                .IsRequired();
            modelBuilder.Entity<NcfType>()
                .Property(a => a.IsDefaultCreditMemo)
                .HasColumnType("bit")
                .IsRequired();

            modelBuilder.Entity<NcfType>()
                .Property(a => a.Description)
                .HasMaxLength(50)
                .HasColumnType("varchar")
                .IsRequired();

            modelBuilder.Entity<NcfType>()
                .HasMany(a => a.NcfHistories)
                .WithRequired(b => b.NcfType)
                .HasForeignKey(c => c.NcfTypeId);

            modelBuilder.Entity<NcfType>()
                .HasMany(a => a.NcfSequenceDetails)
                .WithRequired(b => b.NcfType)
                .HasForeignKey(c => c.NcfId);


            #endregion

            #endregion

            #region Items

            #region Item
            modelBuilder.Entity<Item>()
                .ToTable("Item", "dbo")
                .HasKey(a => a.Id);

            modelBuilder.Entity<Item>()
                .Property(a => a.Sku)
                .HasMaxLength(25)
                .HasColumnType("varchar")
                .IsRequired();

            modelBuilder.Entity<Item>()
                .Property(a => a.ItemTaxId)
                .HasColumnType("int");
            modelBuilder.Entity<Item>()
                .Property(a => a.ItemDepartmentId)
                .HasColumnType("int");
            modelBuilder.Entity<Item>()
                .Property(a => a.VendorId)
                .HasColumnType("int");
            modelBuilder.Entity<Item>()
                .Property(a => a.ItemDiscountId)
                .HasColumnType("int");

            modelBuilder.Entity<Item>()
                .Property(a => a.Name)
                .HasMaxLength(55)
                .HasColumnType("varchar")
                .IsRequired();
            modelBuilder.Entity<Item>()
                .Property(a => a.Price)
                .HasColumnType("decimal")
                .IsRequired();

            modelBuilder.Entity<Item>()
                .HasMany(a => a.TransactionLines)
                .WithRequired(b => b.Item)
                .HasForeignKey(c => c.ItemId);
            #endregion

            #region ItemDepartment
            modelBuilder.Entity<ItemDepartment>()
                .ToTable("ItemDepartment", "dbo")
                .HasKey(a => a.Id);

            modelBuilder.Entity<ItemDepartment>()
                .Property(a => a.Name)
                .HasMaxLength(55)
                .HasColumnType("varchar")
                .IsRequired();

            modelBuilder.Entity<ItemDepartment>()
                .HasMany(a => a.Items)
                .WithRequired(b => b.ItemDepartment)
                .HasForeignKey(c => c.ItemDepartmentId);
            #endregion

            #region ItemDiscount
            modelBuilder.Entity<ItemDiscount>()
                .ToTable("ItemDiscount", "dbo")
                .HasKey(a => a.Id);

            modelBuilder.Entity<ItemDiscount>()
                .Property(a => a.Name)
                .HasMaxLength(55)
                .HasColumnType("varchar")
                .IsRequired();

            modelBuilder.Entity<ItemDiscount>()
                .Property(a => a.AmountPercent)
                .HasColumnType("decimal")
                .IsRequired();

            modelBuilder.Entity<ItemDiscount>()
                .HasMany(a => a.Items)
                .WithRequired(b => b.ItemDiscount)
                .HasForeignKey(c => c.ItemDiscountId);
            #endregion

            #region ItemTax
            modelBuilder.Entity<ItemTax>()
                .ToTable("ItemTax", "dbo")
                .HasKey(a => a.Id);

            modelBuilder.Entity<ItemTax>()
                .Property(a => a.Name)
                .HasMaxLength(55)
                .HasColumnType("varchar")
                .IsRequired();

            modelBuilder.Entity<ItemTax>()
                .Property(a => a.AmountPercent)
                .HasColumnType("decimal")
                .IsRequired();

            modelBuilder.Entity<ItemTax>()
                .HasMany(a => a.Items)
                .WithRequired(b => b.ItemTax)
                .HasForeignKey(c => c.ItemTaxId);
            #endregion

            #endregion

            #region PaymentMethod
            #region PaymentMethod
            modelBuilder.Entity<PaymentMethod>()
                .ToTable("PaymentMethod", "dbo")
                .HasKey(a => a.Id);

            modelBuilder.Entity<PaymentMethod>()
                .Property(a => a.Name)
                .HasMaxLength(55)
                .HasColumnType("varchar")
                .IsRequired();

            modelBuilder.Entity<PaymentMethod>()
                .HasMany(a => a.TransactionPayments)
                .WithRequired(b => b.PaymentMethod)
                .HasForeignKey(c => c.PaymentMethodId);
            #endregion

            #endregion


            #region Transactions

            #region TransactionHeader
            modelBuilder.Entity<TransactionHeader>()
                .ToTable("TransactionHeader", "dbo")
                .HasKey(a => a.Id);

            modelBuilder.Entity<TransactionHeader>()
                .Property(a => a.ReceiptId)
                .HasMaxLength(25)
                .HasColumnType("varchar")
                .IsRequired();

            modelBuilder.Entity<TransactionHeader>()
                .Property(a => a.CustomerId)
                .HasColumnType("int");

            modelBuilder.Entity<TransactionHeader>()
                .Property(a => a.DiscountAmount)
                .HasColumnType("decimal")
                .IsRequired();
            modelBuilder.Entity<TransactionHeader>()
                .Property(a => a.TotalAmount)
                .HasColumnType("decimal")
                .IsRequired();
            modelBuilder.Entity<TransactionHeader>()
                .Property(a => a.TaxAmount)
                .HasColumnType("decimal")
                .IsRequired();
            modelBuilder.Entity<TransactionHeader>()
                .Property(a => a.TotalPayment)
                .HasColumnType("decimal")
                .IsRequired();

            modelBuilder.Entity<TransactionHeader>()
                .HasMany(a => a.TransactionLines)
                .WithRequired(b => b.TransactionHeader)
                .HasForeignKey(c => c.TransactionHeaderId);

            modelBuilder.Entity<TransactionHeader>()
                .HasMany(a => a.TransactionPayments)
                .WithRequired(b => b.TransactionHeader)
                .HasForeignKey(c => c.TransactionHeaderId);
            #endregion

            #region TransactionLines
            modelBuilder.Entity<TransactionLines>()
                .ToTable("TransactionLines", "dbo")
                .HasKey(a => a.Id);

            modelBuilder.Entity<TransactionLines>()
                .Property(a => a.ReceiptId)
                .HasMaxLength(25)
                .HasColumnType("varchar")
                .IsRequired();

            modelBuilder.Entity<TransactionLines>()
                .Property(a => a.ItemId)
                .HasColumnType("int");
            modelBuilder.Entity<TransactionLines>()
                .Property(a => a.TransactionHeaderId)
                .HasColumnType("int");

            modelBuilder.Entity<TransactionLines>()
               .Property(a => a.Description)
               .HasMaxLength(100)
               .HasColumnType("varchar")
               .IsRequired();


            modelBuilder.Entity<TransactionLines>()
                .Property(a => a.Quantity)
                .HasColumnType("decimal")
                .IsRequired();
            modelBuilder.Entity<TransactionLines>()
                .Property(a => a.Price)
                .HasColumnType("decimal")
                .IsRequired();
            modelBuilder.Entity<TransactionLines>()
                .Property(a => a.TaxPercent)
                .HasColumnType("decimal")
                .IsRequired();
            modelBuilder.Entity<TransactionLines>()
                .Property(a => a.DiscountPercent)
                .HasColumnType("decimal")
                .IsRequired();
            modelBuilder.Entity<TransactionLines>()
                .Property(a => a.TotalAmount)
                .HasColumnType("decimal")
                .IsRequired();
            modelBuilder.Entity<TransactionLines>()
                .Property(a => a.TaxAmount)
                .HasColumnType("decimal")
                .IsRequired();
            #endregion

            #region TransactionPayments
            modelBuilder.Entity<TransactionPayments>()
                .ToTable("TransactionPayments", "dbo")
                .HasKey(a => a.Id);

            modelBuilder.Entity<TransactionPayments>()
                .Property(a => a.ReceiptId)
                .HasMaxLength(25)
                .HasColumnType("varchar")
                .IsRequired();

            modelBuilder.Entity<TransactionPayments>()
                .Property(a => a.PaymentMethodId)
                .HasColumnType("int");
            modelBuilder.Entity<TransactionPayments>()
                .Property(a => a.TransactionHeaderId)
                .HasColumnType("int");

            
            modelBuilder.Entity<TransactionPayments>()
                .Property(a => a.TotalAmount)
                .HasColumnType("decimal")
                .IsRequired();
            #endregion

            #endregion
        }
    }
}
