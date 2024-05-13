using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using AuthReadyAPI.DataLayer.Models.Employees;

namespace AuthReadyAPI.DataLayer.Models.SeederConfigurations
{
    public class EmployeeSeeder : IEntityTypeConfiguration<EmployeeClass>
    {
        public void Configure(EntityTypeBuilder<EmployeeClass> builder)
        {
            builder.HasData(
                    new EmployeeClass
                    {
                        Id = 1,
                        FirstName = "Abe",
                        LastName = "Abeson",
                        LastModifiedByUser = "Seeded",
                        LastModifiedOnDate = DateTime.UtcNow,
                        GrossAnnualSalary = 40000.00,
                        Currency = "GBP"
                    },
                    new EmployeeClass
                    {
                        Id = 2,
                        FirstName = "Alex",
                        LastName = "Alexson",
                        LastModifiedByUser = "Seeded",
                        LastModifiedOnDate = DateTime.UtcNow,
                        GrossAnnualSalary = 36000.00,
                        Currency = "GBP"
                    },
                    new EmployeeClass
                    {
                        Id = 3,
                        FirstName = "Bryan",
                        LastName = "Bryanson",
                        LastModifiedByUser = "Seeded",
                        LastModifiedOnDate = DateTime.UtcNow,
                        GrossAnnualSalary = 50000.00,
                        Currency = "GBP"
                    },
                    new EmployeeClass
                    {
                        Id = 4,
                        FirstName = "Bryce",
                        LastName = "Brycenson",
                        LastModifiedByUser = "Seeded",
                        LastModifiedOnDate = DateTime.UtcNow,
                        GrossAnnualSalary = 75000.00,
                        Currency = "GBP"
                    },
                    new EmployeeClass
                    {
                        Id = 5,
                        FirstName = "Charles",
                        LastName = "Charleston",
                        LastModifiedByUser = "Seeded",
                        LastModifiedOnDate = DateTime.UtcNow,
                        GrossAnnualSalary = 84000.00,
                        Currency = "GBP"
                    },
                    new EmployeeClass
                    {
                        Id = 6,
                        FirstName = "Christina",
                        LastName = "Christinason",
                        LastModifiedByUser = "Seeded",
                        LastModifiedOnDate = DateTime.UtcNow,
                        GrossAnnualSalary = 95000.00,
                        Currency = "GBP"
                    },
                    new EmployeeClass
                    {
                        Id = 7,
                        FirstName = "Dana",
                        LastName = "Danason",
                        LastModifiedByUser = "Seeded",
                        LastModifiedOnDate = DateTime.UtcNow,
                        GrossAnnualSalary = 99000.00,
                        Currency = "GBP"
                    },
                    new EmployeeClass
                    {
                        Id = 8,
                        FirstName = "Dianne",
                        LastName = "Dianneson",
                        LastModifiedByUser = "Seeded",
                        LastModifiedOnDate = DateTime.UtcNow,
                        GrossAnnualSalary = 5000.00,
                        Currency = "GBP"
                    },
                    new EmployeeClass
                    {
                        Id = 9,
                        FirstName = "Deb",
                        LastName = "Deborahson",
                        LastModifiedByUser = "Seeded",
                        LastModifiedOnDate = DateTime.UtcNow,
                        GrossAnnualSalary = 15000.00,
                        Currency = "GBP"
                    },
                    new EmployeeClass
                    {
                        Id = 10,
                        FirstName = "Derek",
                        LastName = "Derekson",
                        LastModifiedByUser = "Seeded",
                        LastModifiedOnDate = DateTime.UtcNow,
                        GrossAnnualSalary = 21000.00,
                        Currency = "GBP"
                    },
                    new EmployeeClass
                    {
                        Id = 11,
                        FirstName = "Erica",
                        LastName = "Ericason",
                        LastModifiedByUser = "Seeded",
                        LastModifiedOnDate = DateTime.UtcNow,
                        GrossAnnualSalary = 24000.00,
                        Currency = "GBP"
                    }
                );
        }
    }
}
