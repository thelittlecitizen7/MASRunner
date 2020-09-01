using MAS.Auctions;
using System.Collections.Generic;

namespace MAS.Agents
{
    public interface IAgent
    {
        public string Name { get; set; }
        int TotalMoney { get; set; }
        string Id { get; set; }
        List<IAuction> Auctions { get; set; }
        bool IsWantToParticipate(IAuction auction);

        bool IsWantToOfferNewOffer(IAuction auction);

        int GetNewOffer(IAuction auction);

        bool IsExistInAuction(string auctionId);

        void AddToAuction(IAuction auction);

        void RemoveFromAuction(IAuction auction);

        void GetWinnerOfAuction(IAuction auction);

        void UpdateMoney(int money);
    }
}
