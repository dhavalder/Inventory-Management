using MailKit.Net.Smtp;
using MimeKit;
using Inventory_Management_System.Models.Dto;

public class EmailService
{
    private readonly IConfiguration _config;

    public EmailService(IConfiguration config)
    {
        _config = config;
    }

    public async Task SendEmailAsync(EmailDto emailDto)
    {
        var message = new MimeMessage();
        message.From.Add(new MailboxAddress("Inventory System", _config["EmailSettings:From"]));
        message.To.Add(MailboxAddress.Parse(emailDto.ToEmail));
        message.Subject = emailDto.Subject;

        message.Body = emailDto.IsHtml
            ? new TextPart("html") { Text = emailDto.Body }
            : new TextPart("plain") { Text = emailDto.Body };

        using var client = new SmtpClient();

        try
        {
            await client.ConnectAsync(
                _config["EmailSettings:SmtpServer"],
                int.Parse(_config["EmailSettings:Port"]),
                false
            );

            await client.AuthenticateAsync(
                _config["EmailSettings:Username"],
                _config["EmailSettings:Password"]
            );

            await client.SendAsync(message);
        }
        finally
        {
            await client.DisconnectAsync(true);
        }
    }
}
