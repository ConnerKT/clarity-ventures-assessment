using email;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Identity.UI.Services;
using System;
using System.IO;
using System.Threading.Tasks;

public class Program
{
    public static async Task Main(string[] args)
    {
        var config = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
            .Build();

        var serviceProvider = new ServiceCollection()
            .AddLogging(configure => configure.AddConsole())
            .AddSingleton<IConfiguration>(config)
            .AddTransient<IEmailSender, SendEmail>()
            .BuildServiceProvider();

        var emailSender = serviceProvider.GetService<IEmailSender>();
        string email = "recipient@example.com";
        string subject = "Test Email";
        string htmlMessage = "<p>This is a test email.</p>";

        await emailSender.SendEmailAsync(email, subject, htmlMessage);
        Console.WriteLine("Email sent.");
    }
}