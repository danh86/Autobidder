using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AutoBidder.Helpers
{
    public static class Sleeper
    {
        //Sleeps a random amount of time. Makes requests look a touch more real
        public static void Sleep(int minSleepTime, int maxSleepTime)
        {
            Random rand = new Random(DateTime.Now.Millisecond + DateTime.Now.Second + DateTime.Now.Minute);
            int sleepTime = rand.Next(minSleepTime, maxSleepTime);
            System.Threading.Thread.Sleep(sleepTime);
        }
    }
}
