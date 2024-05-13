namespace AuthReadyAPI.DataLayer.DTOs.TaxCalculator
{
    public class CalculatedTaxDTO
    {
        public string Currency { get; set; }
        public int EmployeeId { get; set; }
        public double GrossAnnualSalary { get; set; }
        public double GrossMonthlySalary { get; set; }
        public double NetAnnualSalary { get; set; }
        public double NetMonthlySalary { get; set; }
        public double AnnualTaxPaid { get; set; }
        public double MonthlyTaxPaid { get; set; }
        public TaxPayoutsDTO? Payouts { get; set; }
    }
}
