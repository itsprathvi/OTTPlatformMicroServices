using Event.Bus;
using MailKit.Net.Smtp;
using MassTransit;
using MimeKit;
using System.Diagnostics;

namespace Consumer
{
    public class MessageConsumer : IConsumer<RentMovieEvent>
    {
        public async Task Consume(ConsumeContext<RentMovieEvent> context)
        {
            SendEmailAsync(context.Message);
            Debug.WriteLine(context.Message);
        }

        public async Task SendEmailAsync(RentMovieEvent message)
        {
            //To see this email, browse to http://localhost:8025/ (MailHog Web UI) after running MailHog in Docker.
            //This does not send an actual email, but captures it in MailHog for testing purposes.
            var email = new MimeMessage();

            email.From.Add(new MailboxAddress("Training App", "no-reply@training.local"));
            email.To.Add(MailboxAddress.Parse("recepient@company.com"));
            email.Subject = "Training Email";

            email.Body = new TextPart("plain")
            {
                Text = "This email was captured by MailHog running in Docker." +
                $"This cart checkout was done by {message.UserName}. The total cart amount is INR 			{message.TotalPrice}."
            };

            using var smtp = new SmtpClient();
            await smtp.ConnectAsync("mailhog", 1025, false);
            await smtp.SendAsync(email);
            await smtp.DisconnectAsync(true);
        }
    }
}
