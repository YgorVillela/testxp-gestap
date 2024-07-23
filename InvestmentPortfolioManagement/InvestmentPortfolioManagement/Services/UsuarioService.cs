using InvestmentPortfolioManagement.Data;
using InvestmentPortfolioManagement.Models;
using Microsoft.EntityFrameworkCore;

namespace InvestmentPortfolioManagement.Services
{
    public class UsuarioService : IUsuarioService
    {
        private readonly DataContext _context;

        public UsuarioService(DataContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Usuario>> GetUsuariosAsync()
        {
            return await _context.Usuarios.Include(u => u.Transacoes).ToListAsync();
        }

        public async Task<Usuario> GetUsuarioByIdAsync(int id)
        {
            return await _context.Usuarios.Include(u => u.Transacoes).FirstOrDefaultAsync(u => u.Id == id);
        }

        public async Task<Usuario> AddUsuarioAsync(Usuario usuario)
        {
            _context.Usuarios.Add(usuario);
            await _context.SaveChangesAsync();
            return usuario;
        }

        public async Task<Usuario> UpdateUsuarioAsync(Usuario usuario)
        {
            _context.Entry(usuario).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return usuario;
        }

        public async Task<bool> DeleteUsuarioAsync(int id)
        {
            var usuario = await _context.Usuarios.FindAsync(id);
            if (usuario == null)
            {
                return false;
            }

            _context.Usuarios.Remove(usuario);
            await _context.SaveChangesAsync();
            return true;
        }
        public async Task<IEnumerable<ProdutoSaldo>> GetProdutoSaldosAsync(int usuarioId)
        {
            var usuario = await _context.Usuarios.Include(u => u.Transacoes)
                                                 .ThenInclude(t => t.ProdutoFinanceiro)
                                                 .FirstOrDefaultAsync(u => u.Id == usuarioId);

            if (usuario == null)
            {
                throw new Exception("Usuario not found.");
            }

            var saldos = usuario.Transacoes.GroupBy(t => t.ProdutoFinanceiro)
                                           .Select(g => new ProdutoSaldo
                                           {
                                               ProdutoFinanceiro = g.Key,
                                               Quantidade = g.Sum(t => t.Quantidade)
                                           }).ToList();

            return saldos;
        }

        public async Task<IEnumerable<Transacao>> GetTransacoesAsync(int usuarioId)
        {
            var transacoes = await _context.Transacoes
                                           .Where(t => t.UsuarioId == usuarioId)
                                           .Include(t => t.ProdutoFinanceiro)
                                           .ToListAsync();
            return transacoes;
        }
    }
}
