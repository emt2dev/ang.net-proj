using AuthReadyAPI.DataLayer.DTOs.Employees;
using AuthReadyAPI.DataLayer.DTOs.TaxCalculator;
using AuthReadyAPI.DataLayer.Interfaces;
using AuthReadyAPI.DataLayer.Models.Employees;
using AuthReadyAPI.DataLayer.Models.PII;
using AuthReadyAPI.DataLayer.Models.TaxCalculator;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace AuthReadyAPI.DataLayer.Repositories
{
    public class TaxCalculatorRepository : ITaxCalculatorRepository
    {
        private AuthDbContext _context;
        private IMapper _mapper;
        public TaxCalculatorRepository(AuthDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<List<string>> GetAllAvailableCurrencies()
        {
            List<TaxPayoutsClass> PayoutsList = await _context.TaxPayouts.ToListAsync();
            List<string> ListOfCurrencies = new List<string>();

            foreach (var item in PayoutsList) ListOfCurrencies.Add(item.Currency);

            return ListOfCurrencies;
        }

        public async Task<CalculatedTaxDTO> Calculate(CalculatedTaxDTO DTO)
        {
            EmployeeClass EnsureEmployeeExists = await _context.Employees.Where(x => x.Id == DTO.EmployeeId).FirstOrDefaultAsync();
            if (EnsureEmployeeExists is null) return null;

            TaxPayoutsClass EnsureCurrencyExists = await _context.TaxPayouts.Where(x => x.Currency == DTO.Currency).FirstOrDefaultAsync();
            if (EnsureCurrencyExists is null) return null;

            CalculatedTaxDTO OutgoingDTO = new CalculatedTaxDTO();
            OutgoingDTO.Currency = DTO.Currency;
            OutgoingDTO.EmployeeId = DTO.EmployeeId;

            OutgoingDTO.GrossAnnualSalary = DTO.GrossAnnualSalary;
            OutgoingDTO.GrossMonthlySalary = CalculateMonthly(DTO.GrossAnnualSalary);

            switch (OutgoingDTO.Currency)
            {
                case "GBP":
                    // Here we ensure that the GBP is inside of our enums
                    OutgoingDTO.AnnualTaxPaid = await UKCalculateAnnualTaxPaid(OutgoingDTO.GrossAnnualSalary, OutgoingDTO.Currency);
                    OutgoingDTO.MonthlyTaxPaid = Math.Round(CalculateMonthly(OutgoingDTO.AnnualTaxPaid), 2, MidpointRounding.AwayFromZero);

                    OutgoingDTO.NetAnnualSalary = OutgoingDTO.GrossAnnualSalary - OutgoingDTO.AnnualTaxPaid;
                    OutgoingDTO.NetMonthlySalary = Math.Round(CalculateMonthly(OutgoingDTO.NetAnnualSalary), 2, MidpointRounding.AwayFromZero);

                    OutgoingDTO.Payouts = await UKGetPayoutsDTO(OutgoingDTO.AnnualTaxPaid, OutgoingDTO.Currency, EnsureCurrencyExists);
                    return OutgoingDTO;

                default:
                    return OutgoingDTO;
            }
        }

        public double CalculateMonthly(double AnnualAmount)
        {
            return AnnualAmount / 12;
        }

        public async Task<TaxPayoutsDTO> UKGetPayoutsDTO(double AnnualTaxAmount, string Currency, TaxPayoutsClass UKPayouts)
        {
            TaxPayoutsDTO Payouts = new TaxPayoutsDTO();

            // We don't use automapper here due to the calculations being performed.
            Payouts.Id = UKPayouts.Id;
            Payouts.LastModifiedByUser = UKPayouts.LastModifiedByUser;
            Payouts.LastModifiedOnDate = UKPayouts.LastModifiedOnDate;
            Payouts.Currency = UKPayouts.Currency;

            Payouts.Welfare = Math.Round((AnnualTaxAmount * UKPayouts.Welfare), 2, MidpointRounding.AwayFromZero);
            Payouts.Health = Math.Round((AnnualTaxAmount * UKPayouts.Health), 2, MidpointRounding.AwayFromZero);
            Payouts.StatePensions = Math.Round((AnnualTaxAmount * UKPayouts.StatePensions), 2, MidpointRounding.AwayFromZero);
            Payouts.Education = Math.Round((AnnualTaxAmount * UKPayouts.Education), 2, MidpointRounding.AwayFromZero);
            Payouts.NationalDebtInterest = Math.Round((AnnualTaxAmount * UKPayouts.NationalDebtInterest), 2, MidpointRounding.AwayFromZero);
            Payouts.Defence = Math.Round((AnnualTaxAmount * UKPayouts.Defence), 2, MidpointRounding.AwayFromZero);
            Payouts.PublicOrderSafety = Math.Round((AnnualTaxAmount * UKPayouts.PublicOrderSafety), 2, MidpointRounding.AwayFromZero);
            Payouts.Transport = Math.Round((AnnualTaxAmount * UKPayouts.Transport), 2, MidpointRounding.AwayFromZero);
            Payouts.BusinessAndIndustry = Math.Round((AnnualTaxAmount * UKPayouts.BusinessAndIndustry), 2, MidpointRounding.AwayFromZero);
            Payouts.GovAdmin = Math.Round((AnnualTaxAmount * UKPayouts.GovAdmin), 2, MidpointRounding.AwayFromZero);
            Payouts.Culture = Math.Round((AnnualTaxAmount * UKPayouts.Culture), 2, MidpointRounding.AwayFromZero);
            Payouts.Environment = Math.Round((AnnualTaxAmount * UKPayouts.Environment), 2, MidpointRounding.AwayFromZero);
            Payouts.HouseAndUtilities = Math.Round((AnnualTaxAmount * UKPayouts.HouseAndUtilities), 2, MidpointRounding.AwayFromZero);
            Payouts.OverseasAid = Math.Round((AnnualTaxAmount * UKPayouts.OverseasAid), 2, MidpointRounding.AwayFromZero);

            return Payouts;
        }

        public async Task<double> UKCalculateAnnualTaxPaid(double AnnualGrossAmount, string Currency)
        {
            TaxBandClass UKTaxBands = await _context.TaxBands.Where(x => x.Currency == Currency).FirstOrDefaultAsync();

            if (UKTaxBands is null) return 0.00;

            double WorkingAmount = 0.00;
            double TaxAmount = 0.00;

            // Check if the annual gross amount is within tax band A
            if (AnnualGrossAmount <= UKTaxBands.Band_A_Limit)
            {
                // No tax for band A
                UKTaxBands.Band_A_Amount = AnnualGrossAmount;
                TaxAmount = 0.00; // No tax for band A
            }
            else
            {
                // Taxable amount for band A
                UKTaxBands.Band_A_Amount = UKTaxBands.Band_A_Limit;
                WorkingAmount = AnnualGrossAmount - UKTaxBands.Band_A_Limit;
            }

            // Check if the remaining amount is within tax band B
            if (WorkingAmount > 0 && WorkingAmount <= (UKTaxBands.Band_B_Limit - UKTaxBands.Band_A_Limit))
            {
                UKTaxBands.Band_B_Amount = WorkingAmount;
                TaxAmount += UKTaxBands.Band_B_Amount * (UKTaxBands.Band_B_Percentage / 100.0); // Calculate tax for band B
            }
            else if (WorkingAmount > (UKTaxBands.Band_B_Limit - UKTaxBands.Band_A_Limit))
            {
                // If the remaining amount exceeds band B limit, calculate tax for band B
                UKTaxBands.Band_B_Amount = UKTaxBands.Band_B_Limit - UKTaxBands.Band_A_Limit;
                TaxAmount += UKTaxBands.Band_B_Amount * (UKTaxBands.Band_B_Percentage / 100.0); // Calculate tax for band B

                // Calculate tax for band C
                UKTaxBands.Band_C_Amount = WorkingAmount - (UKTaxBands.Band_B_Limit - UKTaxBands.Band_A_Limit);
                TaxAmount += UKTaxBands.Band_C_Amount * (UKTaxBands.Band_C_Percentage / 100.0); // Calculate tax for band C
            }

            return TaxAmount;
        }
    }
}
