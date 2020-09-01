using MAS.Agents;
using MAS.Auctions;
using System.Collections.Generic;

namespace MAS.MasRunner
{
    public delegate void NotifyAuctionGoingToStart(IAuction auction);
    public interface IMas
    {
        List<IAuction> Auctions { get; set; }

        List<IAgent> Agents { get; set; }

        event NotifyAuctionGoingToStart NotifyGoingToStart;

        void StartAuctions();

        void RemoveAuctions(IAuction auction);

    }
}
