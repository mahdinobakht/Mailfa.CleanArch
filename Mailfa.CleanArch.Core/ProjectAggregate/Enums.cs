using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mailfa.CleanArch.Core.ProjectAggregate
{
    internal class Enums
    {
        public enum RequestTypes
        {
            Signup = 1,
            Login = 2,
            CommercialUserRequest = 3,
            ResetPassword = 4,
            PasswordCode = 5,
            ForgetPassword = 21,
            RecoveryMethod = 22,
            RecoveryVerify = 23,
            InviteCode = 24,
            Activate = 25
        }

    }
}
