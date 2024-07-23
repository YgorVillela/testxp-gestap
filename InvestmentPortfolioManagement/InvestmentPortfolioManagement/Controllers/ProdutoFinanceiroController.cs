using InvestmentPortfolioManagement.Models;
using InvestmentPortfolioManagement.Services;
using Microsoft.AspNetCore.Mvc;

namespace InvestmentPortfolioManagement.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProdutoFinanceiroController : ControllerBase
    {
        private readonly IProdutoFinanceiroService _produtoService;

        public ProdutoFinanceiroController(IProdutoFinanceiroService produtoService)
        {
            _produtoService = produtoService;
        }

        [HttpGet]
        public async Task<IActionResult> GetProdutos()
        {
            var produtos = await _produtoService.GetProdutosAsync();
            return Ok(produtos);
        }

        [HttpPost("AdicionarProduto")]
        public async Task<IActionResult> AddProduto(ProdutoFinanceiro produto)
        {
            var novoProduto = await _produtoService.AddProdutoAsync(produto);
            return Ok(novoProduto);
        }

        [HttpPut("AtualizarProduto")]
        public async Task<IActionResult> UpdateProduto(ProdutoFinanceiro produto)
        {
            var produtoAtualizado = await _produtoService.UpdateProdutoAsync(produto);
            return Ok(produtoAtualizado);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduto(int id)
        {
            var result = await _produtoService.DeleteProdutoAsync(id);
            if (!result) return NotFound();
            return Ok();
        }
    }
}
