using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Reflection;
using AutoBidder.Attributes;

namespace AutoBidder.Requests
{
    public class RequestBase
    {
        public string RequestUrl = "";

        public string RequestData = "";

        [HttpHeader("Host")]
        public string Host = "utas.fut.ea.com";

        [HttpHeader("Connection")]
        public string Connection = "keep-alive";

        //[HttpHeader("Content-Length")]
        //public string ContentLength = "1";

        [HttpHeader("Origin")]
        public string Origin = "http://www.easports.com";

        [HttpHeader("User-Agent")]
        public string UserAgent = "Mozilla/5.0 (Windows NT 6.1; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/30.0.1599.69 Safari/537.36";

        [HttpHeader("Content-Type")]
        public string ContentType = "application/json";

        [HttpHeader("Accept")]
        public string Accept = "application/json";

        [HttpHeader("X-UT-Embed-Error")]
        public string XUtEmbedError = "true";

        [HttpHeader("X-UT-PHISHING-TOKEN")]
        public string XUtPhishingToken = "";

        [HttpHeader("X-HTTP-Method-Override")]
        public string HttpMethodOverride = "GET";

        [HttpHeader("Referer")]
        public string Referer = "https://www.easports.com/iframe/fut15/bundles/futweb/web/flash/FifaUltimateTeam.swf?cl=144469";

        //[HttpHeader("Accept-Encoding")]
        //public string AcceptEncoding = "gzip,deflate,sdch";

        [HttpHeader("Accept-Language")]
        public string AcceptLanguage = "en-US,en;q=0.8";

        [HttpHeader("Cookie")]
        public string Cookie = "";

        public RequestBase()
        {
            XUtPhishingToken = System.Configuration.ConfigurationManager.AppSettings.Get("phishingToken");
            Cookie = System.Configuration.ConfigurationManager.AppSettings.Get("cookieValue");
        }

        public HttpWebResponse MakeRequest()
        {
            HttpWebRequest req = (HttpWebRequest)HttpWebRequest.Create(new Uri(RequestUrl));

            //Add all of the headers
            AutoBidder.Helpers.WebRequestHelper.AddHttpHeaders(ref req, AutoBidder.Attributes.HttpHeader.GetHeaders(this));

            req.Method = "POST";
            req.AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate;

            //As it's a post request, add the data to post
            if (!String.IsNullOrEmpty(RequestData)) { AutoBidder.Helpers.WebRequestHelper.SetRequestStream(ref req, ref RequestData); }

            //Make the request
            return (HttpWebResponse)req.GetResponse();
            //string respString = AutoBidder.Helpers.WebResponseHelper.ReadResponse(resp);
        }
    }
}
