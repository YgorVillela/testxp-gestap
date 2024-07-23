public class TransacaoRequest
{
    public int Id { get; set; }
    public int ProdutoFinanceiroId { get; set; }
    public int UsuarioId { get; set; }
    public int Quantidade { get; set; }
    public decimal ValorTotal { get; set; }
    public DateTime DataTransacao { get; set; }
}
