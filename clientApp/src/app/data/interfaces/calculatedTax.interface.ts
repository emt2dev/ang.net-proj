import { TaxPayouts } from "./taxPayouts.interface";

export interface CalculatedTax {
    "currency" : string,
    "employeeId": string,
    "grossAnnualSalary": number,
    "grossMonthlySalary": number,
    "netAnnualSalary": number,
    "netMonthlySalary": number,
    "annualTaxPaid": number,
    "monthlyTaxPaid": number,
    "payouts": TaxPayouts
}