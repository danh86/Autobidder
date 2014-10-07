using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AutoBidder.Attributes;

namespace AutoBidder.Requests
{
    public class PlayerBid : RequestBase
    {
        private const string REQ_URL = "https://utas.fut.ea.com/ut/game/fifa15/trade/[--PlayerId--]/bid ";
		
		//session id
        [HttpHeader("X-UT-SID")]
        public string XUtSid = "";
		
        public string PlayerId
        {
            set {RequestUrl = REQ_URL.Replace("[--PlayerId--]", value);}
        }

        public int BidAmount
        {
            set {RequestData = String.Format("{{\"bid\":{0}}}", value);}
        }

        public PlayerBid()
        {
            HttpMethodOverride = "PUT";
			XUtSid = System.Configuration.ConfigurationManager.AppSettings.Get("sessionToken");
        }
    }
}
