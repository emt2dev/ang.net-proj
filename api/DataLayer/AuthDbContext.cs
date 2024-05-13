using AuthReadyAPI.DataLayer.Models.Employees;
using AuthReadyAPI.DataLayer.Models.PII;
using AuthReadyAPI.DataLayer.Models.SeederConfigurations;
using AuthReadyAPI.DataLayer.Models.TaxCalculator;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace AuthReadyAPI.DataLayer
{
    public class AuthDbContext : IdentityDbContext<APIUserClass>
    {
        public AuthDbContext(DbContextOptions options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            /* Role */
            modelBuilder.ApplyConfiguration(new RoleSeeder()); // seeds the roles table
            modelBuilder.ApplyConfiguration(new TaxPayoutsSeeder()); // seeds the tax payouts table
            modelBuilder.ApplyConfiguration(new TaxBandSeeder()); // seeds the tax bands table
            modelBuilder.ApplyConfiguration(new EmployeeSeeder()); // seeds the tax employees table
        }

        // Modelling
        public DbSet<EmployeeClass> Employees { get; set; }
        public DbSet<TaxPayoutsClass> TaxPayouts { get; set; }
        public DbSet<TaxBandClass> TaxBands { get; set; }
    }
}
