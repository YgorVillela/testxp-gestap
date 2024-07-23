using System.Collections.Generic;

namespace InvestmentPortfolioManagement.Models
{
    public class Usuario
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Email { get; set; }
        public List<Transacao> Transacoes { get; set; } = new List<Transacao>();
    }
}
