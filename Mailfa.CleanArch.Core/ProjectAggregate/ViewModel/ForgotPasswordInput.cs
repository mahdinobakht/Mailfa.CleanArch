using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mailfa.CleanArch.Core.ProjectAggregate.ViewModel
{
    public class ForgotPasswordInput
    {
        public string Email { get; set; }
        public string? Captcha { get; set; }
    }
}
