using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoopReceiptManager.Events
{
    public class ReceiptDetailsReceivedEventArgs : EventArgs
    {
        public bool SuccessfullyFetchedReceipt { get; private set; }
        public ReceiptDetails ReceiptDetails { get; private set; }
        public string ReceiptId { get; private set; }

        public ReceiptDetailsReceivedEventArgs(string receiptId, ReceiptDetails receiptDetails, bool success)
        {
            ReceiptId = receiptId;
            ReceiptDetails = receiptDetails;
            SuccessfullyFetchedReceipt = success;
        }
    }
}
