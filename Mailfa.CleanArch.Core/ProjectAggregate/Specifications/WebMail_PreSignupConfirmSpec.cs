using Ardalis.Specification;
using Mailfa.CleanArch.Core.ProjectAggregate.Models.webMail;

namespace Mailfa.CleanArch.Core.ProjectAggregate.Specifications;

public class PreSignupConfirmWithItemsSpec : Specification<WebMail_PreSignupConfirm>, ISingleResultSpecification
{
  public PreSignupConfirmWithItemsSpec(string mobileNumber)
  {
        Query
            .Where(p => p.PSC_Mobile == mobileNumber );
  }
}
