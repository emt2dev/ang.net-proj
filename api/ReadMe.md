This project was built using AuthReadyAPI, an open-source pseudo-framework that uses .NET Web API and JWT.

===================================
EmployeeDTO Class Documentation
===================================

Namespace: AuthReadyAPI.DataLayer.DTOs.Employees

Description:
Data Transfer Object (DTO) representing an employee.

Properties:
- FirstName (string): The first name of the employee.
- LastName (string): The last name of the employee.
- GrossAnnualSalary (double): The gross annual salary of the employee.

===================================
BaseDTO Class Documentation
===================================

Namespace: AuthReadyAPI.DataLayer.DTOs

Description:
Base Data Transfer Object (DTO) with common properties.

Properties:
- Id (int): The unique identifier of the DTO.
- LastModifiedOnDate (DateTime): The date when the DTO was last modified.
- LastModifiedByUser (string): The user who last modified the DTO.

===================================
BaseTaxDTO Class Documentation
===================================

Namespace: AuthReadyAPI.DataLayer.DTOs.TaxCalculator

Description:
Base Data Transfer Object (DTO) for tax-related entities.

Properties:
- Currency (string): The currency in which tax calculations are done.

===================================
CalculatedTaxDTO Class Documentation
===================================

Namespace: AuthReadyAPI.DataLayer.DTOs.TaxCalculator

Description:
Data Transfer Object (DTO) representing calculated tax details for an employee.

Properties:
- Currency (string): The currency in which tax calculations are done.
- EmployeeId (int): The ID of the employee.
- GrossAnnualSalary (double): The gross annual salary of the employee.
- GrossMonthlySalary (double): The gross monthly salary of the employee.
- NetAnnualSalary (double): The net annual salary of the employee.
- NetMonthlySalary (double): The net monthly salary of the employee.
- AnnualTaxPaid (double): The annual tax paid by the employee.
- MonthlyTaxPaid (double): The monthly tax paid by the employee.
- Payouts (TaxPayoutsDTO): Optional tax payouts for the employee.

===================================
TaxPayoutsDTO Class Documentation
===================================

Namespace: AuthReadyAPI.DataLayer.DTOs.TaxCalculator

Description:
Data Transfer Object (DTO) representing tax payouts for an employee.

Properties:
- Currency (string): The currency in which tax payouts are calculated.
- Welfare (double): Amount allocated for welfare.
- Health (double): Amount allocated for health.
- StatePensions (double): Amount allocated for state pensions.
- Education (double): Amount allocated for education.
- NationalDebtInterest (double): Amount allocated for national debt interest.
- Defence (double): Amount allocated for defense.
- PublicOrderSafety (double): Amount allocated for public order safety.
- Transport (double): Amount allocated for transport.
- BusinessAndIndustry (double): Amount allocated for business and industry.
- GovAdmin (double): Amount allocated for government administration.
- Culture (double): Amount allocated for culture.
- Environment (double): Amount allocated for environment.
- HouseAndUtilities (double): Amount allocated for housing and utilities.
- OverseasAid (double): Amount allocated for overseas aid.

===================================
TaxCalculatorRepository Class Documentation
===================================

Namespace: AuthReadyAPI.DataLayer.Repositories

Description:
Repository class for performing tax calculations.

Dependencies:
- AuthDbContext: DbContext for accessing the database.
- ITaxCalculatorRepository: Interface defining methods for tax calculation.

Methods:
- Calculate: Calculates tax for an employee based on provided data.
- CalculateMonthly: Calculates the monthly amount based on the annual amount.
- UKGetPayoutsDTO: Calculates tax payouts for the UK based on annual tax amount and currency.
- UKCalculateAnnualTaxPaid: Calculates the annual tax paid for the UK based on the annual gross amount and currency.


===================================
EmployeeRepository Class Documentation
===================================

Namespace: AuthReadyAPI.DataLayer.Repositories

Description:
Repository class for accessing and manipulating employee data.

Dependencies:
- AuthDbContext: DbContext for accessing the database.
- IMapper: AutoMapper for object mapping.

Methods:
- GetAllEmployees: Retrieves all employees from the database and maps them to EmployeeDTO objects.
- UpdateEmployee: Updates an employee in the database based on the provided EmployeeDTO.

