using System.ComponentModel.DataAnnotations;

namespace Inventory_Management_System.Models.Dto
{
    public class OtpVerifyDto
    {
        [Required(ErrorMessage = "Email is required.")]
        [EmailAddress(ErrorMessage = "Invalid email format.")]
        public string? Email { get; set; }

        [Required(ErrorMessage = "OTP is required.")]
        public string? Otp { get; set; }
    }
}
