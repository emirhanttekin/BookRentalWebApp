using Microsoft.AspNetCore.Identity.UI.Services;

namespace WebApplication2.Data.Context
{
    public class EmailSender : IEmailSender
    {
        public Task SendEmailAsync(string email, string subject, string htmlMessage)
        {
            //Email Gönderme İşlemlerinizi Yapabilirsiniz
           return Task.CompletedTask;
        }
    }
}
