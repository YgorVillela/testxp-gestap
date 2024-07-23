using InvestmentPortfolioManagement.Models;

namespace InvestmentPortfolioManagement.Services
{
    public interface IUsuarioService
    {
        Task<IEnumerable<Usuario>> GetUsuariosAsync();
        Task<Usuario> GetUsuarioByIdAsync(int id);
        Task<Usuario> AddUsuarioAsync(Usuario usuario);
        Task<Usuario> UpdateUsuarioAsync(Usuario usuario);
        Task<bool> DeleteUsuarioAsync(int id);
        Task<IEnumerable<ProdutoSaldo>> GetProdutoSaldosAsync(int usuarioId);
        Task<IEnumerable<Transacao>> GetTransacoesAsync(int usuarioId);
    }
}
