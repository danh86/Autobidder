using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AutoBidder.Attributes;

namespace AutoBidder.Requests
{
    public class PlayerSearch : RequestBase
    {
        private const string REQ_URL = "https://utas.fut.ea.com/ut/game/fifa15/transfermarket?num=16&start=[--startNo--]&lev=gold&type=player&macr=650";

        //session id
        [HttpHeader("X-UT-SID")]
        public string XUtSid = "";

        public int startNo
        {
            set { RequestUrl = REQ_URL.Replace("[--startNo--]", value.ToString()); }
        }

        public PlayerSearch()
        {
            RequestUrl = REQ_URL;
            XUtSid = System.Configuration.ConfigurationManager.AppSettings.Get("sessionToken");
        }

        public string GetExampleResponse()
        {
            return System.IO.File.ReadAllText("C:\\Users\\robertp\\Documents\\GitHub\\Autobidder\\AutoBidder\\ExampleResponses\\PlayerResp.txt");
        }
    }
}
