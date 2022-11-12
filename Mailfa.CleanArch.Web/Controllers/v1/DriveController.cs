using Mailfa.CleanArch.Controllers;
using Mailfa.CleanArch.Core.Interfaces;
using Mailfa.CleanArch.Core.ProjectAggregate.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Mailfa.CleanArch.Web.Controllers.v1
{
    [Route("v1/drive")]
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
        public async Task<IActionResult> GetFolders(GetFolderInput request)
        {
            var result = await driveService.GetFolders(request);
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
