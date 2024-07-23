using Microsoft.AspNetCore.Mvc;
using InvestmentPortfolioManagement.Services;
using InvestmentPortfolioManagement.Models;
using InvestmentPortfolioManagement.Data;
using Microsoft.EntityFrameworkCore;

namespace InvestmentPortfolioManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TransacaoController : ControllerBase
    {
        private readonly ITransacaoService _transacaoService;
        private readonly DataContext _context;

        public TransacaoController(ITransacaoService transacaoService, DataContext context)
        {
            _transacaoService = transacaoService ?? throw new ArgumentNullException(nameof(transacaoService));
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        [HttpGet]
        public async Task<IActionResult> GetTransacoes()
        {
            try
            {
                var transacoes = await _transacaoService.GetTransacoesAsync();
                return Ok(transacoes);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetTransacao(int id)
        {
            var transacao = await _transacaoService.GetTransacaoByIdAsync(id);
            if (transacao == null)
            {
                return NotFound();
            }
            return Ok(transacao);
        }

        [HttpPost]
        public async Task<IActionResult> CreateTransacao([FromBody] TransacaoRequest transacaoRequest)
        {
            if (transacaoRequest == null)
            {
                return BadRequest("TransacaoRequest is null.");
            }

            var produtoFinanceiro = await _context.ProdutosFinanceiros.FindAsync(transacaoRequest.ProdutoFinanceiroId);
            var usuario = await _context.Usuarios.FindAsync(transacaoRequest.UsuarioId);

            if (produtoFinanceiro == null || usuario == null)
            {
                return NotFound("ProdutoFinanceiro or Usuario not found.");
            }

            var transacao = new Transacao
            {
                ProdutoFinanceiroId = transacaoRequest.ProdutoFinanceiroId,
                UsuarioId = transacaoRequest.UsuarioId,
                Quantidade = transacaoRequest.Quantidade,
                ValorTotal = transacaoRequest.ValorTotal,
                DataTransacao = transacaoRequest.DataTransacao,
                ProdutoFinanceiro = produtoFinanceiro,
                Usuario = usuario
            };

            try
            {
                _context.Transacoes.Add(transacao);
                await _context.SaveChangesAsync();
                return Ok(transacao);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception: {ex.Message}");
                return StatusCode(500, "Internal server error: " + ex.Message + (ex.InnerException != null ? " Inner exception: " + ex.InnerException.Message : ""));
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateTransacao(int id, [FromBody] TransacaoRequest transacaoRequest)
        {
            if (transacaoRequest == null || transacaoRequest.Id != id)
            {
                return BadRequest();
            }

            var produtoFinanceiro = await _context.ProdutosFinanceiros.FindAsync(transacaoRequest.ProdutoFinanceiroId);
            var usuario = await _context.Usuarios.FindAsync(transacaoRequest.UsuarioId);

            if (produtoFinanceiro == null)
            {
                return NotFound("ProdutoFinanceiro not found.");
            }

            if (usuario == null)
            {
                return NotFound("Usuario not found.");
            }

            var transacao = new Transacao
            {
                Id = transacaoRequest.Id,
                ProdutoFinanceiroId = transacaoRequest.ProdutoFinanceiroId,
                UsuarioId = transacaoRequest.UsuarioId,
                Quantidade = transacaoRequest.Quantidade,
                ValorTotal = transacaoRequest.ValorTotal,
                DataTransacao = transacaoRequest.DataTransacao,
                ProdutoFinanceiro = produtoFinanceiro,
                Usuario = usuario
            };

            try
            {
                _context.Entry(transacao).State = EntityState.Modified;
                await _context.SaveChangesAsync();
                return Ok(transacao);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception: {ex.Message}");
                return StatusCode(500, "Internal server error: " + ex.Message + (ex.InnerException != null ? " Inner exception: " + ex.InnerException.Message : ""));
            }
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTransacao(int id)
        {
            var deleted = await _transacaoService.DeleteTransacaoAsync(id);
            if (!deleted)
            {
                return NotFound();
            }

            return NoContent();
        }

        [HttpPost("buy")]
        public async Task<IActionResult> BuyProduct([FromBody] CompraVendaRequest request)
        {
            if (request == null)
            {
                return BadRequest("Request is null.");
            }

            try
            {
                var transacao = await _transacaoService.BuyProductAsync(request);
                return Ok(transacao);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception: {ex.Message}");
                return StatusCode(500, "Internal server error: " + ex.Message + (ex.InnerException != null ? " Inner exception: " + ex.InnerException.Message : ""));
            }
        }

        [HttpPost("sell")]
        public async Task<IActionResult> SellProduct([FromBody] CompraVendaRequest request)
        {
            if (request == null)
            {
                return BadRequest("Request is null.");
            }

            try
            {
                var transacao = await _transacaoService.SellProductAsync(request);
                return Ok(transacao);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception: {ex.Message}");
                return StatusCode(500, "Internal server error: " + ex.Message + (ex.InnerException != null ? " Inner exception: " + ex.InnerException.Message : ""));
            }
        }
    }
}
