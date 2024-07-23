using InvestmentPortfolioManagement.Data;
using InvestmentPortfolioManagement.Models;
using InvestmentPortfolioManagement.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace InvestmentPortfolioManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        private readonly IUsuarioService _usuarioService;
        private readonly DataContext _context;

        public UsuarioController(IUsuarioService usuarioService, DataContext context)
        {
            _usuarioService = usuarioService;
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetUsuarios()
        {
            var usuarios = await _usuarioService.GetUsuariosAsync();
            return Ok(usuarios);
        }

        [HttpPost]
        public async Task<IActionResult> CreateUsuario(UsuarioRequest usuarioRequest)
        {
            if (usuarioRequest == null)
            {
                return BadRequest("Usuario is null.");
            }

            var usuario = new Usuario
            {
                Nome = usuarioRequest.Nome,
                Email = usuarioRequest.Email
            };

            try
            {
                _context.Usuarios.Add(usuario);
                await _context.SaveChangesAsync();
                return Ok(usuario);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception: {ex.Message}");
                return StatusCode(500, "Internal server error: " + ex.Message + (ex.InnerException != null ? " Inner exception: " + ex.InnerException.Message : ""));
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetUsuario(int id)
        {
            var usuario = await _context.Usuarios.FindAsync(id);
            if (usuario == null)
            {
                return NotFound();
            }
            return Ok(usuario);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUsuario(int id, [FromBody] UsuarioRequest usuarioRequest)
        {
            if (usuarioRequest == null)
            {
                return BadRequest();
            }

            var usuario = await _context.Usuarios.FindAsync(id);
            if (usuario == null)
            {
                return NotFound("Usuario not found.");
            }

            usuario.Nome = usuarioRequest.Nome;
            usuario.Email = usuarioRequest.Email;

            try
            {
                _context.Entry(usuario).State = EntityState.Modified;
                await _context.SaveChangesAsync();
                return Ok(usuario);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception: {ex.Message}");
                return StatusCode(500, "Internal server error: " + ex.Message + (ex.InnerException != null ? " Inner exception: " + ex.InnerException.Message : ""));
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUsuario(int id)
        {
            var result = await _usuarioService.DeleteUsuarioAsync(id);
            if (!result)
            {
                return NotFound();
            }
            return NoContent();
        }
        [HttpGet("{id}/saldos")]
        public async Task<IActionResult> GetProdutoSaldos(int id)
        {
            try
            {
                var saldos = await _usuarioService.GetProdutoSaldosAsync(id);
                return Ok(saldos);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception: {ex.Message}");
                return StatusCode(500, "Internal server error: " + ex.Message + (ex.InnerException != null ? " Inner exception: " + ex.InnerException.Message : ""));
            }
        }

        [HttpGet("{id}/transacoes")]
        public async Task<IActionResult> GetTransacoes(int id)
        {
            try
            {
                var transacoes = await _usuarioService.GetTransacoesAsync(id);
                return Ok(transacoes);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception: {ex.Message}");
                return StatusCode(500, "Internal server error: " + ex.Message + (ex.InnerException != null ? " Inner exception: " + ex.InnerException.Message : ""));
            }
        }
    }
}
