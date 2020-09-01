using MAS.Agents;

namespace MAS.Auctions.Event
{
    public class NotifyAgentsToGiveOffer
    {
        public object locker = new object();
        public object locker2 = new object();
        private IAgent _agent { get; set; }
        public NotifyAgentsToGiveOffer(IAgent agent)
        {
            _agent = agent;
        }

        public void NotifyStartAuction(IAuction auction)
        {
            bool isAgentWantToOffer = _agent.IsWantToOfferNewOffer(auction);
            if (isAgentWantToOffer)
            {
                var CurrentWinerAgent = auction.WinnerAgent;
                if (!auction.LastOfferQueue.IsEmpty)
                {
                    if (CurrentWinerAgent.Key != null)
                    {
                        if (CurrentWinerAgent.Key.Id != _agent.Id)
                        {
                            CheckIfValidOffer(auction);
                        }
                    }
                }
                else
                {
                    CheckIfValidOffer(auction);
                }
            }

        }

        private void CheckIfValidOffer(IAuction auction)
        {
            int agentOfferPrice = _agent.GetNewOffer(auction);
            int finalOffer = agentOfferPrice - auction.MinimumPriceJump;
            if (finalOffer > auction.CurrentPrice)
            {
                auction.SetPrice(agentOfferPrice);
                auction.SetAgentWithMaxOffer(_agent, agentOfferPrice);
            }

        }


    }
}
