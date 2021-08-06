using PosLibrary.Model.Entities.User;
using System;
using System.Data.Entity;
using System.ComponentModel.DataAnnotations.Schema;

namespace PosLibrary.Model.Context
{
    public class MainDbContext : DbContext
    {

        public MainDbContext() : base("name=mssql")
        { }

        public DbSet<GroupPermission> GroupPermission { get; set; }
        public DbSet<Permission> Permission { get; set; }
        public DbSet<User> User { get; set; }
        public DbSet<UserGroup> UserGroup { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            #region Common Entities
            modelBuilder.Properties<int>()
                .Where(a => a.Name == "Id")
                .Configure(c => c.HasColumnType("int").HasColumnName("Id")
                .IsKey()
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity));
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

            #region UserGroups
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

            #endregion
        }
    }
}
