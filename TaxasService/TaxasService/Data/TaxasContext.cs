using Microsoft.EntityFrameworkCore;
using TaxasService.Models;

namespace TaxasService.Data
{
    public class TaxasContext : DbContext
    {
        public TaxasContext(DbContextOptions<TaxasContext> options) : base(options) { }

        public DbSet<Taxa> Taxas { get; set; }
    }
}
