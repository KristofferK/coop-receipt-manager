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
            coopReceiptManager.OnReceiptDetailsReceived += ReceiptDetailsReceivedHandler;

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

            Print("Successfully signed into " + e.Email + "\n", ConsoleColor.Green);
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

            Print("Total amount: " + e.Receipts.Sum(receipt => receipt.Amount) + "\n", ConsoleColor.Cyan);
            Print("Loading data for " + e.Receipts.First().Id, ConsoleColor.Green);
            coopReceiptManager.GetReceiptDetails(e.Receipts.First().Id);
        }

        private static void ReceiptDetailsReceivedHandler(object sender, ReceiptDetailsReceivedEventArgs e)
        {
            if (!e.SuccessfullyFetchedReceipt)
            {
                Print("Failed to load receipt " + e.ReceiptId, ConsoleColor.Red);
                return;
            }

            Print("Successfully loaded details for receipt " + e.ReceiptId, ConsoleColor.Green);
            Print(e.ReceiptDetails.ToString(), ConsoleColor.Cyan);
        }
    }
}
