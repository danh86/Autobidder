﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;

namespace AutoBidder
{
    public class Controller
    {
        public void RunTest()
        {
            AutoBidder.Requests.PlayerSearch ps = new AutoBidder.Requests.PlayerSearch();
            string s = ps.GetExampleResponse();
            var v = JsonConvert.DeserializeObject<AutoBidder.Entities.RootObject>(s);

            //System.Net.HttpWebResponse wr1 = ps.MakeRequest();
            //string s1 = AutoBidder.Helpers.WebResponseHelper.ReadResponse(wr1);

            //AutoBidder.Requests.RefreshSession rs = new AutoBidder.Requests.RefreshSession();
            //System.Net.HttpWebResponse wr2 = rs.MakeRequest();
            //string s2 = AutoBidder.Helpers.WebResponseHelper.ReadResponse(wr2);
        }

		public void RunLiveTest()
		{
			Console.WriteLine("starting test...");
			AutoBidder.Requests.PlayerSearch ps = new AutoBidder.Requests.PlayerSearch();
            ps.startNo = 32;

			//below should be classed up
			Console.WriteLine("sending request...");
			System.Net.HttpWebResponse wr1 = ps.MakeRequest();
			String s1 = Helpers.WebResponseHelper.ReadResponse(wr1);
			Console.WriteLine("response = " + s1);
			Console.WriteLine("parsing response...");
			AutoBidder.Entities.RootObject v1 = JsonConvert.DeserializeObject<AutoBidder.Entities.RootObject>(s1);

			Console.WriteLine("test complete!");
		}

        public void BidLowGolds()
        {
            Console.WriteLine("starting bid low golds...");
            for (int i = 0; i < 500; i++)
            {
                AutoBidder.Requests.PlayerSearch ps = new AutoBidder.Requests.PlayerSearch();
                ps.startNo = (i * 16);
                ps.MaxPrice = 250;

                //below should be classed up
                System.Net.HttpWebResponse wr1 = ps.MakeRequest();
                String s1 = Helpers.WebResponseHelper.ReadResponse(wr1);
                AutoBidder.Entities.RootObject v1 = JsonConvert.DeserializeObject<AutoBidder.Entities.RootObject>(s1);

                foreach (Entities.AuctionInfo ai in v1.auctionInfo)
                {
                    if (ai.currentBid < 300 && ai.itemData.rating > 77 && ai.startingBid <= 300 && ai.itemData.rareflag == 0)
                    {
                        Requests.PlayerBid pb = new Requests.PlayerBid();
                        pb.BidAmount = 300;
                        pb.PlayerId = ai.tradeId.ToString();

                        //below should be classed
                        System.Net.HttpWebResponse wr2 = pb.MakeRequest();
                        String s2 = Helpers.WebResponseHelper.ReadResponse(wr2);

                        Console.WriteLine("Winning bid - for item id : " + ai.itemData.id + " bid amount : 300 quick sell price : " + ai.itemData.discardValue);
                        Helpers.Sleeper.Sleep(2000, 8000);
                    }
                }
				Helpers.Sleeper.Sleep(2000, 8000);
            }
        }
        

        public void CommonBuyout()
        {
            Console.WriteLine("Starting common buyout search...");

            for (int i = 0; i < 2500; i++)
            {
                int pageNo = 0;
                AutoBidder.Requests.PlayerSearch ps = new AutoBidder.Requests.PlayerSearch();
                ps.startNo = pageNo;
                ps.MaxBuyout = 300;
                System.Net.HttpWebResponse wr1 = ps.MakeRequest();
                String s1 = Helpers.WebResponseHelper.ReadResponse(wr1);
                AutoBidder.Entities.BaseRequest v1 = JsonConvert.DeserializeObject<AutoBidder.Entities.BaseRequest>(s1);

                bool haveBuyout = false;

                foreach (Entities.FutItem fi in v1.auctionInfo)
                {
                    haveBuyout = false;

                    if (int.Parse(fi.itemData.rating) > 76)
                    {                    
                        //look at buyout
                        Requests.PlayerBid pb = new Requests.PlayerBid();
                        int currentBid = 0;   

                        if (int.Parse(fi.buyNowPrice) <= 300 && int.Parse(fi.buyNowPrice) > 0)
                        {
                            pb.BidAmount = int.Parse(fi.buyNowPrice);
                            currentBid = int.Parse(fi.buyNowPrice);
                            haveBuyout = true;
                        }

                        if (haveBuyout)
                        {
                            pb.PlayerId = fi.tradeId;
                            System.Net.HttpWebResponse wr2 = pb.MakeRequest();
                            String s2 = Helpers.WebResponseHelper.ReadResponse(wr2);
                            Console.WriteLine("Buyout for item id : " + fi.itemData.id + " bid amount : " + currentBid + " quick sell price : " + fi.itemData.discardValue);                            
                        }    
                    }                
                }

                Helpers.Sleeper.Sleep(2000, 3000);
            }
        }

