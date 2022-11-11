using Ardalis.Result;
using Mailfa.CleanArch.Core.ProjectAggregate;

namespace Mailfa.CleanArch.Core.Interfaces
{
    public interface IHmAccountsService
    {
        int GetDomainId();
        Task<hMailServer.Account> CreateNewAccount(string username, string firstname, string lastname, string password, int groupId);
        hMailServer.Domain GetDomainData(string domainName);
    }

}