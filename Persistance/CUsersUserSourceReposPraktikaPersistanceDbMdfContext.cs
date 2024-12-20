using Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Persistance;

public partial class CUsersUserSourceReposPraktikaPersistanceDbMdfContext : DbContext
{
    public CUsersUserSourceReposPraktikaPersistanceDbMdfContext()
    {
    }

    public CUsersUserSourceReposPraktikaPersistanceDbMdfContext(DbContextOptions<CUsersUserSourceReposPraktikaPersistanceDbMdfContext> options)
        : base(options)
    {
    }

    public virtual DbSet<City> Cities { get; set; }

    public virtual DbSet<MaterialType> MaterialTypes { get; set; }

    public virtual DbSet<Partner> Partners { get; set; }

    public virtual DbSet<PartnerType> PartnerTypes { get; set; }

    public virtual DbSet<Product> Products { get; set; }

    public virtual DbSet<Sale> Sales { get; set; }

    public virtual DbSet<SalePlace> SalePlaces { get; set; }

    public virtual DbSet<Street> Streets { get; set; }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=C:\\Users\\User\\RiderProjects\\Alphapol\\Persistance\\Db.mdf;Integrated Security=True;Connect Timeout=30");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<City>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__City__3214EC075E2F1C36");

            entity.ToTable("City");

            entity.Property(e => e.CityName).HasMaxLength(50);
        });

        modelBuilder.Entity<MaterialType>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Material__3214EC07BFD393C9");

            entity.ToTable("MaterialType");

            entity.Property(e => e.Koef).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.MaterialName).HasMaxLength(50);
        });

        modelBuilder.Entity<Partner>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Partner__3214EC072EC1C19B");

            entity.ToTable("Partner");

            entity.Property(e => e.Discount).HasColumnType("decimal(3, 2)");
            entity.Property(e => e.Inn)
                .HasMaxLength(12)
                .HasColumnName("INN");
            entity.Property(e => e.Phone).HasMaxLength(50);

            entity.HasOne(d => d.IdCityNavigation).WithMany(p => p.Partners)
                .HasForeignKey(d => d.IdCity)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Partner_ToCity");

            entity.HasOne(d => d.IdStreetNavigation).WithMany(p => p.Partners)
                .HasForeignKey(d => d.IdStreet)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Partner_ToStreet");

            entity.HasOne(d => d.IdTypeNavigation).WithMany(p => p.Partners)
                .HasForeignKey(d => d.IdType)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Partner_ToType");
        });

        modelBuilder.Entity<PartnerType>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__PartnerT__3214EC077B3F5042");

            entity.ToTable("PartnerType");

            entity.Property(e => e.PartnerTypeName).HasMaxLength(50);
        });

        modelBuilder.Entity<Product>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Product__3214EC0730448F3D");

            entity.ToTable("Product");

            entity.Property(e => e.MinCost).HasColumnType("decimal(8, 2)");

            entity.HasOne(d => d.IdMaterialTypeNavigation).WithMany(p => p.Products)
                .HasForeignKey(d => d.IdMaterialType)
                .HasConstraintName("FK_Table_ToMaterialType");
        });

        modelBuilder.Entity<Sale>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Sale__3214EC07B5CA05E9");

            entity.ToTable("Sale");

            entity.HasOne(d => d.IdPartnerNavigation).WithMany(p => p.Sales)
                .HasForeignKey(d => d.IdPartner)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Sale_ToPartner");

            entity.HasOne(d => d.IdProductNavigation).WithMany(p => p.Sales)
                .HasForeignKey(d => d.IdProduct)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Sale_ToProduct");

            entity.HasOne(d => d.IdSalePlaceNavigation).WithMany(p => p.Sales)
                .HasForeignKey(d => d.IdSalePlace)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Sale_ToSalePlace");
        });

        modelBuilder.Entity<SalePlace>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__SalePlac__3214EC07FE415BE2");

            entity.ToTable("SalePlace");
        });

        modelBuilder.Entity<Street>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Street__3214EC0726CE2918");

            entity.ToTable("Street");

            entity.Property(e => e.StreetName).HasMaxLength(50);
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__User__3214EC078AC267F0");

            entity.ToTable("User");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
