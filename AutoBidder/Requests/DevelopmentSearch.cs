using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AutoBidder.Attributes;

namespace AutoBidder.Requests
{
    class DevelopmentSearch : RequestBase
    {
        private const string REQ_URL = "https://utas.fut.ea.com/ut/game/fifa15/transfermarket?num=16&start=[--startNo--]&type=development&lev=gold";
        
        //session id
        [HttpHeader("X-UT-SID")]
        public string XUtSid = "";

        public int startNo
        {
            set { RequestUrl = RequestUrl.Replace("[--startNo--]", value.ToString()); }
        }
       
        public string category
        {
            set { RequestUrl = RequestUrl + "&cat=" + value; }
        }
        
        public int minBid
        {
            set { RequestUrl = RequestUrl + "&micr=" + value; }
        }
   
        public int maxBid
        {
            set { RequestUrl = RequestUrl + "&macr=" + value; }
        }
       
        public int minBuyout
        {
            set { RequestUrl = RequestUrl + "&minb=" + value; }
        }
        
        public int maxBuyout
        {
            set { RequestUrl = RequestUrl + "&maxb=" + value; }
        }

        public DevelopmentSearch()
        {
            RequestUrl = REQ_URL;
            XUtSid = System.Configuration.ConfigurationManager.AppSettings.Get("sessionToken");
        }
    }
}
