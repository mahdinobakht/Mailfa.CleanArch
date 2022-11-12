using Ardalis.Specification;
using Mailfa.CleanArch.Core.ProjectAggregate.Models.drive;
using Mailfa.CleanArch.Core.ProjectAggregate.Models.webMail;
using static Mailfa.CleanArch.Core.ProjectAggregate.Enums;

namespace Mailfa.CleanArch.Core.ProjectAggregate.Specifications.webMail;

public class Drive_FoldersByUserIdSpec : Specification<DriveFolder>, ISingleResultSpecification
{
    public Drive_FoldersByUserIdSpec(int userId)
    {
        Query
            .Where(p => p.DFL_UserID == userId);
    }
}



