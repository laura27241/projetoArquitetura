using Microsoft.EntityFrameworkCore;
using ResidenciasService.Models;

namespace ResidenciasService.Data
{
    public class ResidenciasContext : DbContext
    {
        public ResidenciasContext(DbContextOptions<ResidenciasContext> options) : base(options) { }

        public DbSet<Residencia> Residencias { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Residencia>().HasKey(p => p.Id);

            base.OnModelCreating(modelBuilder);
        }
    }
}
