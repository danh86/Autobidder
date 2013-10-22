using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AutoBidder.Attributes;

namespace AutoBidder.Requests
{
    public class RefreshSession : RequestBase
    {
        private const string REQ_URL = "http://www.easports.com/iframe/fut/p/ut/auth";

        [HttpHeader("X-UT-Route")]
        public string XUtRoute = "https://utas.fut.ea.com:443";

        public RefreshSession()
        {
            RequestUrl = REQ_URL;
            HttpMethodOverride = "POST";
        }
    }
}
