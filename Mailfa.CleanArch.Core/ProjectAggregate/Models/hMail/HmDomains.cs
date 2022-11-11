using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mailfa.CleanArch.Core.ProjectAggregate.Models.webMail;

namespace Mailfa.CleanArch.Core.ProjectAggregate.Models.hMail
{
    public partial class HmDomains
    {
        public HmDomains()
        {
            WebMailGroups = new HashSet<WebMail_Groups>();
        }

        public int Domainid { get; set; }
        public string Domainname { get; set; }
        public byte Domainactive { get; set; }
        public string Domainpostmaster { get; set; }
        public int Domainmaxsize { get; set; }
        public string Domainaddomain { get; set; }
        public int Domainmaxmessagesize { get; set; }
        public byte Domainuseplusaddressing { get; set; }
        public string Domainplusaddressingchar { get; set; }
        public int Domainantispamoptions { get; set; }
        public byte Domainenablesignature { get; set; }
        public byte Domainsignaturemethod { get; set; }
        public string Domainsignatureplaintext { get; set; }
        public string Domainsignaturehtml { get; set; }
        public byte Domainaddsignaturestoreplies { get; set; }
        public byte Domainaddsignaturestolocalemail { get; set; }
        public int Domainmaxnoofaccounts { get; set; }
        public int Domainmaxnoofaliases { get; set; }
        public int Domainmaxnoofdistributionlists { get; set; }
        public int Domainlimitationsenabled { get; set; }
        public int Domainmaxaccountsize { get; set; }
        public string Domaindkimselector { get; set; }
        public string Domaindkimprivatekeyfile { get; set; }
        public int? Domainmsgcount { get; set; }
        public long? Domaintotalmsgsize { get; set; }

        public virtual ICollection<WebMail_Groups> WebMailGroups { get; set; }
    }
}
