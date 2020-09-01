using MAS.Agents;
using MAS.Auctions;
using MAS.MasRunner.Events;
using MAS.Utils;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace MAS.MasRunner
{
    public class MasHandlerRunner : IMas
    {
        public static int maxAttemptAuction = 10;

        public event NotifyAuctionGoingToStart NotifyGoingToStart;

        private object _removeLocker = new object();

        public List<IAuction> Auctions { get; set; }

        public List<IAgent> Agents { get; set; }

        public MasHandlerRunner(List<IAgent> agents, List<IAuction> auctions)
        {
            Auctions = auctions;
            Agents = agents;
            MakeAllAgentsSubscribes();
        }

        private void MakeAllAgentsSubscribes()
        {
            Agents.ForEach(a => NotifyGoingToStart += new NotifyAuctionGoingToStartEvent(a).NotifyGoingToStart);
        }

        public void RemoveAuctions(IAuction auction)
        {
            if (Auctions.Contains(auction))
            {
                lock (_removeLocker)
                {
                    Auctions.Remove(auction);
                }
            }
        }


        private async Task<bool> AskAllAgentsToParticipateInAuction(IAuction auction)
        {
            bool runTimer = true;
            int count = 0;
            while (runTimer && count < 20)
            {
                Util.RunAllEventInParallel(NotifyGoingToStart, auction);
                bool isEveryOneWantToParticipate = auction.AllParticipateAgents.Count > 0;
                if (isEveryOneWantToParticipate)
                {
                    return true;
                }
                count++;
                Thread.Sleep(1000);
            }
            return false;
        }

        public void StartAuctions()
        {
            List<Task> allTaks = new List<Task>();

            foreach (IAuction auction in Auctions)
            {
                TimeSpan secondsDelay = GetAuctionTaskDelay(auction);
                Task t = Task.Delay(secondsDelay).ContinueWith(o => RunSpecificAuction(auction));
                allTaks.Add(t);
            }
            Task.WaitAll(allTaks.ToArray());
        }

        private async void RunSpecificAuction(IAuction auction)
        {
            bool hasPerformToStartAuction = await AskAllAgentsToParticipateInAuction(auction);
            if (hasPerformToStartAuction)
            {
                RemoveAuctions(auction);
                await auction.StartAuction();
            }

        }

        private TimeSpan GetAuctionTaskDelay(IAuction auction)
        {

            if (auction.StartDateTime > DateTime.Now)
            {
                var seconds = (auction.StartDateTime - DateTime.Now).TotalSeconds;
                int n = (int)seconds;
                return new TimeSpan(0, 0, n);
            }
            return new TimeSpan(0, 0, 0);
        }
    }
}
