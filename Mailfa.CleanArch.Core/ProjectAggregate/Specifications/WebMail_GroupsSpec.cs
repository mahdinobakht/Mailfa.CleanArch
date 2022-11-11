using Ardalis.Specification;
using Mailfa.CleanArch.Core.ProjectAggregate.Models.webMail;

namespace Mailfa.CleanArch.Core.ProjectAggregate.Specifications;

public class GroupByDefaultAndDomainIdSpec : Specification<WebMail_Groups>, ISingleResultSpecification
{
  public GroupByDefaultAndDomainIdSpec(int domainId)
  {
        Query
            .Where(p => p.Group_Default == true && p.Group_DomainId == domainId);
  }
}

 