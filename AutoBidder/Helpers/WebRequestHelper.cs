using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.IO;

namespace AutoBidder.Helpers
{
    public static class WebRequestHelper
    {
        public static void AddHttpHeaders(ref HttpWebRequest req, Dictionary<string, string> dic)
        {
            if (dic == null || req == null) { return; }

            foreach (KeyValuePair<string, string> kvp in dic)
            {
                //.net doesn't allow me to add certain headers to the collection, have to do it via
                //their stupid objects, sigh....
                switch (kvp.Key)
                {
                    case "Host":
                        req.Host = kvp.Value;
                        break;
                    case "Connection":
                        if (String.Compare(kvp.Value, "keep-alive", true) == 0)
                        {
                            req.KeepAlive = true;
                        }
                        else
                        {
                            req.Connection = kvp.Value;
                        }
                        break;
                    case "Content-Length":
                        req.ContentLength = long.Parse(kvp.Value);
                        break;
                    case "User-Agent":
                        req.UserAgent = kvp.Value;
                        break;
                    case "Content-Type":
                        req.ContentType = kvp.Value;
                        break;
                    case "Accept":
                        req.Accept = kvp.Value;
                        break;
                    case "Referer":
                        req.Referer = kvp.Value;
                        break;
                    default:
                        req.Headers.Add(kvp.Key, kvp.Value);
                        break;
                }
            }
        }

        public static void SetRequestStream(ref HttpWebRequest req, ref string toAdd)
        {
            Stream reqStream = req.GetRequestStream();
            StreamWriter sw = new StreamWriter(reqStream);
            sw.Write(toAdd);
            sw.Close();
            reqStream.Close();
            sw.Dispose();
            reqStream.Dispose();
        }
    }
}