        public void RareBuyout()
        {
            Console.WriteLine("Starting rare buyout search...");
            bool overHour = false;

            for (int i = 1; i < 2500; i++)
            {
                //if we went over an hour...reset page number
                if (overHour)
                {
                    i = 1;
                }

                int hourMarkAttempt = 12 * 3000;
                int pageNo = (i * 12) + hourMarkAttempt;

                AutoBidder.Requests.PlayerSearch ps = new AutoBidder.Requests.PlayerSearch();
                ps.startNo = pageNo;
                ps.MaxBuyout = 300;
                System.Net.HttpWebResponse wr1 = ps.MakeRequest();
                String s1 = Helpers.WebResponseHelper.ReadResponse(wr1);
                AutoBidder.Entities.BaseRequest v1 = JsonConvert.DeserializeObject<AutoBidder.Entities.BaseRequest>(s1);
                
                bool haveBuyout = false;

                foreach (Entities.FutItem fi in v1.auctionInfo)
                {
                    haveBuyout = false;

                    //see if expires is more than 1 hour (in seconds, so 3600)
                    if (int.Parse(fi.expires) >= 3600)
                    {
                        overHour = true;
                    }

                    if (fi.itemData.rareflag == "1")
                    {
                        //look at buyout
                        Requests.PlayerBid pb = new Requests.PlayerBid();
                        int currentBid = 0;

                        if (int.Parse(fi.buyNowPrice) <= 600 && int.Parse(fi.buyNowPrice) > 0)
                        {
                            pb.BidAmount = int.Parse(fi.buyNowPrice);
                            currentBid = int.Parse(fi.buyNowPrice);
                            haveBuyout = true;
                        }

                        if (haveBuyout)
                        {
                            pb.PlayerId = fi.tradeId;
                            System.Net.HttpWebResponse wr2 = pb.MakeRequest();
                            String s2 = Helpers.WebResponseHelper.ReadResponse(wr2);
                            Console.WriteLine("Buyout for item id : " + fi.itemData.id + " bid amount : " + currentBid + " quick sell price : " + fi.itemData.discardValue);
                        }
                    }
                }

                Helpers.Sleeper.Sleep(2000, 3000);
            }
        }

        public void PremBid(string outputStr)
        {
            for (int i = 250; i < 2500; i++)
            {
                int pageNo = i * 16;
                AutoBidder.Requests.PlayerSearch ps = new AutoBidder.Requests.PlayerSearch();
                ps.startNo = pageNo;
                ps.MaxPrice = 550;
                ps.MaxBuyout = 600;
                System.Net.HttpWebResponse wr1 = ps.MakeRequest();
                String s1 = Helpers.WebResponseHelper.ReadResponse(wr1);
                AutoBidder.Entities.BaseRequest v1 = JsonConvert.DeserializeObject<AutoBidder.Entities.BaseRequest>(s1);
                Helpers.Sleeper.Sleep(2000, 3000);
                bool haveABid = false;
                bool haveBuyout = false;

                foreach (Entities.FutItem fi in v1.auctionInfo)
                {
                    haveABid = false;
                    haveBuyout = false;
                    if (int.Parse(fi.currentBid) <= 600 && int.Parse(fi.itemData.rating) > 76 && int.Parse(fi.startingBid) <=600)
                    {
                        int currentBid = 0;                        
                        Requests.PlayerBid pb = new Requests.PlayerBid();
                        if (fi.itemData.rareflag == "1")
                        {
                            //look at buyout
                            if (int.Parse(fi.buyNowPrice) <= 600 && int.Parse(fi.buyNowPrice) > 0)
                            {
                                pb.BidAmount = int.Parse(fi.buyNowPrice);
                                currentBid = int.Parse(fi.buyNowPrice);
                                haveBuyout = true;
                            }
                            //look at bidding
                            else if (int.Parse(fi.currentBid) <= 600)
                            {
                                //if its zero starting price - bid the starting bid + 50
                                if (int.Parse(fi.currentBid) == 0 && int.Parse(fi.startingBid) <= 600)
                                {
                                    pb.BidAmount = int.Parse(fi.startingBid);
                                    currentBid = int.Parse(fi.startingBid);
                                    haveABid = true;
                                }
                                else if (int.Parse(fi.currentBid) > 0 && int.Parse(fi.currentBid) < 600)
                                {
                                    pb.BidAmount = int.Parse(fi.currentBid) + 50;
                                    currentBid = int.Parse(fi.currentBid) + 50;
                                    haveABid = true;
                                }
                            }
                        }
                        else
                        {
                            //look at buyout
                            if (int.Parse(fi.buyNowPrice) <= 300 && int.Parse(fi.buyNowPrice) > 0)
                            {
                                pb.BidAmount = int.Parse(fi.buyNowPrice);
                                currentBid = int.Parse(fi.buyNowPrice);
                                haveBuyout = true;
                            }
                            //look at bidding
                            else if (int.Parse(fi.currentBid) <= 300)
                            {
                                //if its zero starting price - bid the starting bid + 50
                                if (int.Parse(fi.currentBid) == 0 && int.Parse(fi.startingBid) <= 300)
                                {
                                    pb.BidAmount = int.Parse(fi.startingBid);
                                    currentBid = int.Parse(fi.startingBid);
                                    haveABid = true;
                                }
                                else if (int.Parse(fi.currentBid) > 0 && int.Parse(fi.currentBid) < 300)
                                {
                                    pb.BidAmount = int.Parse(fi.currentBid) + 50;
                                    currentBid = int.Parse(fi.currentBid) + 50;
                                    haveABid = true;
                                }
                            }
                        }

                        if (haveABid || haveBuyout)
                        {
                            pb.PlayerId = fi.tradeId;
                            System.Net.HttpWebResponse wr2 = pb.MakeRequest();
                            String s2 = Helpers.WebResponseHelper.ReadResponse(wr2);
                            string bidOrBuy = "";
                            if (haveABid) { bidOrBuy = "Bid"; } else { bidOrBuy = "Buyout"; }
                            outputStr = bidOrBuy + " for item id : " + fi.itemData.id + " bid amount : " + currentBid + " quick sell price : " + fi.itemData.discardValue;
                            Console.WriteLine(bidOrBuy + " for item id : " + fi.itemData.id + " bid amount : " + currentBid + " quick sell price : " + fi.itemData.discardValue);
                            Helpers.Sleeper.Sleep(2000, 3000);
                        }
                    }
                }
            }
        }

