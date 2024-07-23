using System;
using System.IO;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;

public class Program
{
    public static async Task Main(string[] args)
    {
        var config = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
            .Build();

        var smtpServer = config["Email:SmtpServer"];
        var smtpPort = int.Parse(config["Email:SmtpPort"]);
        var smtpUser = config["Email:SmtpUser"];
        var smtpPass = config["Email:SmtpPass"];

        var mailMessage = new MailMessage
        {
            From = new MailAddress(smtpUser),
            Subject = "Test Email",
            Body = "This is a test email.",
            IsBodyHtml = true,
        };

        mailMessage.To.Add("ygor.villela35@gmail.com");

        using (var smtpClient = new SmtpClient(smtpServer, smtpPort))
        {
            smtpClient.Credentials = new NetworkCredential(smtpUser, smtpPass);
            smtpClient.EnableSsl = true;
            try
            {
                await smtpClient.SendMailAsync(mailMessage);
                Console.WriteLine("Email sent successfully.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception: {ex.Message}");
            }
        }
    }
}
