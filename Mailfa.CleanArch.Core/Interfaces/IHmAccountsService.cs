using Ardalis.Result;
using Mailfa.CleanArch.Core.ProjectAggregate;

namespace Mailfa.CleanArch.Core.Interfaces
{
    public  interface IHmAccountsService
    {
        int GetDomainId();
        Task<hMailServer.Account> createNewAccount(string username, string firstname, string lastname, string password, int groupId);
    }

}