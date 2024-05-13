using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using AuthReadyAPI.DataLayer.Models.TaxCalculator;

namespace AuthReadyAPI.DataLayer.Models.SeederConfigurations
{
    public class TaxPayoutsSeeder : IEntityTypeConfiguration<TaxPayoutsClass>
    {
        public void Configure(EntityTypeBuilder<TaxPayoutsClass> builder)
        {
            builder.HasData(
                    new TaxPayoutsClass
                    {
                        Id = 1,
                        LastModifiedByUser = "Seeded",
                        LastModifiedOnDate = DateTime.UtcNow,
                        Currency = "GBP",
                        Welfare = 0.243,
                        Health = 0.203,
                        StatePensions = 0.129,
                        Education = 0.123,
                        NationalDebtInterest = 0.055,
                        Defence = 0.052,
                        PublicOrderSafety = 0.042,
                        Transport = 0.042,
                        BusinessAndIndustry = 0.025,
                        GovAdmin = 0.021,
                        Culture = 0.016,
                        Environment = 0.016,
                        HouseAndUtilities = 0.015,
                        OverseasAid = 0.011
                    }
                );
        }
    }
}
