using Ardalis.Specification;
using Mailfa.CleanArch.Core.ProjectAggregate.hMail;

namespace Mailfa.CleanArch.Core.ProjectAggregate.Specifications.hMail;

public class HmAccountsWithAccountIdSpec : Specification<hm_accounts>, ISingleResultSpecification
{
    public HmAccountsWithAccountIdSpec(int accountid)
    {
        Query
            .Where(p => p.accountid == accountid);
    }
}


public class HmAccountsWithAccountIdAndAddressSpec : Specification<hm_accounts>, ISingleResultSpecification
{
    public HmAccountsWithAccountIdAndAddressSpec(int accountDomainId, string emailAddress)
    {
        Query
            .Where(p => p.Accountdomainid == accountDomainId && p.Accountaddress == emailAddress);
    }
}


