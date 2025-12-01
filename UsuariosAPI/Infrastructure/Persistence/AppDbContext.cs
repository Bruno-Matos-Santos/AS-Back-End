using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence;

public class AppDbContext : DbContext
{
    public DbSet<Usuario> Usuarios { get; set; } = null!;

    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Usuario>(entity =>
        {
            
            entity.Property(u => u.Nome).HasMaxLength(100).IsRequired();
            entity.Property(u => u.Telefone).HasMaxLength(20).IsRequired(false);

            
            entity.HasIndex(u => u.Email).IsUnique();
            
            
            entity.Property(u => u.Email)
                .IsRequired()
                .HasMaxLength(320)
                
                .HasConversion(
                    v => v.ToLower(),
                    v => v
                );

            
            entity.Property(u => u.Senha).IsRequired();

            entity.HasQueryFilter(u => u.Ativo == true);

            entity.Property(u => u.DataAtualizacao).IsRequired(false);
        });
    }
}