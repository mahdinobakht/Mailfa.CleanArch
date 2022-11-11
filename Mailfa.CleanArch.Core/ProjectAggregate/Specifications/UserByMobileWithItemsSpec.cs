using Ardalis.Specification;
using Mailfa.CleanArch.Core.ProjectAggregate.Models;

namespace Mailfa.CleanArch.Core.ProjectAggregate.Specifications;

public class UserByMobileWithItemsSpec : Specification<WebMail_Users>, ISingleResultSpecification
{
  public UserByMobileWithItemsSpec(string mobileNumber)
  {
        Query
            .Where(p => p.User_Mobile == mobileNumber && p.User_MobileConfirmed==1);
  }
}
