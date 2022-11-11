using Ardalis.Specification;
using Mailfa.CleanArch.Core.ProjectAggregate.Models.webMail;
using static Mailfa.CleanArch.Core.ProjectAggregate.Enums;

namespace Mailfa.CleanArch.Core.ProjectAggregate.Specifications;

public class CheckRequestByIpAndTypeSpec : Specification<WebMail_CheckRequest>, ISingleResultSpecification
{
    public CheckRequestByIpAndTypeSpec(string ip, int requestTypes)
    {
        Query
            .Where(p => p.CR_IP == ip && p.CR_Type == requestTypes);
    }
}


public class CheckRequestByIpAndTypeAndExpireSpec : Specification<WebMail_CheckRequest>, ISingleResultSpecification
{


    public CheckRequestByIpAndTypeAndExpireSpec(string ip, int requestTypes, DateTime expireDate)
    {
        Query
            .Where(p => p.CR_IP == ip && p.CR_Type == requestTypes && p.CR_ExpireDate >= expireDate);
    }

}


