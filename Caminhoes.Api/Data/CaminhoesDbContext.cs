using Microsoft.EntityFrameworkCore;
using Caminhoes.Api.Models;

namespace Caminhoes.Api.Data
{
    public class CaminhoesDbContext : DbContext
    {
        public CaminhoesDbContext(DbContextOptions<CaminhoesDbContext> options) : base(options) { }

        public DbSet<Caminhao> Caminhoes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Caminhao>(entity =>
            {
                entity.HasKey(e => e.CodigoChassi);
                entity.HasIndex(e => e.CodigoChassi).IsUnique(); // Garante unicidade no banco
                entity.Property(e => e.Modelo).IsRequired();
                entity.Property(e => e.AnoFabricacao).IsRequired();
                entity.Property(e => e.CodigoChassi).IsRequired();
                entity.Property(e => e.Cor).IsRequired();
                entity.Property(e => e.Planta).IsRequired();
            });
        }
    }
}
