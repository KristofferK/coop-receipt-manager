using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoopReceiptManager.Events
{
    public class ReceiptsReceivedEventArgs : EventArgs
    {
        public bool SuccessfullyFetchedReceipts { get; private set; }
        public IEnumerable<Receipt> Receipts { get; set; }

        public ReceiptsReceivedEventArgs(IEnumerable<Receipt> receipts, bool success)
        {
            Receipts = receipts;
            SuccessfullyFetchedReceipts = success;
        }
    }
}
