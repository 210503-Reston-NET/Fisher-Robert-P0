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
        public virtual DbSet<Customer> Customers { get; set; }
        public virtual DbSet<Employee> Employees { get; set; }
        public virtual DbSet<Inventory> Inventories { get; set; }
        public virtual DbSet<Order> Orders { get; set; }
        public virtual DbSet<Product> Products { get; set; }
        public virtual DbSet<Store> Stores { get; set; }
        public virtual DbSet<Transaction> Transactions { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Server=tcp:net-bearly-camping.database.windows.net,1433;Initial Catalog=BearlyCampingData;Persist Security Info=False;User ID=dbadmin;Password=Dark.13lood22;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");

            modelBuilder.Entity<Account>(entity =>
            {
                entity.HasKey(e => e.UserName)
                    .HasName("PK__Accounts__C9F284571FBA6C3F");

                entity.Property(e => e.UserName)
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.Property(e => e.Created).HasColumnType("date");

                entity.Property(e => e.UserPassword)
                    .IsRequired()
                    .HasMaxLength(30)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Customer>(entity =>
            {
                entity.HasKey(e => e.CustId)
                    .HasName("PK__Customer__049E3A8952FAF981");

                entity.Property(e => e.CustId).HasColumnName("CustID");

                entity.Property(e => e.FirstName)
                    .IsRequired()
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.Property(e => e.LastName)
                    .IsRequired()
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.Property(e => e.UserName)
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.HasOne(d => d.UserNameNavigation)
                    .WithMany(p => p.Customers)
                    .HasForeignKey(d => d.UserName)
                    .HasConstraintName("FK__Customers__UserN__7EF6D905");
            });

            modelBuilder.Entity<Employee>(entity =>
            {
                entity.HasKey(e => e.EmpId)
                    .HasName("PK__Employee__AF2DBA790DE00DF6");

                entity.Property(e => e.EmpId).HasColumnName("EmpID");

                entity.Property(e => e.CustId).HasColumnName("CustID");

                entity.Property(e => e.StartDate).HasColumnType("date");

                entity.Property(e => e.UserName)
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.HasOne(d => d.Cust)
                    .WithMany(p => p.Employees)
                    .HasForeignKey(d => d.CustId)
                    .HasConstraintName("FK__Employees__CustI__02C769E9");

                entity.HasOne(d => d.UserNameNavigation)
                    .WithMany(p => p.Employees)
                    .HasForeignKey(d => d.UserName)
                    .HasConstraintName("FK__Employees__UserN__01D345B0");
            });

            modelBuilder.Entity<Inventory>(entity =>
            {
                entity.HasKey(e => new { e.StoreId, e.Isbn })
                    .HasName("PK__Inventor__9FC5238F4A1BF7FE");

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
                    .HasConstraintName("FK__Inventory__ISBN__1209AD79");

                entity.HasOne(d => d.Store)
                    .WithMany(p => p.Inventories)
                    .HasForeignKey(d => d.StoreId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Inventory__Store__11158940");
            });

            modelBuilder.Entity<Order>(entity =>
            {
                entity.HasKey(e => e.OrderNumber)
                    .HasName("PK__Orders__CAC5E7420E963DE3");

                entity.Property(e => e.CustId).HasColumnName("CustID");

                entity.Property(e => e.DateCreated).HasColumnType("date");

                entity.Property(e => e.StoreId).HasColumnName("StoreID");

                entity.Property(e => e.Total)
                    .HasColumnType("money")
                    .HasColumnName("total");

                entity.HasOne(d => d.Cust)
                    .WithMany(p => p.Orders)
                    .HasForeignKey(d => d.CustId)
                    .HasConstraintName("FK__Orders__CustID__09746778");

                entity.HasOne(d => d.Store)
                    .WithMany(p => p.Orders)
                    .HasForeignKey(d => d.StoreId)
                    .HasConstraintName("FK__Orders__StoreID__0A688BB1");
            });

            modelBuilder.Entity<Product>(entity =>
            {
                entity.HasKey(e => e.Isbn)
                    .HasName("PK__Products__447D36EB844588E7");

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

                entity.Property(e => e.StoreAddress)
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.Property(e => e.StoreName)
                    .HasMaxLength(30)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Transaction>(entity =>
            {
                entity.HasKey(e => new { e.Isbn, e.OrderNumber })
                    .HasName("PK__Transact__78D1689F99600DE0");

                entity.Property(e => e.Isbn)
                    .HasMaxLength(13)
                    .IsUnicode(false)
                    .HasColumnName("ISBN");

                entity.HasOne(d => d.IsbnNavigation)
                    .WithMany(p => p.Transactions)
                    .HasForeignKey(d => d.Isbn)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Transactio__ISBN__0D44F85C");

                entity.HasOne(d => d.OrderNumberNavigation)
                    .WithMany(p => p.Transactions)
                    .HasForeignKey(d => d.OrderNumber)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Transacti__Order__0E391C95");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
