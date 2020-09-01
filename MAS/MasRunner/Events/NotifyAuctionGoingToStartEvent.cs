using MAS.Agents;
using MAS.Auctions;

namespace MAS.MasRunner.Events
{
    public class NotifyAuctionGoingToStartEvent
    {
        private IAgent _agent;
        private object locker = new object();
        public NotifyAuctionGoingToStartEvent(IAgent agent)
        {
            _agent = agent;
        }

        public void NotifyGoingToStart(IAuction auction)
        {
            lock (locker)
            {
                if (_agent.IsWantToParticipate(auction))
                {
                    auction.AddAgentToAuction(_agent);
                    _agent.AddToAuction(auction);
                }
            }
        }
    }
}
