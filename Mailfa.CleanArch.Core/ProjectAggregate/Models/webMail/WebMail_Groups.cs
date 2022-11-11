using hMailServer;
using Mailfa.CleanArch.Core.ProjectAggregate.Models.hMail;
using Mailfa.CleanArch.SharedKernel;
using Mailfa.CleanArch.SharedKernel.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mailfa.CleanArch.Core.ProjectAggregate.Models.webMail
{
    public partial class WebMail_Groups : EntityBase, IAggregateRoot
    {
        public int Group_Id { get; set; }
        public int? Group_DomainId { get; set; }
        public string Group_Name { get; set; }
        public bool? Group_AdvertiseEnable { get; set; }
        public bool? Group_WapEnable { get; set; }
        public bool? Group_Default { get; set; }
        public bool? Group_UpgradeEnable { get; set; }
        public int? Group_Mail_MaxSize { get; set; }
        public byte? Group_Mail_EnableLimits { get; set; }
        public byte? Group_Mail_DisablePOP3 { get; set; }
        public byte? Group_Mail_DisableSMTP { get; set; }
        public byte? Group_Mail_DisableIMAP { get; set; }
        public bool? Group_Mail_GroupSending { get; set; }
        public byte? Group_Mail_POP3AccountLimit { get; set; }
        public bool? Group_Mail_AutoForwardEnable { get; set; }
        public bool? Group_Mail_AutoReplyEnable { get; set; }
        public short? Group_Mail_ReceptionMaxLimit { get; set; }
        public int? Group_Mail_MaxEmailPerHour { get; set; }
        public short? Group_Mail_MaxEmailPerDay { get; set; }
        public short? Group_Mail_MaxSuspendPerEmail { get; set; }
        public short Group_Disk_WebDiskSize { get; set; }
        public bool? Group_Disk_Enable { get; set; }
        public byte? Group_Sms_Enable { get; set; }
        public int? Group_SMS_PersianPrice { get; set; }
        public int? Group_SMS_EnglishPrice { get; set; }
        public bool? Group_SMS_SignatureEnable { get; set; }
        public bool? Group_Membership_AdminApproval { get; set; }
        public bool? Group_Membership_MobileApproval { get; set; }
        public bool? Group_Membership_EmailApproval { get; set; }
        public int? Group_Membership_Price { get; set; }
        public short? Group_Membership_ValidateMonth { get; set; }
        public bool? Group_SpacialUser { get; set; }

        // public virtual HmDomains Group_Domain { get; set; }
    }
}
