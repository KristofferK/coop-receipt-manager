using CoopReceiptManager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Example
{
    internal static class CredentialManager
    {
        public static CoopCredentials GetCredentails()
        {
            return new CoopCredentials()
            {
                Email = "example@gmail.com",
                Password = "12456"
            };
        }
    }
}
