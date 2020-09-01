using MAS.Agents;
using MAS.Auctions.Event;
using MAS.Enums;
using MAS.IO.SystemOutput;
using MAS.Products;
using MAS.Utils;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace MAS.Auctions
{

    public class Auction : IAuction
    {
        private object _printLocket = new object();

        public int MinimumPriceJump { get; set; }

        public string Id { get; set; }

        public int CurrentPrice { get; set; }

        public int StartPrice { get; set; }

        public DateTime StartDateTime { get; set; }

        public IProduct Product { get; set; }

        public List<IAgent> AllParticipateAgents { get; set; }

        public AuctionStatus AuctionStatus { get; set; }

        public IOutput _systemOutput { get; set; }

        public event NotifyStartAuction StartAuctionAgents;

        public event NotifyEveryOneOnWinner NotifyWinnerEvent;

        public ConcurrentQueue<KeyValuePair<IAgent, int>> LastOfferQueue { get; set; }

        public KeyValuePair<IAgent, int> WinnerAgent { get; set; }

        private string _startMsg
        {
            get
            {
                string msg = $"---------------------------------------------- {Environment.NewLine}";
                msg += $"The Auction with id {Id} started {Environment.NewLine}";
                msg += $"{Product.ToString()} {Environment.NewLine}";
                msg += $"Start Price : {StartPrice} {Environment.NewLine}";
                msg += $"---------------------------------------------- {Environment.NewLine}";
                return msg;
            }
        }

        public Auction(int startPrice, int minimumJumpPrice, DateTime startdateTime, IProduct product, IOutput systemOutput)
        {
            WinnerAgent = new KeyValuePair<IAgent, int>();
            LastOfferQueue = new ConcurrentQueue<KeyValuePair<IAgent, int>>();
            Id = Guid.NewGuid().ToString();
            CurrentPrice = startPrice;
            _systemOutput = systemOutput;
            Product = product;
            AllParticipateAgents = new List<IAgent>();
            StartDateTime = startdateTime;
            StartPrice = startPrice;
            MinimumPriceJump = minimumJumpPrice;
        }


        public void AddAgentToAuction(IAgent agent)
        {
            _systemOutput.Print($"The agent : {agent.Name} going into auction with id {Id}");
            AllParticipateAgents.Add(agent);
            AddStartAuctionAgent(agent);
            AddNotifyOnWinner(agent);

        }

        public void SetPrice(int price)
        {
            CurrentPrice = price;
        }

        public void SetAgentWithMaxOffer(IAgent agent, int offer)
        {
            lock (_printLocket)
            {
                KeyValuePair<IAgent, int> data = new KeyValuePair<IAgent, int>(agent, offer);
                LastOfferQueue.Enqueue(data);
                WinnerAgent = LastOfferQueue.Last();
                PrintTheCurrentAuctionSituation();
            }
        }

        private void SetStatusAcution(AuctionStatus auctionStatus)
        {
            AuctionStatus = auctionStatus;
        }

        public void CloseAuction()
        {
            SetStatusAcution(AuctionStatus.Closed);
            _systemOutput.Print($"The auction with id {Id} is finshed");
            if (!LastOfferQueue.IsEmpty)
            {
                _systemOutput.Print($"The Winner is {WinnerAgent.Key.Name} with offer of {WinnerAgent.Value} on auction {Id}");
            }
            else
            {
                _systemOutput.Print($"There is no Winner in this auction {Id}");
            }
            Util.RunAllEventInParallel(NotifyWinnerEvent, this);
        }

        private void StartEndProgress()
        {
            int max = 5;
            int count = 0;
            while (count != max)
            {
                _systemOutput.Print($"The auction with id {Id} will end in {max - count}");
                int previosPrice = CurrentPrice;
                NotifyAllForGiveNewOffer();
                if (previosPrice != CurrentPrice)
                {
                    return;
                }
                count++;
                Thread.Sleep(1000);
            }

        }

        public async Task StartAuction()
        {
            _systemOutput.Print(_startMsg);

            int count = 0;
            SetStatusAcution(AuctionStatus.DuringAuction);
            while (count != 40 && AuctionStatus != AuctionStatus.Closed)
            {
                int previosCurrentPrice = CurrentPrice;
                NotifyAllForGiveNewOffer();
                if (CurrentPrice == previosCurrentPrice)
                {
                    SetStatusAcution(AuctionStatus.EndProgress);
                    StartEndProgress();

                    if (CurrentPrice == previosCurrentPrice)
                    {
                        SetStatusAcution(AuctionStatus.Closed);

                    }
                    else
                    {
                        NotifyAllForGiveNewOffer();
                    }
                }

                count++;
                Thread.Sleep(1000);
            }
            CloseAuction();
        }

        public void NotifyAllForGiveNewOffer()
        {
            Util.RunAllEventInParallel(StartAuctionAgents, this);

        }

        public void NotifyAllAgentsOnWinner()
        {
            WinnerAgent.Key.UpdateMoney(-CurrentPrice);
            Util.RunAllEventInParallel(NotifyWinnerEvent, this);
        }

        private void AddStartAuctionAgent(IAgent agent)
        {
            StartAuctionAgents += new NotifyAgentsToGiveOffer(agent).NotifyStartAuction;
        }

        private void AddNotifyOnWinner(IAgent agent)
        {
            NotifyWinnerEvent += new NotifyEveryoneOnWinner(agent).NotifyWinner;
        }

        private void PrintTheCurrentAuctionSituation()
        {
            string msg = $"The agent with name : {WinnerAgent.Key.Name} , offers price : {WinnerAgent.Value} on auction id : {Id}";
            _systemOutput.Print(msg);
        }

    }
}
