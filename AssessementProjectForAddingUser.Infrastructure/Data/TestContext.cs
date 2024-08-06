using AssessementProjectForAddingUser.Domain.Entity;
using Microsoft.EntityFrameworkCore;

namespace AssessementProjectForAddingUser.Infrastructure.Data;

public partial class TestContext : DbContext
{
    public TestContext()
    {
    }

    public TestContext(DbContextOptions<TestContext> options)
        : base(options)
    {
    }

    public virtual DbSet<UserAddressAnkit> UserAddressAnkits { get; set; }

    public virtual DbSet<UserDetailsAnkit> UserDetailsAnkits { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlServer("Name=DefaultConnection");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<UserAddressAnkit>(entity =>
        {
            entity.HasKey(e => e.AddressId).HasName("PK__userAddr__091C2AFB79D056C0");

            entity.ToTable("userAddress_Ankit");

            entity.Property(e => e.City)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.Country)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.State)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.ZipCode)
                .HasMaxLength(20)
                .IsUnicode(false);

            entity.HasOne(d => d.User).WithMany(p => p.UserAddressAnkits)
                .HasForeignKey(d => d.Userid)
                .HasConstraintName("FK__userAddre__Useri__25A75D29");
        });

        modelBuilder.Entity<UserDetailsAnkit>(entity =>
        {
            entity.HasKey(e => e.UserId).HasName("PK__userDeta__1788CC4CE4838285");

            entity.ToTable("userDetails_Ankit");

            entity.Property(e => e.AlternatePhone)
                .HasMaxLength(12)
                .IsUnicode(false)
                .HasDefaultValueSql("(NULL)");
            entity.Property(e => e.CreatedDate).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.Email)
                .HasMaxLength(30)
                .IsUnicode(false);
            entity.Property(e => e.FirstName)
                .HasMaxLength(30)
                .IsUnicode(false);
            entity.Property(e => e.ImagePath)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.IsActive).HasDefaultValue(false);
            entity.Property(e => e.LastName)
                .HasMaxLength(30)
                .IsUnicode(false);
            entity.Property(e => e.MiddleName)
                .HasMaxLength(30)
                .IsUnicode(false);
            entity.Property(e => e.Password)
                .HasMaxLength(30)
                .IsUnicode(false);
            entity.Property(e => e.Phone)
                .HasMaxLength(12)
                .IsUnicode(false);
            entity.Property(e => e.UpdateDate).HasDefaultValueSql("(getdate())");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
