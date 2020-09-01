using MAS.Auctions;
using MAS.Products;
using System;

namespace MAS.Factory
{
    public interface IAuctionFactory
    {
        IAuction CreateAuction(int startPrice, int minimumJumpPrice, DateTime startdateTime, IProduct product);
    }
}
