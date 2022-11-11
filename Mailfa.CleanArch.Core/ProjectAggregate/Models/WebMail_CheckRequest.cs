using Mailfa.CleanArch.SharedKernel.Interfaces;
using Mailfa.CleanArch.SharedKernel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mailfa.CleanArch.Core.ProjectAggregate.Models
{
    public class WebMail_CheckRequest : EntityBase, IAggregateRoot
    {

        public int CR_ID { get; set; }
        public int CR_Type { get; set; }
        public string CR_IP { get; set; }
        public DateTime CR_ExpireDate { get; set; }
        public int CR_Count { get; set; }
    }
}
