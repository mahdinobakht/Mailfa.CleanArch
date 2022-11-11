using Mailfa.CleanArch.SharedKernel.Interfaces;
using Mailfa.CleanArch.SharedKernel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mailfa.CleanArch.Core.ProjectAggregate.hMail
{
    public class HmImapfolders : EntityBase, IAggregateRoot
    {
        public int Folderid { get; set; }
        public int Folderaccountid { get; set; }
        public int Folderparentid { get; set; }
        public string Foldername { get; set; }
        public byte Folderissubscribed { get; set; }
        public DateTime Foldercreationtime { get; set; }
        public long Foldercurrentuid { get; set; }
        public byte Foldersmsenable { get; set; }
    }
}
