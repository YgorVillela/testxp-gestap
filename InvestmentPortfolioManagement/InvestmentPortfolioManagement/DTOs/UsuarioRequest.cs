namespace InvestmentPortfolioManagement.Models
{
    public class UsuarioRequest
    {
        public string Nome { get; set; }
        public string Email { get; set; }
        public List<int> Transacoes { get; set; } = new List<int>();
    }
}
