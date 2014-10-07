using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AutoBidderConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            AutoBidder.Controller c = new AutoBidder.Controller();
            c.RunTest();
            //c.PremBid("");
            //c.FitnessBid();
            //c.FitnessBuyout();
        }
    }
}
