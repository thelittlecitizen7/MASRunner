using MAS.Auctions;
using System;

namespace MAS.Agents
{
    public class Agent : AgentBase
    {

        private static Random rnd = new Random();

        public Agent(int money, string name) : base(money, name)
        {
        }

        public override int GetNewOffer(IAuction auction)
        {

            int number;

            number = rnd.Next(10);

            int finalOfferPrice = auction.CurrentPrice + auction.MinimumPriceJump + number;

            return finalOfferPrice;

        }

        public override bool IsWantToOfferNewOffer(IAuction auction)
        {
            if (TotalMoney > auction.CurrentPrice)
            {
                int number = rnd.Next(100);
                if (number > 15 && number < 80)
                {
                    return true;
                }
            }
            return false;
        }

        public override bool IsWantToParticipate(IAuction auction)
        {
            return TotalMoney > auction.StartPrice;
        }


    }
}
