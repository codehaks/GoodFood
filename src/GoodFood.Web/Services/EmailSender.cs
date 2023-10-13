using System.Net;
using System.Net.Mail;
using Microsoft.AspNetCore.Identity.UI.Services;

namespace GoodFood.Web.Services;

public class EmailSender : IEmailSender
{
    private readonly IConfiguration _configuration;

    public EmailSender(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public async Task SendEmailAsync(string email, string subject, string htmlMessage)
    {
        //var client = new SmtpClient("smtp.gmail.com")
        //{
        //    Credentials = new NetworkCredential(_configuration["email:address"], _configuration["email:password"]),
        //    Port = 587,
        //    EnableSsl = true,
        //    UseDefaultCredentials = false,
        //};
        //var address = _configuration["email:address"];

        //var mailMessage = new MailMessage
        //{
        //    From = new MailAddress(address),
        //    IsBodyHtml = true,
        //    Body = htmlMessage,
        //    Subject = subject,
        //};
        //mailMessage.To.Add(email);
        //client.SendAsync(mailMessage, CancellationToken.None);

        await Task.CompletedTask;
    }
}
