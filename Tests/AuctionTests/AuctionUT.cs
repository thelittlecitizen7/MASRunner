using MAS.Auctions;
using MAS.Products;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Tests.AuctionTests
{

    [TestClass]
    public class AuctionUT
    {

        [TestMethod]
        public void Check_Init_Constructor_Params()
        {
            int startPrice = 200;
            int jumpPrice = 100;
            DateTime startDate = DateTime.Now;
            IProduct product = new OfficeProduct();

            Auction auction = new Auction(startPrice, jumpPrice, startDate, product, null);

            Assert.AreEqual(startPrice, auction.CurrentPrice);
            Assert.AreEqual(startDate, auction.StartDateTime);
            Assert.AreEqual(jumpPrice, auction.MinimumPriceJump);
            Assert.AreEqual(startDate, auction.StartDateTime);
            Assert.AreEqual(product, auction.Product);
        }

        [TestMethod]
        public void Check_Set_Price_Auction()
        {
            int startPrice = 200;
            int jumpPrice = 100;
            DateTime startDate = DateTime.Now;
            IProduct product = new OfficeProduct();
            int actualPrice = 100;

            Auction auction = new Auction(startPrice, jumpPrice, startDate, product, null);

            auction.SetPrice(actualPrice);
            Assert.AreEqual(actualPrice, auction.CurrentPrice);
        }


    }
}
