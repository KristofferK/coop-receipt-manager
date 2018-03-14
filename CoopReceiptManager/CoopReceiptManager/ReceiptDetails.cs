using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoopReceiptManager
{
    public class ReceiptDetails
    {
        public string Content { get; set; }

        public override string ToString()
        {
            return Content;
        }
    }
}
