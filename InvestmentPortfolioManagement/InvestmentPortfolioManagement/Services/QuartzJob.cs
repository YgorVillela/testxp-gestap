using Quartz;
using System;
using System.Threading.Tasks;

namespace InvestmentPortfolioManagement.Services
{
    public class QuartzJob : IJob
    {
        private readonly EmailService _emailService;

        public QuartzJob(EmailService emailService)
        {
            _emailService = emailService;
        }

        public Task Execute(IJobExecutionContext context)
        {
            _emailService.SendEmailAsync("admin@example.com", "Produtos com Vencimento Próximo", "Existem produtos que estão com vencimento próximo.");

            return Task.CompletedTask;
        }
    }
}
