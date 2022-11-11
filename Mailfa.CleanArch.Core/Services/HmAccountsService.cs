using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mailfa.CleanArch.Core.Interfaces;
using Mailfa.CleanArch.Core.ProjectAggregate.Helpers;
using Mailfa.CleanArch.Core.ProjectAggregate.hMail;
using Mailfa.CleanArch.Core.ProjectAggregate.Specifications;
using Mailfa.CleanArch.SharedKernel.Interfaces;


namespace Mailfa.CleanArch.Core.Services
{
    class HmAccountsService : IHmAccountsService
    {
        private readonly IRepository<hm_accounts> _hmAccountsRepositroy;
        private readonly IRepository<HmImapfolders> _hmImapfoldersRepository;

     
        public HmAccountsService(IRepository<hm_accounts> hmAccountsRepositroy, IRepository<HmImapfolders> hmImapfoldersRepository)
        {
            _hmAccountsRepositroy = hmAccountsRepositroy;
            _hmImapfoldersRepository = hmImapfoldersRepository;
        }

        public static hMailServer.Application _application = null;
        // ایجاد اتصال به سرور پیش فرض به استفاده از تنظیمات سیستم
        private hMailServer.Application currentServer
        {
            get
            {
                if (_application != null)
                    return _application;

                var serverType = Type.GetTypeFromProgID("hMailServer.Application", GeneralHelper.GetConfigurationKey("Server_IP"));
                _application = (hMailServer.Application)Activator.CreateInstance(serverType);

                _application.Authenticate(GeneralHelper.GetConfigurationKey("Server_UserName"), GeneralHelper.GetConfigurationKey("Server_Password"));
                _application.Connect();

                return _application;
            }
        }

        public int GetDomainId()
        {
            return currentServer.Domains.ItemByName[GeneralHelper.GetConfigurationKey("DefaultDomainName")].ID;
        }

        private string getFullEmail(string username)
        {
            if (username.Contains("@"))
                return username;
            return username + "@" + GeneralHelper.GetConfigurationKey("DefaultDomainName");
        }

        static List<HmImapfolders> GetFolders()
        {
            return new List<HmImapfolders> { new HmImapfolders { Foldername = "Sent Items", Folderissubscribed = 1 }, new HmImapfolders { Foldername = "Drafts", Folderissubscribed = 1 }, new HmImapfolders { Foldername = "Deleted Items", Folderissubscribed = 0 }, new HmImapfolders { Foldername = "Junk E-mail", Folderissubscribed = 1 } };
        }

        public async Task<hMailServer.Account> createNewAccount(string username, string firstname, string lastname, string password, int groupId)
        {
            //var serverType = Type.GetTypeFromProgID("hMailServer.Application", GeneralHelper.GetConfigurationKey("Server_IP"));
            //hMailServer.Application _happlication = (hMailServer.Application)Activator.CreateInstance(serverType);

            var account = currentServer.Authenticate(GeneralHelper.GetConfigurationKey("Server_UserName"), GeneralHelper.GetConfigurationKey("Server_Password"));
            //_happlication.Connect();

            
            account.PersonFirstName = firstname;
            account.PersonLastName = lastname;
            account.Password = password;
            account.Address = getFullEmail(username);
            account.Active = false;
            account.AdminLevel = hMailServer.eAdminLevel.hAdminLevelNormal;

            account.Save();


            var hmAccountSpec = new HmAccountsWithAccountIdSpec(account.ID);
            var accountData = await _hmAccountsRepositroy.FirstOrDefaultAsync(hmAccountSpec);

            var xpo = 1;
            accountData.Accountdomainid = GetDomainId();
            await _hmAccountsRepositroy.UpdateAsync(accountData);
            _hmAccountsRepositroy.SaveChangesAsync();

            var folders = GetFolders();
            foreach (var fold in folders)
            {
                fold.Folderaccountid = account.ID;
                fold.Folderparentid = -1;
                fold.Foldercreationtime = DateTime.Now;
                fold.Foldercurrentuid = 0;
            }
            await _hmImapfoldersRepository.AddRangeAsync(folders);
            //_hmImapfoldersRepository.SaveChangesAsync();

            return account;
        }

        //بررسی آزاد بودن یوزر نیم برروی میل سرور
        //public static bool checkUsernameAvailability(string username, MailFa.Models.mailfaContext _context)
        //{
        //    int dId = GetDomainId();
        //    string addr = getFullEmail(username);
        //    if (!_context.HmAccounts.Any(x => x.Accountdomainid == dId && x.Accountaddress == addr))
        //    {
        //        return true;
        //    }

        //    return false;
        //}

    }
}

//namespace Mailfa.CleanArch.Core.Services
//{
//    internal class 
//    {
//      



//        //internal static string getUserFullName(HmAccounts account)
//        //{
//        //    string name = "";
//        //    if (!string.IsNullOrEmpty(account.Accountpersonfirstname))
//        //    {
//        //        name = account.Accountpersonfirstname;
//        //    }
//        //    if (!string.IsNullOrEmpty(account.Accountpersonlastname))
//        //    {
//        //        if (name != "")
//        //            name += " ";
//        //        name = account.Accountpersonlastname;
//        //    }
//        //    if (name != "")
//        //    {
//        //        name = " ، " + name;
//        //    }
//        //    return name;
//        //}
//        //internal static void checkFolders(HmAccounts account, Models.mailfaContext _context)
//        //{
//        //    try
//        //    {
//        //        var userfolders = _context.HmImapfolders.Where(x => x.Folderaccountid == account.Accountid).ToList();
//        //        userfolders = userfolders.Where(x => x.Foldername != "INBOX").ToList();
//        //        var list = userfolders.Select(x => x.Foldername).ToArray();
//        //        var folders = getFolders().Where(x => !list.Contains(x.Foldername));
//        //        if (folders.Any())
//        //        {
//        //            foreach (var fold in folders)
//        //            {
//        //                fold.Folderaccountid = account.Accountid;
//        //                fold.Folderparentid = -1;
//        //                fold.Foldercreationtime = DateTime.Now;
//        //                fold.Foldercurrentuid = 0;
//        //            }
//        //            _context.AddRange(folders);
//        //            _context.SaveChanges();
//        //        }
//        //    }
//        //    catch { }
//        //}
//    }
//}

