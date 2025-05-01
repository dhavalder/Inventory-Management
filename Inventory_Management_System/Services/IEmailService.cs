using Inventory_Management_System.Models.Dto;

namespace Inventory_Management_System.Services
{
    public interface IEmailService
    {
        Task SendEmailAsync(EmailDto emailDto);
    }
}
