using Mailfa.CleanArch.Core.Interfaces;
using Mailfa.CleanArch.Core.ProjectAggregate.Models.webMail;
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
        private readonly IRepository<WebMail_Users> driveRepository;

        public DriveService(IRepository<WebMail_Users> _driveRepository)
        {
            driveRepository = _driveRepository;

        }

    }
}
