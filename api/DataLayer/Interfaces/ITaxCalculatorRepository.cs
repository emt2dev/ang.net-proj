using AuthReadyAPI.DataLayer.DTOs.TaxCalculator;
using AuthReadyAPI.DataLayer.Models.TaxCalculator;

namespace AuthReadyAPI.DataLayer.Interfaces
{
    public interface ITaxCalculatorRepository
    {
        public Task<CalculatedTaxDTO> Calculate(CalculatedTaxDTO DTO);
        public double CalculateMonthly(double AnnualAmount);
        public Task<TaxPayoutsDTO> UKGetPayoutsDTO(double AnnualTaxAmount, string Currency, TaxPayoutsClass UKPayouts);
        public Task<double> UKCalculateAnnualTaxPaid(double AnnualGrossAmount, string Currency);
        public Task<List<string>> GetAllAvailableCurrencies();
    }
}
