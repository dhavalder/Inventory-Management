using System.ComponentModel.DataAnnotations;

namespace Inventory_Management_System.Models.Dto;

public class EmailDto
{
    [Required(ErrorMessage = "Recipient email is required.")]
    [EmailAddress(ErrorMessage = "Invalid email format.")]
    public string ToEmail { get; set; } = string.Empty;

    [Required(ErrorMessage = "Subject is required.")]
    [StringLength(100, ErrorMessage = "Subject cannot exceed 100 characters.")]
    public string Subject { get; set; } = string.Empty;

    [Required(ErrorMessage = "Message body is required.")]
    public string Body { get; set; } = string.Empty;

    public bool IsHtml { get; set; } = false;
}
