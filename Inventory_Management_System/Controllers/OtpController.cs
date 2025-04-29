using Inventory_Management_System.Models.Dto;
using Inventory_Management_System.Services;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Inventory_Management_System.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OtpController : ControllerBase
    {
        private readonly OtpService _otpService;

        public OtpController(OtpService otpService)
        {
            _otpService = otpService;
        }

        [HttpPost("sendOtp")]
        public async Task<IActionResult> SendOtp([FromBody] OtpSendDto request)

        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (string.IsNullOrEmpty(request.Email))
            {
                return BadRequest(new { Success = false, Message = "Email is required" });
            }

            // Generate OTP for the given email and send it
            var otp = _otpService.GenerateOtp(request.Email);
            await _otpService.SendOtpEmailAsync(request.Email, otp);

            return Ok(new { Success = true, Message = "OTP sent successfully" });
        } 

        [HttpPost("verifyOtp")]
        public IActionResult VerifyOtp([FromBody] OtpVerifyDto dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (string.IsNullOrEmpty(dto.Email) || string.IsNullOrEmpty(dto.Otp))
            {
                return BadRequest(new { Success = false, Message = "Email and OTP are required" });
            }

            var isValid = _otpService.VerifyOtp(dto.Email, dto.Otp);

            if (!isValid)
            {
                return BadRequest(new { Success = false, Message = "Invalid OTP" });
            }

            return Ok(new { Success = true, Message = "OTP verified successfully" });
        }
    }
}
