using Microsoft.EntityFrameworkCore;

namespace Loterias.Infra.Data.Model
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }

        public DbSet<Estado> Estado { get; set; }
        public DbSet<Municipio> Municipio { get; set; }
    }
}
