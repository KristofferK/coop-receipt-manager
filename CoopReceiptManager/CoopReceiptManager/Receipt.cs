using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoopReceiptManager
{
    public class Receipt
    {
        public string Id { get; set; }
        public DateTime Date { get; set; }
        public string Store { get; set; }
        public decimal? BonusEarned { get; set; }
        public decimal Amount { get; set; }

        public override string ToString()
        {
            return $"{Date.ToLongDateString()} {Date.ToShortTimeString()} | {Store} | {Amount} | {BonusEarned} | {Id}";
        }
    }
}
