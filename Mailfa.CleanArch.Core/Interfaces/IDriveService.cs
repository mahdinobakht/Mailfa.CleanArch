using Ardalis.Result;
using Mailfa.CleanArch.Core.ProjectAggregate.Models.drive;
using Mailfa.CleanArch.Core.ProjectAggregate.ViewModel;
using System.Globalization;

namespace Mailfa.CleanArch.Core.Interfaces
{
    public  interface IDriveService
    {
         Task<Result<GetDriveFolderOutput>> GetFolders(GetFolderInput request);

        
       
    }

}