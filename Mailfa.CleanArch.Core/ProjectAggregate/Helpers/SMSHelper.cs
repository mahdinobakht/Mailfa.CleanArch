using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Intrinsics.X86;
using System.Text;
using System.Threading.Tasks;

namespace Mailfa.CleanArch.Core.ProjectAggregate.Helpers
{
    internal class SMSHelper
    {
        private static string SMS_APIKey = GeneralHelper.GetConfigurationKey("SMS_APIKey");
        public static bool SendVerifyCode(string mobile, string code)
        {
            var sms = new Ghasedak.Core.Api(SMS_APIKey);
            try
            {
                var result = sms.VerifyAsync(1, "mailfaactive", new string[] { mobile }, code).Result;
                return true;
            }
            catch (Exception ex)
            {
                //var xpo = ex.Message;
                return false;
            }
        }

        // ارسال اطلاعات اکانت
        public static void SendAccountInfo(string name, string lastname, string email, string password, string mobile)
        {
            var sms = new Ghasedak.Core.Api(SMS_APIKey);
            try
            {
                name = name.Replace(" ", "");
                lastname = lastname.Replace(" ", "");
                email = email.Replace(" ", "");
                password = password.Replace(" ", "");

                var result = sms.VerifyAsync(1, "mailfaaccount", new string[] { mobile }, param1: name, param2: lastname, param3: email, param4: password).Result;
            }
            catch
            {

            }
        }
    }
}
