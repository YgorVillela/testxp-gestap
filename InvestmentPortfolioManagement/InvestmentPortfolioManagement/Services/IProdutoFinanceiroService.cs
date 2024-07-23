using InvestmentPortfolioManagement.Models;

namespace InvestmentPortfolioManagement.Services
{
    public interface IProdutoFinanceiroService
    {
        Task<List<ProdutoFinanceiro>> GetProdutosAsync();
        Task<ProdutoFinanceiro> AddProdutoAsync(ProdutoFinanceiro produto);
        Task<ProdutoFinanceiro> UpdateProdutoAsync(ProdutoFinanceiro produto);
        Task<bool> DeleteProdutoAsync(int id);
    }
}
