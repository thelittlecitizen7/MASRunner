using MAS.Agents;

namespace MAS.Auctions.Event
{
    public class NotifyEveryoneOnWinner
    {
        private IAgent _agent { get; set; }
        private object locker = new object();
        public NotifyEveryoneOnWinner(IAgent agent)
        {
            _agent = agent;
        }

        public void NotifyWinner(IAuction auction)
        {
            lock (locker)
            {
                _agent.GetWinnerOfAuction(auction);
            }
        }
    }
}
