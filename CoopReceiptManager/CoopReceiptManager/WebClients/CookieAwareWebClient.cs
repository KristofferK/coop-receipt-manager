using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace CoopReceiptManager.WebClients
{
    internal class CookieAwareWebClient : WebClient
    {
        public CookieContainer CookieContainer { get; private set; }
        public CookieCollection ResponseCookies { get; set; }

        public CookieAwareWebClient()
        {
            CookieContainer = new CookieContainer();
            ResponseCookies = new CookieCollection();
        }

        protected override WebRequest GetWebRequest(Uri address)
        {
            try
            {
                var request = (HttpWebRequest)base.GetWebRequest(address);
                request.CookieContainer = CookieContainer;
                return request;
            }
            catch (Exception e)
            {
                throw new Exception(address.AbsoluteUri + " | " + e.Message);
            }
        }

        protected override WebResponse GetWebResponse(WebRequest request)
        {
            try
            {
                var response = (HttpWebResponse)base.GetWebResponse(request);
                this.ResponseCookies = response.Cookies;
                return response;
            }
            catch (Exception e)
            {
                throw new Exception(request.RequestUri + " | " + e.Message);
            }
        }
    }
}
