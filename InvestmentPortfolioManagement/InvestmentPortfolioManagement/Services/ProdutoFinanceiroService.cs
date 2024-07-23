using InvestmentPortfolioManagement.Data;
using InvestmentPortfolioManagement.Models;
using Microsoft.EntityFrameworkCore;

namespace InvestmentPortfolioManagement.Services
{
    public class ProdutoFinanceiroService : IProdutoFinanceiroService
    {
        private readonly DataContext _context;

        public ProdutoFinanceiroService(DataContext context)
        {
            _context = context;
        }

        public async Task<List<ProdutoFinanceiro>> GetProdutosAsync()
        {
            return await _context.ProdutosFinanceiros.ToListAsync();
        }

        public async Task<ProdutoFinanceiro> AddProdutoAsync(ProdutoFinanceiro produto)
        {
            _context.ProdutosFinanceiros.Add(produto);
            await _context.SaveChangesAsync();
            return produto;
        }

        public async Task<ProdutoFinanceiro> UpdateProdutoAsync(ProdutoFinanceiro produto)
        {
            _context.ProdutosFinanceiros.Update(produto);
            await _context.SaveChangesAsync();
            return produto;
        }

        public async Task<bool> DeleteProdutoAsync(int id)
        {
            var produto = await _context.ProdutosFinanceiros.FindAsync(id);
            if (produto == null) return false;

            _context.ProdutosFinanceiros.Remove(produto);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
