using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CoopReceiptManager;
using CoopReceiptManager.Events;

namespace Example
{
    class Program
    {
        static void Main(string[] args)
        {
            var coopReceiptManager = new CoopManager();
            coopReceiptManager.OnSignIn += SignInHandler;
            coopReceiptManager.SignIn(CredentialManager.GetCredentails());

            //coopReceiptManager.GetReceipts();
            //Console.ForegroundColor = ConsoleColor.Cyan;
            //coopReceiptManager.GetReceipt("180313020300040745");

        }

        private static void SignInHandler(object sender, SignInEventArgs e)
        {
            if (!e.SuccessfullySignedIn)
            {
                Print("Failed to sign into " + e.Email, ConsoleColor.Red);
                return;
            }

            Print("Successfully signed into " + e.Email, ConsoleColor.Green);
        }
    }
}
