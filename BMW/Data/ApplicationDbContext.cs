using BMW.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace BMW.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options) { }

        // Tabelas
        public DbSet<Utilizador> Utilizador { get; set; }
        public DbSet<Veiculo> Veiculo { get; set; }
        public DbSet<Relatorio> Relatorio { get; set; }
        //public DbSet<FaseDeMontagem> FaseDeMontagem { get; set; }
        public DbSet<Estado> Estado { get; set; }
        public DbSet<Encomenda> Encomenda { get; set; }
        public DbSet<Funcionario> Funcionario { get; set; }
        public DbSet<Posicao> Posicao { get; set; }
        /*
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            // Configuração de chaves compostas
            modelBuilder.Entity<Encomenda>()
                .HasKey(e => e.IdEncomenda);

            modelBuilder.Entity<Funcionario>()
                .HasOne<Utilizador>()
                .WithMany()
                .HasForeignKey(f => f.IdFuncionario)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Relatorio>()
                .HasOne<Funcionario>()
                .WithMany()
                .HasForeignKey(r => r.IdFuncionario)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<FaseDeMontagem>()
                .HasKey(f => f.IdEstadoDeMontagem);

            modelBuilder.Entity<Estado>()
                .HasKey(e => e.IdEstado);

            base.OnModelCreating(modelBuilder);
        }*/
    }
}
