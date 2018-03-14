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

        public delegate void ReceiptsReceivedHandler(object sender, ReceiptsReceivedEventArgs e);
        public event ReceiptsReceivedHandler OnReceiptsReceived;

        public delegate void ReceiptDetailsReceivedHandler(object sender, ReceiptDetailsReceivedEventArgs e);
        public event ReceiptDetailsReceivedHandler OnReceiptDetailsReceived;

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
            new Thread(() =>
            {
                var receipts = webClient.GetReceipts();
                var success = receipts != null;
                OnReceiptsReceived?.Invoke(this, new ReceiptsReceivedEventArgs(receipts, success));
            }).Start();
        }

        public void GetReceiptDetails(string receiptId)
        {
            new Thread(() =>
            {
                var receipt = webClient.GetReceiptDetails(receiptId);
                var success = receipt != null;
                OnReceiptDetailsReceived?.Invoke(this, new ReceiptDetailsReceivedEventArgs(receiptId, receipt, success));
            }).Start();
        }
    }
}
