using InvestmentPortfolioManagement.Models;

namespace InvestmentPortfolioManagement.Services
{
    public interface ITransacaoService
    {
        Task<IEnumerable<Transacao>> GetTransacoesAsync();
        Task<Transacao> GetTransacaoByIdAsync(int id);
        Task<Transacao> CreateTransacaoAsync(Transacao transacao);
        Task<Transacao> UpdateTransacaoAsync(Transacao transacao);
        Task<bool> DeleteTransacaoAsync(int id);
        Task<Transacao> BuyProductAsync(CompraVendaRequest request);
        Task<Transacao> SellProductAsync(CompraVendaRequest request);
    }
}
