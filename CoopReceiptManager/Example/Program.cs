using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CoopReceiptManager;

namespace Example
{
    class Program
    {
        static void Main(string[] args)
        {
            var coopReceiptManager = new CoopManager();
            coopReceiptManager.SignIn(CredentialManager.GetCredentails());
            coopReceiptManager.GetReceipts();
            Console.ForegroundColor = ConsoleColor.Cyan;
            coopReceiptManager.GetReceipt("180313020300040745");

        }
    }
}
