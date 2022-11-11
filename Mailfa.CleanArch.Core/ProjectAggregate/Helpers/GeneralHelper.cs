using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;
using System.Globalization;
using System.Reflection;
using Microsoft.Extensions.Configuration;
using System.ComponentModel.DataAnnotations;
using Mailfa.CleanArch.Core.Properties;

namespace Mailfa.CleanArch.Core.ProjectAggregate.Helpers
{
    public static class GeneralHelper
    {
        public static string GetLocalIPAddress()
        {
            var host = Dns.GetHostEntry(Dns.GetHostName());
            foreach (var ip in host.AddressList)
            {
                if (ip.AddressFamily == AddressFamily.InterNetwork)
                {
                    return ip.ToString();
                }
            }
            throw new Exception("No network adapters with an IPv4 address in the system!");
        }

        public static string ComputeHash(string text)
        {
            using (var sha256 = SHA256.Create())
            {
                // Send a sample text to hash.  
                var hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes("hello world"));
                // Get the hashed string.  
                var hash = BitConverter.ToString(hashedBytes).Replace("-", "").ToLower();
                // Print the string.   
                return hash;
            }

        }

        public static string GetResourcesValue(string name)
        {
            string result = "";

            try
            {
                System.Resources.ResourceManager myResourceClass = new System.Resources.ResourceManager(typeof(Resources));
                result = myResourceClass.GetString(name);
            }
            catch
            { }

            return result;
        }

        public static string GetConfigurationKey(string keyName)
        {
            string result = "";
            try
            {
                result = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build().GetSection("ConfigurationKey")[keyName];
            }
            catch
            { }
            return result;
        }

        public static bool IsValidEmailAddress(string email)
        {
            var attr = new EmailAddressAttribute();
            return attr.IsValid(email);
        }

    }
}
