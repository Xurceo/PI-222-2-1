namespace BLL.ShortDTOs
{
    public class BidShortDTO
    {
        public Guid Id { get; set; }
        public decimal Amount { get; set; }
        public DateTime Time { get; set; }
        public required UserShortDTO User { get; set; }
        public required LotShortDTO Lot { get; set; }
    }

}
