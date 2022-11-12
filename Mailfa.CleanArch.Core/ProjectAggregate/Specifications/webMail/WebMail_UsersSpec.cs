using Ardalis.Specification;
using Mailfa.CleanArch.Core.ProjectAggregate.Models.webMail;

namespace Mailfa.CleanArch.Core.ProjectAggregate.Specifications.webMail;

public class UserByMobileWithItemsSpec : Specification<WebMail_Users>, ISingleResultSpecification
{
    public UserByMobileWithItemsSpec(string mobileNumber)
    {
        Query
            .Where(p => p.User_Mobile == mobileNumber && p.User_MobileConfirmed == 1);
    }
}


public class UserByAccountIdSpec : Specification<WebMail_Users>, ISingleResultSpecification
{
    public UserByAccountIdSpec(int userAccountId)
    {
        Query
            .Where(p => p.User_AccountID == userAccountId);
    }
}

