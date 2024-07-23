using Microsoft.EntityFrameworkCore;
using InvestmentPortfolioManagement.Data;
using InvestmentPortfolioManagement.Services;
using System.Text.Json.Serialization;
using Quartz.Spi;
using Quartz.Impl;
using Quartz;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Configuração do EmailService
builder.Services.AddSingleton<EmailService>(provider =>
{
    var smtpServer = builder.Configuration["Email:SmtpServer"];
    var smtpPort = int.Parse(builder.Configuration["Email:SmtpPort"]);
    var smtpUser = builder.Configuration["Email:SmtpUser"];
    var smtpPass = builder.Configuration["Email:SmtpPass"];
    return new EmailService(smtpServer, smtpPort, smtpUser, smtpPass);
});

builder.Services.AddDbContext<DataContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddScoped<IProdutoFinanceiroService, ProdutoFinanceiroService>();
builder.Services.AddScoped<IUsuarioService, UsuarioService>();
builder.Services.AddScoped<ITransacaoService, TransacaoService>();

// Configure Quartz
builder.Services.AddSingleton<IJobFactory, SingletonJobFactory>();
builder.Services.AddSingleton<ISchedulerFactory, StdSchedulerFactory>();
builder.Services.AddSingleton<QuartzJob>();
builder.Services.AddSingleton(new JobSchedule(
    jobType: typeof(QuartzJob),
    cronExpression: "0 0 9 * * ?"));

builder.Services.AddHostedService<QuartzHostedService>();

// Configuração do Swagger
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Investment Portfolio Management API", Version = "v1" });
});

// Controllers serialização JSON
builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.Preserve;
        options.JsonSerializerOptions.MaxDepth = 64;
    });
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();
