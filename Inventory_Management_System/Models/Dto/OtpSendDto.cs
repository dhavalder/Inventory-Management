using System.ComponentModel.DataAnnotations;

namespace Inventory_Management_System.Models.Dto
{
    public class OtpSendDto
    {
        [Required(ErrorMessage = "Email is required.")]
        [EmailAddress(ErrorMessage = "Invalid email format.")]
        public string Email { get; set; } = string.Empty;
    }
}
