using CoopReceiptManager.Events;
using CoopReceiptManager.WebClients;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CoopReceiptManager
{
    public class CoopManager
    {
        private CoopWebClient webClient = new CoopWebClient();

        public delegate void SignInHandler(object sender, SignInEventArgs e);
        public event SignInHandler OnSignIn;

        public void SignIn(CoopCredentials credentials)
        {
            new Thread(() =>
            {
                var successfullySignedIn = webClient.SignIn(credentials);
                OnSignIn?.Invoke(this, new SignInEventArgs(credentials.Email, successfullySignedIn));
            }).Start();
        }

        public void GetReceipts()
        {
            webClient.GetReceipts();
        }

        public void GetReceipt(string receiptId)
        {
            webClient.GetReceipt(receiptId);
        }
    }
}
