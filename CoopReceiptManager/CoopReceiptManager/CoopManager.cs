using CoopReceiptManager.WebClients;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoopReceiptManager
{
    public class CoopManager
    {
        private CoopWebClient webClient = new CoopWebClient();

        public void SignIn(CoopCredentials credentials)
        {
            webClient.SignIn(credentials);
        }
    }
}
