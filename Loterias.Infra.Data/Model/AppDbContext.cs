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

            modelBuilder.Entity<Concurso>()
                        .HasOne(a => a.Resultado)
                        .WithOne(a => a.Concurso)
                        .HasForeignKey<Resultado>(c => c.Id);

            modelBuilder.Entity<Concurso>()
                        .HasOne(a => a.ConcursoLocalidade)
                        .WithOne(a => a.Concurso)
                        .HasForeignKey<ConcursoLocalidade>(c => c.Id);
        }

        public DbSet<Uf> Uf { get; set; }
        public DbSet<Municipio> Municipio { get; set; }
        public DbSet<Usuario> Usuario { get; set; }
        public DbSet<Concurso> Concurso { get; set; }
        public DbSet<ConcursoLocalidade> ConcursoLocalidade { get; set; }
        public DbSet<Premiacao> Premiacao { get; set; }
        public DbSet<PremiacaoLocalidade> PremiacaoLocalidade { get; set; }
        public DbSet<Resultado> Resultado { get; set; }
        public DbSet<ResultadoNumeroSorteado> ResultadoNumeroSorteado { get; set; }
        public DbSet<ConcursoDadosBruto> ConcursoDadosBruto { get; set; }
    }
}
