using InvestmentPortfolioManagement.Data;
using InvestmentPortfolioManagement.Models;
using Microsoft.EntityFrameworkCore;

namespace InvestmentPortfolioManagement.Services
{
    public class TransacaoService : ITransacaoService
    {
        private readonly DataContext _context;

        public TransacaoService(DataContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Transacao>> GetTransacoesAsync()
        {
            return await _context.Transacoes.Include(t => t.ProdutoFinanceiro).Include(t => t.Usuario).ToListAsync();
        }

        public async Task<Transacao> GetTransacaoByIdAsync(int id)
        {
            return await _context.Transacoes.Include(t => t.ProdutoFinanceiro).Include(t => t.Usuario).FirstOrDefaultAsync(t => t.Id == id);
        }

        public async Task<Transacao> CreateTransacaoAsync(Transacao transacao)
        {
            _context.Transacoes.Add(transacao);
            await _context.SaveChangesAsync();
            return transacao;
        }

        public async Task<Transacao> UpdateTransacaoAsync(Transacao transacao)
        {
            _context.Entry(transacao).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return transacao;
        }

        public async Task<bool> DeleteTransacaoAsync(int id)
        {
            var transacao = await _context.Transacoes.FindAsync(id);
            if (transacao == null)
            {
                return false;
            }

            _context.Transacoes.Remove(transacao);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<Transacao> BuyProductAsync(CompraVendaRequest request)
        {
            var produtoFinanceiro = await _context.ProdutosFinanceiros.FindAsync(request.ProdutoFinanceiroId);
            var usuario = await _context.Usuarios.FindAsync(request.UsuarioId);

            if (produtoFinanceiro == null || usuario == null)
            {
                throw new Exception("ProdutoFinanceiro or Usuario not found.");
            }

            var transacao = new Transacao
            {
                ProdutoFinanceiroId = request.ProdutoFinanceiroId,
                UsuarioId = request.UsuarioId,
                Quantidade = request.Quantidade,
                ValorTotal = produtoFinanceiro.Preco * request.Quantidade,
                DataTransacao = DateTime.UtcNow
            };

            _context.Transacoes.Add(transacao);
            await _context.SaveChangesAsync();
            return transacao;
        }
        public async Task<Transacao> SellProductAsync(CompraVendaRequest request)
        {
            var produtoFinanceiro = await _context.ProdutosFinanceiros.FindAsync(request.ProdutoFinanceiroId);
            var usuario = await _context.Usuarios.FindAsync(request.UsuarioId);

            if (produtoFinanceiro == null || usuario == null)
            {
                throw new Exception("ProdutoFinanceiro or Usuario not found.");
            }

            var transacao = new Transacao
            {
                ProdutoFinanceiroId = request.ProdutoFinanceiroId,
                UsuarioId = request.UsuarioId,
                Quantidade = -request.Quantidade,
                ValorTotal = produtoFinanceiro.Preco * request.Quantidade,
                DataTransacao = DateTime.UtcNow
            };

            _context.Transacoes.Add(transacao);
            await _context.SaveChangesAsync();
            return transacao;
        }
    }
}
