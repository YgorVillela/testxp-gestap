using Microsoft.EntityFrameworkCore;
using InvestmentPortfolioManagement.Models;

namespace InvestmentPortfolioManagement.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) { }

        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Transacao> Transacoes { get; set; }
        public DbSet<ProdutoFinanceiro> ProdutosFinanceiros { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Transacao>()
                .HasOne(t => t.ProdutoFinanceiro)
                .WithMany()
                .HasForeignKey(t => t.ProdutoFinanceiroId);

            modelBuilder.Entity<Transacao>()
                .HasOne(t => t.Usuario)
                .WithMany(u => u.Transacoes)
                .HasForeignKey(t => t.UsuarioId);
        }
    }
}
