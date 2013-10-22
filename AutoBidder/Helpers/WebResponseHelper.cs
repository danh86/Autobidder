using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.IO;

namespace AutoBidder.Helpers
{
    public static class WebResponseHelper
    {
        public static string ReadResponse(HttpWebResponse resp)
        {
            if (resp == null) { return ""; }

            string ret = "";
            using (StreamReader sr = new StreamReader(resp.GetResponseStream()))
            {
                ret = sr.ReadToEnd();
            }
            return ret;
        }

        public static string ReadGZipResponse(HttpWebResponse resp)
        {
            if (resp == null) { return ""; }

            //Prepare for decompress
            System.IO.MemoryStream ms = new MemoryStream();
            resp.GetResponseStream().CopyTo(ms);
            System.IO.Compression.GZipStream sr = new System.IO.Compression.GZipStream(ms,
                System.IO.Compression.CompressionMode.Decompress);
            string ret = "";
            using (StreamReader sr1 = new StreamReader(sr))
            {
                ret = sr1.ReadToEnd();
            }

            sr.Close();
            ms.Close();
            sr.Dispose();
            ms.Dispose();
            return ret;
        }
    }
}
