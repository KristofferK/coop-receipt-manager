using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CoopReceiptManager;
using CoopReceiptManager.Events;
using static Example.ColorfulPrinter;

namespace Example
{
    class Program
    {
        private static CoopManager coopReceiptManager = new CoopManager();

        static void Main(string[] args)
        {
            coopReceiptManager.OnSignIn += SignInHandler;
            coopReceiptManager.OnReceiptsReceived += ReceiptsReceivedHandler;

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
            coopReceiptManager.GetReceipts();
        }

        private static void ReceiptsReceivedHandler(object sender, ReceiptsReceivedEventArgs e)
        {
            if (!e.SuccessfullyFetchedReceipts)
            {
                Print("Failed to load receipts", ConsoleColor.Red);
                return;
            }

            Print("Successfully loaded receipts", ConsoleColor.Green);
            foreach (var receipt in e.Receipts)
            {
                Print(receipt.ToString(), ConsoleColor.Cyan);
            }
        }
    }
}
