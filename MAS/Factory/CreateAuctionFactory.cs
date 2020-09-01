using MAS.Auctions;
using MAS.IO.SystemOutput;
using MAS.Products;
using System;

namespace MAS.Factory
{
    public class CreateAuctionFactory : IAuctionFactory
    {
        public IAuction CreateAuction(int startPrice, int minimumJumpPrice, DateTime startdateTime, IProduct product)
        {
            Random rnd = new Random();
            int number = rnd.Next(1, 16);
            ConsoleColor color = (ConsoleColor)number;
            return new Auction(startPrice, minimumJumpPrice, startdateTime, product, new ConsoleOutput(color));
        }

    }
}
