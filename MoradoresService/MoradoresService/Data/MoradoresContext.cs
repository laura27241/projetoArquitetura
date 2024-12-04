using Microsoft.EntityFrameworkCore;
using MoradoresService.Models;

namespace MoradoresService.Data
{
    public class MoradoresContext : DbContext
    {
        public MoradoresContext(DbContextOptions<MoradoresContext> options) : base(options)
        {
        }

        public DbSet<Morador> Moradores { get; set; }
    }
}
