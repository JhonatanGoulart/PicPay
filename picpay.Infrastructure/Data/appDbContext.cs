using Microsoft.EntityFrameworkCore;
using PicPay.Domain.Entities;

namespace PicPay.Infrastructure.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<User> Users => Set<User>();
    public DbSet<Wallet> Wallets => Set<Wallet>();
    public DbSet<Transaction> Transactions => Set<Transaction>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(u => u.Id);
            entity.HasIndex(u => u.Email).IsUnique();
            entity.HasIndex(u => u.CPF_CNPJ).IsUnique();

            entity.HasOne(u => u.Wallet)
                  .WithOne(w => w.User)
                  .HasForeignKey<Wallet>(w => w.UserId);
        });

        modelBuilder.Entity<Wallet>(entity =>
        {
            entity.HasKey(w => w.UserId);
            entity.Property(w => w.Balance).HasColumnType("decimal(18,2)");
        });

        modelBuilder.Entity<Transaction>(entity =>
        {
            entity.HasKey(t => t.Id);
            entity.Property(t => t.Value).HasColumnType("decimal(18,2)");
        });
    }
}
