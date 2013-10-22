using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;

namespace AutoBidder.Attributes
{
    [AttributeUsage(AttributeTargets.Field)]
    class HttpHeader : System.Attribute
    {
        public string HeaderKey;

        public HttpHeader(string key)
        {
            HeaderKey = key;
        }

        public static Dictionary<string, string> GetHeaders(Object obj)
        {
            Dictionary<string, string> ret = new Dictionary<string, string>();

            foreach (FieldInfo fi in obj.GetType().GetFields())
            {
                HttpHeader att = (HttpHeader)fi.GetCustomAttributes(typeof(HttpHeader), true).SingleOrDefault();
                if (att != null)
                {
                    ret.Add(att.HeaderKey, (string)fi.GetValue(obj));
                }
            }

            return ret;
        }
    }
}
