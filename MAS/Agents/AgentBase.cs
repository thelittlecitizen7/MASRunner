using MAS.Auctions;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MAS.Agents
{
    public abstract class AgentBase : IAgent
    {

        private object locker = new object();
        public string Name { get; set; }
        public int TotalMoney { get; set; }
        public string Id { get; set; }
        public List<IAuction> Auctions { get; set; }

        public AgentBase(int money, string name)
        {
            Name = name;
            Id = Guid.NewGuid().ToString();
            Auctions = new List<IAuction>();
            TotalMoney = money;
        }


        public abstract int GetNewOffer(IAuction auction);

        public abstract bool IsWantToOfferNewOffer(IAuction auction);

        public abstract bool IsWantToParticipate(IAuction auction);

        public void GetWinnerOfAuction(IAuction auction)
        {
            lock (locker)
            {
                RemoveFromAuction(auction);
            }
        }

        public bool IsExistInAuction(string auctionId)
        {
            return Auctions.Any(a => a.Id == auctionId);
        }

        public void RemoveFromAuction(IAuction auction)
        {
            if (IsExistInAuction(auction.Id))
            {
                Auctions.Remove(auction);
            }
        }

        public void UpdateMoney(int money)
        {
            TotalMoney += money;
        }

        public void AddToAuction(IAuction auction)
        {
            if (!IsExistInAuction(auction.Id))
            {
                Auctions.Add(auction);
            }
        }
    }
}
