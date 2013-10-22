using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AutoBidder.Attributes;

namespace AutoBidder.Requests
{
    class DevelopmentBid : RequestBase
    {
        private const string REQ_URL = "https://utas.fut.ea.com/ut/game/fifa14/trade/[--ItemId--]/bid ";
		
		//session id
        [HttpHeader("X-UT-SID")]
        public string XUtSid = "";
		
        public string ItemId
        {
            set {RequestUrl = REQ_URL.Replace("[--ItemId--]", value);}
        }

        public int BidAmount
        {
            set {RequestData = String.Format("{{\"bid\":{0}}}", value);}
        }

        public DevelopmentBid()
        {
            HttpMethodOverride = "PUT";
			XUtSid = System.Configuration.ConfigurationManager.AppSettings.Get("sessionToken");
        }
    }
}
