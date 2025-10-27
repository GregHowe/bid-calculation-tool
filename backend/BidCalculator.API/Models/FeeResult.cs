namespace BidCalculator.API.Models
{
    public class FeeResult
    {
        public decimal BasicFee { get; set; }
        public decimal SpecialFee { get; set; }
        public decimal AssociationFee { get; set; }
        public decimal StorageFee { get; set; }
        public decimal Total { get; set; }
    }
}
