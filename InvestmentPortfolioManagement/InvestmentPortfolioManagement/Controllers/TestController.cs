using Microsoft.AspNetCore.Mvc;
using InvestmentPortfolioManagement.Services;
using System.Threading.Tasks;

namespace InvestmentPortfolioManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestController : ControllerBase
    {
        private readonly EmailService _emailService;

        public TestController(EmailService emailService)
        {
            _emailService = emailService;
        }

        [HttpPost("send-test-email")]
        public async Task<IActionResult> SendTestEmail()
        {
            var to = "ygor.villela35@gmail.com"; // Substitua pelo e-mail real
            var subject = "Test Email";
            var body = "This is a test email sent from the EmailService.";

            try
            {
                await _emailService.SendEmailAsync(to, subject, body);
                return Ok("Email sent successfully.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }
}
