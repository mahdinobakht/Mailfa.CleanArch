using Mailfa.CleanArch.SharedKernel.Interfaces;
using Mailfa.CleanArch.SharedKernel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mailfa.CleanArch.Core.ProjectAggregate.Models
{
    public class WebMail_Users : EntityBase, IAggregateRoot
    {

        public int User_Id { get; set; }
        public DateTime User_ExpireDate { get; set; }
        public int User_AccountID { get; set; }
        public int User_GroupID { get; set; }
        public string User_Address { get; set; }
        public string User_PostalCode { get; set; }
        public string User_City { get; set; }
        public string User_Province { get; set; }
        public int User_CountryID { get; set; }
        public string User_Telephone { get; set; }

        public string User_Fax { get; set; }
        public string User_Mobile { get; set; }
        public string User_OptionalEmail { get; set; }
        public int User_QuestionID { get; set; }
        public string User_QuestionAnswer { get; set; }
        public string User_SignupIP { get; set; }
        public DateTime User_RegisterDate { get; set; }
        public byte User_MobileConfirmed { get; set; }
    }
}