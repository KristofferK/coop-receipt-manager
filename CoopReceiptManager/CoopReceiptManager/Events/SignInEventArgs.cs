using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoopReceiptManager.Events
{
    public class SignInEventArgs : EventArgs
    {
        public bool SuccessfullySignedIn { get; private set; }
        public string Email { get; set; }

        public SignInEventArgs(string email, bool success)
        {
            Email = email;
            SuccessfullySignedIn = success;
        }
    }
}
