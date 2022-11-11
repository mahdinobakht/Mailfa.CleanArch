using Mailfa.CleanArch.SharedKernel.Interfaces;
using Mailfa.CleanArch.SharedKernel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mailfa.CleanArch.Core.ProjectAggregate.Models
{
    public class WebMail_PreSignupConfirm : EntityBase, IAggregateRoot
    {

        public int PSC_Id { get; set; }
        public string PSC_Mobile { get; set; }
        public string PSC_Code { get; set; }
        public string PSC_IP { get; set; }
        public DateTime PSC_CreateDate { get; set; }
        public byte PSC_TryCount { get; set; }
        public bool PSC_Valid { get; set; }
        public string PSC_Token { get; set; }
    }
}
