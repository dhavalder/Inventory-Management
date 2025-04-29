using MailKit.Net.Smtp;
using MimeKit;

namespace Inventory_Management_System.Services
{
    public class OtpService
    {
        private readonly IConfiguration _config;
        private static readonly Dictionary<string, (string Otp, DateTime Expiry)> _otpStorage = new();

        public OtpService(IConfiguration config)
        {
            _config = config;
        }

        public string GenerateOtp(string email)
        {
            var otp = new Random().Next(100000, 999999).ToString();
            _otpStorage[email] = (otp, DateTime.UtcNow.AddMinutes(5)); 
            return otp;
        }

        public async Task SendOtpEmailAsync(string email, string otp)
        {
            if (string.IsNullOrEmpty(email))
                throw new ArgumentNullException(nameof(email), "Email is required.");

            var message = new MimeMessage();
            message.From.Add(new MailboxAddress("Inventory System", _config["EmailSettings:From"]));
            message.To.Add(new MailboxAddress("", email));
            message.Subject = "Your OTP Code";
            message.Body = new TextPart("plain")
            {
                Text = $"Your OTP code is: {otp}"
            };

            using var client = new SmtpClient();
            await client.ConnectAsync(_config["EmailSettings:SmtpServer"], int.Parse(_config["EmailSettings:Port"]), false);
            await client.AuthenticateAsync(_config["EmailSettings:Username"], _config["EmailSettings:Password"]);
            await client.SendAsync(message);
            await client.DisconnectAsync(true);
        }

        public bool VerifyOtp(string email, string otp)
        {
            if (_otpStorage.TryGetValue(email, out var otpEntry))
                return otpEntry.Otp == otp && otpEntry.Expiry > DateTime.UtcNow;

            return false;
        }
    }
}
