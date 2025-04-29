using Inventory_Management_System.Models.Dto;
using Inventory_Management_System.Models.Dto.Authentication;

namespace Inventory_Management_System.Services
{
    public interface IAuthService
    {
        Task<UserDto> RegisterAsync(RegisterDto dto);
        Task<string> LoginAsync(LoginDto dto); // This method should return a JWT token
    }

}
