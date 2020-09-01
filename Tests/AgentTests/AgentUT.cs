using MAS.Agents;
using MAS.Auctions;
using MAS.Products;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Tests.AgentTests
{
    [TestClass]
    public class AgentUT
    {


        [TestMethod]
        public void Check_Constructor_Init_()
        {
            string name = "agent";
            int price = 100;
            AgentBase agent = new Agent(price, name);

            Assert.AreEqual(name, agent.Name);
            Assert.AreEqual(price, agent.TotalMoney);
        }

        [TestMethod]
        public void Check_Add_Money_To_Agent()
        {
            string name = "agent";
            int price = 100;
            int moneyToUpdate = 200;
            AgentBase agent = new Agent(price, name);

            Assert.AreEqual(price, agent.TotalMoney);
            agent.UpdateMoney(moneyToUpdate);
            Assert.AreEqual(moneyToUpdate + price, agent.TotalMoney);
        }


        [TestMethod]
        public void Check_Add_Agent_To_Auction()
        {
            string name = "agent";
            int price = 100;
            AgentBase agent = new Agent(price, name);
            IProduct product = new OfficeProduct();
            Auction auction = new Auction(200, 100, DateTime.Now, product, null);

            agent.AddToAuction(auction);
            Assert.AreEqual(1, agent.Auctions.Count);
            Assert.AreEqual(auction.Id, agent.Auctions[0].Id);
        }

        [TestMethod]
        public void Check_Add_Agent_That_Exist_In_Auction()
        {
            string name = "agent";
            int price = 100;
            AgentBase agent = new Agent(price, name);
            IProduct product = new OfficeProduct();
            Auction auction = new Auction(200, 100, DateTime.Now, product, null);

            agent.AddToAuction(auction);
            agent.AddToAuction(auction);
            Assert.AreEqual(1, agent.Auctions.Count);
            Assert.AreEqual(auction.Id, agent.Auctions[0].Id);
        }

        [TestMethod]
        public void Check_If_Agent_Exist_In_Auction()
        {
            string name = "agent";
            int price = 100;
            AgentBase agent = new Agent(price, name);
            IProduct product = new OfficeProduct();
            Auction auction = new Auction(200, 100, DateTime.Now, product, null);

            agent.AddToAuction(auction);

            bool isAgentExistInAuction = agent.IsExistInAuction(auction.Id);
            Assert.IsTrue(isAgentExistInAuction);
        }



        [TestMethod]
        public void Check_If_Agent_Exist_In_Auction_When_Didnt_Add_Him()
        {
            string name = "agent";
            int price = 100;
            AgentBase agent = new Agent(price, name);
            IProduct product = new OfficeProduct();
            Auction auction = new Auction(200, 100, DateTime.Now, product, null);

            bool isAgentExistInAuction = agent.IsExistInAuction(auction.Id);
            Assert.IsFalse(isAgentExistInAuction);
        }


        [TestMethod]
        public void Check_Remove_Auction_From_Agent()
        {
            string name = "agent";
            int price = 100;
            AgentBase agent = new Agent(price, name);
            IProduct product = new OfficeProduct();
            Auction auction = new Auction(200, 100, DateTime.Now, product, null);

            agent.AddToAuction(auction);
            agent.RemoveFromAuction(auction);
            Assert.AreEqual(0, agent.Auctions.Count);
        }

        [TestMethod]
        public void Check_Remove_Auction_From_Agent_That_Not_Added()
        {
            string name = "agent";
            int price = 100;
            AgentBase agent = new Agent(price, name);
            IProduct product = new OfficeProduct();
            Auction auction = new Auction(200, 100, DateTime.Now, product, null);

            agent.RemoveFromAuction(auction);
            Assert.AreEqual(0, agent.Auctions.Count);
        }


        [TestMethod]
        public void Check_Notify_On_Winner_Of_Auction()
        {
            string name = "agent";
            int price = 100;
            AgentBase agent = new Agent(price, name);
            IProduct product = new OfficeProduct();
            Auction auction = new Auction(200, 100, DateTime.Now, product, null);

            agent.AddToAuction(auction);
            agent.GetWinnerOfAuction(auction);
            Assert.AreEqual(0, agent.Auctions.Count);
        }


        [TestMethod]
        public void Check_Notify_On_Winner_Of_Auction_That_Not_Exist()
        {
            string name = "agent";
            int price = 100;
            AgentBase agent = new Agent(price, name);
            IProduct product = new OfficeProduct();
            Auction auction = new Auction(200, 100, DateTime.Now, product, null);
            Auction auction2 = new Auction(200, 100, DateTime.Now, product, null);

            agent.AddToAuction(auction);
            agent.GetWinnerOfAuction(auction2);
            Assert.AreEqual(1, agent.Auctions.Count);
            Assert.AreEqual(auction.Id, agent.Auctions[0].Id);
        }

        [TestMethod]
        public void Check_Agent_Give_New_Offer()
        {
            string name = "agent";
            int price = 100;
            AgentBase agent = new Agent(price, name);
            IProduct product = new OfficeProduct();
            Auction auction = new Auction(200, 100, DateTime.Now, product, null);

            int offer = agent.GetNewOffer(auction);
            Assert.IsTrue(offer > 0);
        }

    }
}
