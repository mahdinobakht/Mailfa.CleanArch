using Mailfa.CleanArch.SharedKernel.Interfaces;
using Mailfa.CleanArch.SharedKernel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mailfa.CleanArch.Core.ProjectAggregate.hMail
{

    public class hm_accounts : EntityBase, IAggregateRoot 
    {
        public int accountid { get; set; }
        public int Accountdomainid { get; set; }
        public byte Accountadminlevel { get; set; }
        public string Accountaddress { get; set; }
        public string Accountpassword { get; set; }
        public int Accountactive { get; set; }
        public int Accountisad { get; set; }
        public string Accountaddomain { get; set; }
        public string Accountadusername { get; set; }
        public int Accountmaxsize { get; set; }
        public byte Accountvacationmessageon { get; set; }
        public string Accountvacationmessage { get; set; }
        public string Accountvacationsubject { get; set; }
        public byte Accountpwencryption { get; set; }
        public byte Accountforwardenabled { get; set; }
        public string Accountforwardaddress { get; set; }
        public byte Accountforwardkeeporiginal { get; set; }
        public byte Accountenablesignature { get; set; }
        public string Accountsignatureplaintext { get; set; }
        public string Accountsignaturehtml { get; set; }
        public DateTime Accountlastlogontime { get; set; }
        public byte Accountvacationexpires { get; set; }
        public DateTime Accountvacationexpiredate { get; set; }
        public string Accountpersonfirstname { get; set; }
        public string Accountpersonlastname { get; set; }
        public int Accountmaxmsglimit { get; set; }
        public int Accountcountofmsg { get; set; }
        public int Accountlimittime { get; set; }
        public byte Accountenablelimits { get; set; }
        public DateTime Accountblockendtime { get; set; }
        public byte Accountdisablepop3 { get; set; }
        public byte Accountdisableimap { get; set; }
        public byte Accountdisablesmtp { get; set; }
        public string Accountlastloginip { get; set; }
    }
}

