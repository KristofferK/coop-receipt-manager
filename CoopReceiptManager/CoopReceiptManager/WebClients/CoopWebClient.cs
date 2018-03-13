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

        public void SignIn(CoopCredentials credentials)
        {
            var email = WebUtility.UrlEncode(credentials.Email);
            var password = WebUtility.UrlEncode(credentials.Password);
            var payload = $"EmailOrMemberNumber={email}&Password={password}&FacebookSignedRequest=&HashTag=&RedirectUrl=https%3A%2F%2Fcoop.dk%2F&OpenInNewWindow=False&ActiveType=EmailMemberNumber";

            webClient.Headers[HttpRequestHeader.UserAgent] = "Coop Receipt Manager";
            webClient.Headers[HttpRequestHeader.ContentType] = "application/x-www-form-urlencoded";
            webClient.Headers[HttpRequestHeader.Referer] = "https://coop.dk/login";
            webClient.Headers.Add("Origin", "https://coop.dk");
            webClient.Headers.Add("Accept", "text/html,application/xhtml+xml,application/xml;q=0.9,image/webp,*/*;q=0.8");
            webClient.Encoding = Encoding.UTF8;
            var source = webClient.UploadString("https://coop.dk/login/", payload);

            if (source.Contains("<h1>Log ind</h1>"))
            {
                throw new ArgumentException("Invalid credentials provided");
            }
        }

        public void GetReceipts()
        {
            webClient.Headers[HttpRequestHeader.UserAgent] = "Coop Receipt Manager - https://github.com/KristofferK/coop-receipt-manager/";
            webClient.Headers[HttpRequestHeader.Referer] = "https://medlem.coop.dk/medlemskonto+og+kvitteringer/Kvitteringer";
            webClient.Headers.Add("Accept", "application/json, text/javascript, */*; q=0.01");
            var source = webClient.DownloadString("https://medlem.coop.dk/MemberAccount/GetReceipts?pageIndex=1&pageSize=25&fraDato=13-03-2017&tilDato=13-03-2018");
            Console.WriteLine("Receipts: " + source);
        }
    }
}
