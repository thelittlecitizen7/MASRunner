using MAS.Agents;
using MAS.Enums;
using MAS.Products;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MAS.Auctions
{
    public delegate void NotifyStartAuction(IAuction auction);
    public delegate void NotifyEveryOneOnWinner(IAuction auction);
    public interface IAuction
    {
        KeyValuePair<IAgent, int> WinnerAgent { get; set; }
        ConcurrentQueue<KeyValuePair<IAgent, int>> LastOfferQueue { get; set; }
        int MinimumPriceJump { get; set; }

        string Id { get; set; }
        int CurrentPrice { get; }

        public int StartPrice { get; }

        DateTime StartDateTime { get; set; }
        IProduct Product { get; set; }

        List<IAgent> AllParticipateAgents { get; set; }

        AuctionStatus AuctionStatus { get; set; }

        event NotifyEveryOneOnWinner NotifyWinnerEvent;

        event NotifyStartAuction StartAuctionAgents;

        void NotifyAllForGiveNewOffer();

        void CloseAuction();
        void AddAgentToAuction(IAgent agent);

        Task StartAuction();
        void SetAgentWithMaxOffer(IAgent agent, int offer);

        void SetPrice(int price);

    }
}
