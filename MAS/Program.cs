using MAS.Agents;
using MAS.Auctions;
using MAS.Factory;
using MAS.MasRunner;
using MAS.Products;
using System;
using System.Collections.Generic;

namespace MAS
{
    class Program
    {
        static void Main(string[] args)
        {
            List<IAgent> agents = new List<IAgent>()
            {
                new Agent(500,"lior"),
                new Agent(500,"afek"),
              //  new Agent(1000,"carmel"),
               // new Agent(4000,"elay"),
               // new Agent(4000,"yuval"),
               // new Agent(4000,"tal")

            };
            IProduct office = new OfficeProduct();
            IProduct office2 = new OfficeProduct();

            CreateAuctionFactory createAuctionFactory = new CreateAuctionFactory();

            List<IAuction> auctions = new List<IAuction>()
            {
                //createAuctionFactory.CreateAuction(200,100,DateTime.Now,office),
                createAuctionFactory.CreateAuction(200,200,DateTime.Now,office),
                createAuctionFactory.CreateAuction(300,100,DateTime.Now.AddSeconds(3),office2)
            };
            IMas mas = new MasHandlerRunner(agents, auctions);
            mas.StartAuctions();
        }

    }
}
