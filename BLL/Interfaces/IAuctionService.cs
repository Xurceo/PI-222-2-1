using System.Collections.Generic;

namespace BLL.Interfaces
{
    interface IAuctionService
    {
        IEnumerable<AuctionItemDTO> GetAllAuctionItems();
        AuctionItemDto GetAuctionItemById(int id);
        void AddAuctionItem(AuctionItemDTO itemDTO);
        void PlaceBid(BidCreateDTO bidDTO);
        void AproveAuctionItem(int itemId, int managerId);
    }
}
