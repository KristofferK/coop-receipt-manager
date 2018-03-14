using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace CoopReceiptManager.WebClients
{
    internal class CoopWebClient
    {
        private CookieAwareWebClient webClient = new CookieAwareWebClient();

        public CoopWebClient()
        {
            webClient.Headers.Add("User-Agent", "Coop Receipt Manager - https://github.com/KristofferK/coop-receipt-manager/");
            webClient.Encoding = Encoding.UTF8;
        }

        public bool SignIn(CoopCredentials credentials)
        {
            var email = WebUtility.UrlEncode(credentials.Email);
            var password = WebUtility.UrlEncode(credentials.Password);
            var payload = $"EmailOrMemberNumber={email}&Password={password}&FacebookSignedRequest=&HashTag=&RedirectUrl=https%3A%2F%2Fcoop.dk%2F&OpenInNewWindow=False&ActiveType=EmailMemberNumber";
            
            webClient.Headers["Content-Type"] = "application/x-www-form-urlencoded";
            webClient.Headers["Referer"] = "https://coop.dk/login";
            webClient.Headers["Accept"] = "text/html,application/xhtml+xml,application/xml;q=0.9,image/webp,*/*;q=0.8";

            var source = webClient.UploadString("https://coop.dk/login/", payload);
            webClient.Headers.Remove("Referer");
            webClient.Headers.Remove("Accept");
            webClient.Headers.Remove("Content-Type");

            if (source.Contains("<h1>Log ind</h1>"))
            {
                return false;
            }

            return true;
        }

        public IEnumerable<Receipt> GetReceipts()
        {
            string source;
            try
            {
                webClient.Headers["Referer"] = "https://medlem.coop.dk/medlemskonto+og+kvitteringer/Kvitteringer";
                webClient.Headers["Accept"] = "application/json, text/javascript, */*; q=0.01";
                webClient.Headers["X-Requested-With"] = "XMLHttpRequest";

                source = webClient.DownloadString("https://medlem.coop.dk/MemberAccount/GetReceipts?pageIndex=1&pageSize=25&fraDato=01-01-2010&tilDato=31-12-2100");

                var receipts = JObject.Parse(source)["Collection"];
                return receipts.Select(e => new Receipt()
                {
                    Id = e["Id"].ToString(),
                    Amount = (decimal)e["Amount"],
                    BonusEarned = (decimal?)e["BonusLoadedAmount"],
                    Store = e["StoreName"].ToString(),
                    Date = (DateTime)e["PurchaseDate"]
                });
            }
            catch
            {
                return null;
            }
            finally
            {
                webClient.Headers.Remove("Referer");
                webClient.Headers.Remove("Accept");
                webClient.Headers.Remove("X-Requested-With");
            }
        }

        public void GetReceipt(string receiptId)
        {
            webClient.Headers["Referer"] = "https://medlem.coop.dk/medlemskonto+og+kvitteringer/Kvitteringer";
            webClient.Headers["Accept"] = "text/html, */*; q=0.01";
            webClient.Headers["X-Requested-With"] = "XMLHttpRequest";
            webClient.Headers["X-fancyBox"] = "true";

            var source = webClient.DownloadString("https://medlem.coop.dk/MemberAccount/GetReceiptDetails?receiptId=" + receiptId);
            webClient.Headers.Remove("Referer");
            webClient.Headers.Remove("Accept");
            webClient.Headers.Remove("X-Requested-With");
            webClient.Headers.Remove("X-fancyBox");

            Console.WriteLine("Receipt: " + source);
        }
    }
}
