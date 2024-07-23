using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace InvestmentPortfolioManagement.Models
{
    public class Transacao
    {
        public int Id { get; set; }

        [ForeignKey("ProdutoFinanceiro")]
        public int ProdutoFinanceiroId { get; set; }
        public ProdutoFinanceiro? ProdutoFinanceiro { get; set; }

        [ForeignKey("Usuario")]
        public int UsuarioId { get; set; }
        public Usuario? Usuario { get; set; }

        public int Quantidade { get; set; }
        public decimal ValorTotal { get; set; }
        public DateTime DataTransacao { get; set; }
    }
}
