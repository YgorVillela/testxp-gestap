namespace InvestmentPortfolioManagement.Models
{
    public class ProdutoFinanceiro
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public decimal Preco { get; set; }
        public DateTime DataVencimento { get; set; }
    }
}
