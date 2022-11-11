using Ardalis.Specification;
using Mailfa.CleanArch.Core.ProjectAggregate;
using Mailfa.CleanArch.Core.ProjectAggregate.hMail;

namespace Mailfa.CleanArch.Core.ProjectAggregate.Specifications;

public class HmAccountsWithAccountIdSpec : Specification<hm_accounts>, ISingleResultSpecification
{
  public HmAccountsWithAccountIdSpec(int accountid)
  {
        Query
            .Where(p => p.accountid == accountid);
  }
}
