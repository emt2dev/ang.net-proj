namespace AuthReadyAPI.DataLayer.DTOs.TaxCalculator
{
    public class TaxPayoutsDTO : BaseTaxDTO
    {
        public double Welfare { get; set; }
        public double Health { get; set; }
        public double StatePensions { get; set; }
        public double Education { get; set; }
        public double NationalDebtInterest { get; set; }
        public double Defence { get; set; }
        public double PublicOrderSafety { get; set; }
        public double Transport { get; set; }
        public double BusinessAndIndustry { get; set; }
        public double GovAdmin { get; set; }
        public double Culture { get; set; }
        public double Environment { get; set; }
        public double HouseAndUtilities { get; set; }
        public double OverseasAid { get; set; }
    }
}
