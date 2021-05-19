using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace StoreDL.Entities
{
    public partial class BearlyCampingDataContext : DbContext
    {
        public BearlyCampingDataContext()
        {
        }

        public BearlyCampingDataContext(DbContextOptions<BearlyCampingDataContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Account> Accounts { get; set; }
        public virtual DbSet<Inventory> Inventories { get; set; }
        public virtual DbSet<Order> Orders { get; set; }
        public virtual DbSet<Product> Products { get; set; }
        public virtual DbSet<Store> Stores { get; set; }
        public virtual DbSet<Transaction> Transactions { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");

            modelBuilder.Entity<Account>(entity =>
            {
                entity.HasKey(e => e.UserName)
                    .HasName("PK__Accounts__C9F284570F352B70");

                entity.Property(e => e.UserName)
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.Property(e => e.Created).HasColumnType("date");

                entity.Property(e => e.EmployeeId)
                    .HasMaxLength(3)
                    .IsUnicode(false)
                    .HasColumnName("EmployeeID");

                entity.Property(e => e.FirstName)
                    .IsRequired()
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.Property(e => e.LastName)
                    .IsRequired()
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.Property(e => e.UserPassword)
                    .IsRequired()
                    .HasMaxLength(30)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Inventory>(entity =>
            {
                entity.HasKey(e => new { e.StoreId, e.Isbn })
                    .HasName("PK__Inventor__9FC5238F849DFDA7");

                entity.ToTable("Inventory");

                entity.Property(e => e.StoreId).HasColumnName("StoreID");

                entity.Property(e => e.Isbn)
                    .HasMaxLength(13)
                    .IsUnicode(false)
                    .HasColumnName("ISBN");

                entity.HasOne(d => d.IsbnNavigation)
                    .WithMany(p => p.Inventories)
                    .HasForeignKey(d => d.Isbn)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Inventory__ISBN__1A69E950");

                entity.HasOne(d => d.Store)
                    .WithMany(p => p.Inventories)
                    .HasForeignKey(d => d.StoreId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Inventory__Store__1975C517");
            });

            modelBuilder.Entity<Order>(entity =>
            {
                entity.HasKey(e => e.OrderNumber)
                    .HasName("PK__Orders__CAC5E7423255B9BB");

                entity.Property(e => e.DateCreated).HasColumnType("date");

                entity.Property(e => e.StoreId).HasColumnName("StoreID");

                entity.Property(e => e.Total)
                    .HasColumnType("decimal(18, 0)")
                    .HasColumnName("total");

                entity.Property(e => e.UserName)
                    .IsRequired()
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.HasOne(d => d.Store)
                    .WithMany(p => p.Orders)
                    .HasForeignKey(d => d.StoreId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Orders__StoreID__12C8C788");

                entity.HasOne(d => d.UserNameNavigation)
                    .WithMany(p => p.Orders)
                    .HasForeignKey(d => d.UserName)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Orders__UserName__11D4A34F");
            });

            modelBuilder.Entity<Product>(entity =>
            {
                entity.HasKey(e => e.Isbn)
                    .HasName("PK__Products__447D36EBD663E6D0");

                entity.Property(e => e.Isbn)
                    .HasMaxLength(13)
                    .IsUnicode(false)
                    .HasColumnName("ISBN");

                entity.Property(e => e.Price).HasColumnType("money");

                entity.Property(e => e.ProductName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Store>(entity =>
            {
                entity.Property(e => e.StoreId).HasColumnName("StoreID");

                entity.Property(e => e.StoreCity)
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.Property(e => e.StoreState)
                    .HasMaxLength(30)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Transaction>(entity =>
            {
                entity.HasKey(e => new { e.Isbn, e.OrderNumber })
                    .HasName("PK__Transact__78D1689F78F93B21");

                entity.Property(e => e.Isbn)
                    .HasMaxLength(13)
                    .IsUnicode(false)
                    .HasColumnName("ISBN");

                entity.HasOne(d => d.IsbnNavigation)
                    .WithMany(p => p.Transactions)
                    .HasForeignKey(d => d.Isbn)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Transactio__ISBN__15A53433");

                entity.HasOne(d => d.OrderNumberNavigation)
                    .WithMany(p => p.Transactions)
                    .HasForeignKey(d => d.OrderNumber)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Transacti__Order__1699586C");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
