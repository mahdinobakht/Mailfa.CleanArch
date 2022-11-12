using Ardalis.Result;
using Mailfa.CleanArch.Core.Interfaces;
using Mailfa.CleanArch.Core.ProjectAggregate.Helpers;
using Mailfa.CleanArch.Core.ProjectAggregate.Models.drive;
using Mailfa.CleanArch.Core.ProjectAggregate.Models.webMail;
using Mailfa.CleanArch.Core.ProjectAggregate.Specifications.webMail;
using Mailfa.CleanArch.Core.ProjectAggregate.ViewModel;
using Mailfa.CleanArch.SharedKernel.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mailfa.CleanArch.Core.Services
{
    internal class DriveService : IDriveService
    {
        private readonly IRepository<DriveFolder> driveFolderRepository;

        public DriveService(IRepository<DriveFolder> _driveFolderRepository)
        {
            driveFolderRepository = _driveFolderRepository;

        }

        public async Task<Result<GetDriveFolderOutput>> GetFolders(GetFolderInput request)
        {
            var result = new GetDriveFolderOutput();

            try
            {
                var userId = GeneralHelper.GetUserId();
                var foldersSpec = new Drive_FoldersByUserIdSpec(userId);
                var folderData = await driveFolderRepository.ListAsync(foldersSpec);
                if (folderData != null)
                {
                    result.DriveFolders = folderData;
                    return Result<GetDriveFolderOutput>.Success(value: result);
                }
                else
                    return Result<GetDriveFolderOutput>.Success(value: result, successMessage: GeneralHelper.GetResourcesValue("UserFolderNotExist"));

            }
            catch (Exception ex)
            {
                return Result<GetDriveFolderOutput>.Error(ex.Message);
            }



        }

    }
}
