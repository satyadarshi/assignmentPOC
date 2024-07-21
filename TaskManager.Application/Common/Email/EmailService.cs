using SendGrid;
using SendGrid.Helpers.Mail;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace TaskManager.Application.Common.Email
{
    public class EmailService : IEmailService
    {
        public async Task<bool> SendMail(string assignedTo, string taskName)
        {
            var client = new SendGridClient("YourSendGridAPIKey");
            var from = new EmailAddress("noreply@taskmanager.com", "Task Manager");
            var subject = "New Task Assigned";
            var to = new EmailAddress(assignedTo);
            var plainTextContent = $"You have been assigned a new task: {taskName}";
            var htmlContent = $"<strong>You have been assigned a new task: {taskName}</strong>";
            var msg = MailHelper.CreateSingleEmail(from, to, subject, plainTextContent, htmlContent);

            await client.SendEmailAsync(msg);
            return true;
        }

    }
}
