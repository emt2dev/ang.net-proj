namespace AuthReadyAPI.DataLayer.Models.TaxCalculator
{
    public class TaxBandClass : BaseTaxClass
    {
        // Tax Band A
        public double Band_A_Amount { get; set; }
        public double Band_A_Limit { get; set; }
        public int Band_A_Percentage { get; set; }

        // Tax Band B
        public double Band_B_Amount { get; set; }
        public double Band_B_Limit { get; set; }
        public int Band_B_Percentage { get; set; }

        // Tax Band C
        public double Band_C_Amount { get; set; }
        public int Band_C_Percentage { get; set; }
    }
}