        public void FitnessBid()
        {
            for (int i = 0; i < 20; i++)
            {
                int pageNo = i * 16;
                AutoBidder.Requests.DevelopmentSearch ds = new AutoBidder.Requests.DevelopmentSearch();
                ds.startNo = pageNo;
                ds.category = "fitness";
                ds.minBid = 600;
                ds.maxBid = 900;                     
                System.Net.HttpWebResponse wr1 = ds.MakeRequest();
                String s1 = Helpers.WebResponseHelper.ReadResponse(wr1);
                AutoBidder.Entities.BaseRequest v1 = JsonConvert.DeserializeObject<AutoBidder.Entities.BaseRequest>(s1);
                Helpers.Sleeper.Sleep(5000, 10000);

                foreach (Entities.FutItem fi in v1.auctionInfo)
                {
                    if (int.Parse(fi.currentBid) < 600)
                    {
                        Requests.DevelopmentBid db = new Requests.DevelopmentBid();
                        if (fi.itemData.rareflag == "1")
                        {
                            db.BidAmount = 800;
                        }

                        db.ItemId = fi.tradeId;
                        System.Net.HttpWebResponse wr2 = db.MakeRequest();
                        String s2 = Helpers.WebResponseHelper.ReadResponse(wr2);
                        Helpers.Sleeper.Sleep(2000, 8000);
                    }
                }
            }
        }

        public void FitnessBuyout()
        {
            for (int i = 0; i < 20; i++)
            {
                int pageNo = i * 16;
                AutoBidder.Requests.DevelopmentSearch ds = new AutoBidder.Requests.DevelopmentSearch();
                ds.startNo = pageNo;
                ds.category = "fitness";
                ds.minBuyout = 300;
                ds.maxBuyout = 950;
                System.Net.HttpWebResponse wr1 = ds.MakeRequest();
                String s1 = Helpers.WebResponseHelper.ReadResponse(wr1);
                AutoBidder.Entities.BaseRequest v1 = JsonConvert.DeserializeObject<AutoBidder.Entities.BaseRequest>(s1);
                Helpers.Sleeper.Sleep(5000, 10000);

                foreach (Entities.FutItem fi in v1.auctionInfo)
                {
                    if (int.Parse(fi.buyNowPrice) <= 950)
                    {
                        Requests.DevelopmentBid db = new Requests.DevelopmentBid();
                        if (fi.itemData.rareflag == "1")
                        {
                            //bid the buyout price
                            db.BidAmount = int.Parse(fi.buyNowPrice);
                        }

                        db.ItemId = fi.tradeId;
                        System.Net.HttpWebResponse wr2 = db.MakeRequest();
                        String s2 = Helpers.WebResponseHelper.ReadResponse(wr2);
                        Helpers.Sleeper.Sleep(2000, 8000);
                    }
                }
            }
        }
    }
}
