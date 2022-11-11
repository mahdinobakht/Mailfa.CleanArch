using Mailfa.CleanArch.Controllers;
using Mailfa.CleanArch.Core.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Mailfa.CleanArch.Web.Controllers.v1
{
    [Route("drive")]
    [ApiController]
    public class DriveController : BaseApiController<DriveController>
    {
        private readonly IDriveService driveService;
        public DriveController(IDriveService _driveService)
        {
            driveService = _driveService;
        }



        [HttpPost("GetFolders")]
        [AllowAnonymous]
        public async Task<IActionResult> GetFolders(string mobileNumber)
        {
            var result = await driveService.GetFolders(mobileNumber);
            return Ok(result);
        }


        //[HttpPost("GetFiles")]
        //[AllowAnonymous]
        //public async Task<IActionResult> GetFiles(string mobileNumber)
        //{
        //    var result = await driveService.GetFiles(mobileNumber);
        //    return Ok(result);
        //}


    }
}
