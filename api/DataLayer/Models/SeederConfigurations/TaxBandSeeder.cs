using AuthReadyAPI.DataLayer.Models.TaxCalculator;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace AuthReadyAPI.DataLayer.Models.SeederConfigurations
{
    public class TaxBandSeeder : IEntityTypeConfiguration<TaxBandClass>
    {
        public void Configure(EntityTypeBuilder<TaxBandClass> builder)
        {
            builder.HasData(
                    new TaxBandClass
                    {
                        Id = 1,
                        LastModifiedByUser = "Seeded",
                        LastModifiedOnDate = DateTime.UtcNow,
                        Currency = "GBP",

                        Band_A_Limit = 5000.00,
                        Band_A_Amount = 0.00,
                        Band_A_Percentage = 0,

                        Band_B_Limit = 20000.00,
                        Band_B_Amount = 0.00,
                        Band_B_Percentage = 20,

                        Band_C_Amount = 0.00,
                        Band_C_Percentage = 40
                    }
                );
        }
    }
}
