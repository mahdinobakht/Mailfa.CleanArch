using Microsoft.AspNetCore.Mvc;
using Mailfa.CleanArch.Core.Interfaces;
using Microsoft.AspNetCore.Authorization;
using System.Configuration;
using Microsoft.Extensions.Configuration;
using Mailfa.CleanArch.Core.ProjectAggregate.ViewModel;

namespace Mailfa.CleanArch.Controllers
{
    [Route("user")]
    [ApiController]

    public class UserController : BaseApiController<UserController>
    {
        private readonly IUserService _userService;
        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost("GetVerificationCode")]
        [AllowAnonymous]
        public async Task<IActionResult> GetVerificationCode(string mobileNumber)
        {
            var result = await _userService.GetVerificationCode(mobileNumber);
            return Ok(result);
        }

        [HttpPost("VerifyVerificationCode")]
        [AllowAnonymous]
        public async Task<IActionResult> VerifyVerificationCode(VerifyVerificationCodeInput request)
        {
            var result = await _userService.VerifyVerificationCode(request);
            return Ok(result);
        }


        [HttpPost("Signup")]
        [AllowAnonymous]
        public async Task<IActionResult> Signup(VerifyVerificationCodeInput request)
        {
            var result = await _userService.Signup(request);
            return Ok(result);
        }


    }
}
