using Mailfa.CleanArch.SharedKernel.Interfaces;
using Mailfa.CleanArch.SharedKernel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;

namespace Mailfa.CleanArch.Core.ProjectAggregate.Models.drive
{
    [Table("DriveFolder", Schema = "Drive")]
    public class DriveFolder : EntityBase, IAggregateRoot
    {
        public int DFL_ID { get; set; }
        public string? DFL_Name { get; set; }
        public int DFL_ParentID { get; set; }
        public int DFL_Size { get; set; }
        public bool DFL_IsShared { get; set; }
        public int DFL_UserID { get; set; }
        public Int16 DFL_Version { get; set; }
        public DateTime DFL_CreateDate { get; set; }

        

    }
}
