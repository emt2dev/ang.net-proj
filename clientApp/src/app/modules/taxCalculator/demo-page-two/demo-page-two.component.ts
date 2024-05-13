import { CommonModule } from "@angular/common";
import { Component, inject, Input, Output, EventEmitter, OnInit } from "@angular/core";
import { tap } from "rxjs";
import { environment } from "../../../../../environment";
import { CalculatedTax } from "../../../data/interfaces/calculatedTax.interface";
import { EmployeeInterface } from "../../../data/interfaces/employee.interface";
import { TaxPayouts } from "../../../data/interfaces/taxPayouts.interface";
import { CalculatorService } from "../../../services/taxCalculator/calculator.service";
import { ActivatedRoute } from "@angular/router";
import { UpdateModalComponent } from "../components/update-modal/update-modal.component";
import { NavbarComponent } from "../../global/navbar/navbar.component";

@Component({
  selector: 'app-demo-page-two',
  standalone: true,
  imports: [CommonModule, UpdateModalComponent, NavbarComponent],
  templateUrl: './demo-page-two.component.html',
  styleUrl: './demo-page-two.component.css'
})
export class DemoPageTwoComponent implements OnInit {
  activatedRoute = inject(ActivatedRoute);
  employeeId: number = 0;

  ngOnInit() {
    const intermediary = this.activatedRoute.snapshot.params['employeeId'];
    this.employeeId = intermediary;
    this.getEmployee();
  }

  disabledCalculationButton: boolean = false;
  calculatedCurrencyOptions: string[] = ["GBP"];

  currencyBasedTaxPayouts: TaxPayouts = {
    "welfare": 0,
    "health": 0,
    "statePensions": 0,
    "education": 0,
    "nationalDebtInterest": 0,
    "defence": 0,
    "publicOrderSafety": 0,
    "transport": 0,
    "businessAndIndustry": 0,
    "govAdmin": 0,
    "culture": 0,
    "environment": 0,
    "houseAndUtilities": 0,
    "overseasAid": 0,
    "currency": "",
    "id": 0, 
    "lastModifiedOnDate": environment.staticDateForOutgoing,
    "lastModifiedByUser": ""
  };

  calculatedWithTaxPayouts: CalculatedTax = {
    "currency" : "",
    "employeeId": "",
    "grossAnnualSalary": 0,
    "grossMonthlySalary": 0,
    "netAnnualSalary": 0,
    "netMonthlySalary": 0,
    "annualTaxPaid": 0,
    "monthlyTaxPaid": 0,
    "payouts": this.currencyBasedTaxPayouts
  };

  calculatorService = inject(CalculatorService);

  employee: EmployeeInterface = {
    firstName: "",
    lastName: "",
    grossAnnualSalary: 0,
    id: "",
    lastModifiedOnDate: "",
    lastModifiedByUser: "",
    currency: ""
  };

  getEmployee(): void {
    this.calculatorService.getOneEmployee(this.employeeId).subscribe({
      next: (employee: EmployeeInterface) => {
        this.employee = employee;

        this.calculateTax();
      },
      error: (error) => {
        alert(`Error fetching employee: ${error}`);
      }
    });
  }
  

  updateDisplayedEmployees() {
    throw new Error("Method not implemented.");
  }

  calculateTax(): void {
    this.disabledCalculationButton = true;
      this.calculatedWithTaxPayouts.employeeId = this.employee.id;
      this.calculatedWithTaxPayouts.currency = this.employee.currency;
      this.calculatedWithTaxPayouts.payouts.currency = this.employee.currency;
      this.calculatedWithTaxPayouts.grossAnnualSalary = this.employee.grossAnnualSalary;

      this.calculatorService.getCalculatedTax(this.calculatedWithTaxPayouts).pipe(
        tap((result) => {
          this.calculatedWithTaxPayouts = result;
          this.calculatedWithTaxPayouts.payouts.lastModifiedOnDate = this.calculatedWithTaxPayouts.payouts?.lastModifiedOnDate.substring(0, 10);
          this.calculatedWithTaxPayouts.payouts.welfare = this.calculatorService.formattedEconomyAmount(this.calculatedWithTaxPayouts.payouts.welfare);
          this.calculatedWithTaxPayouts.payouts.health = this.calculatorService.formattedEconomyAmount(this.calculatedWithTaxPayouts.payouts.health);
          this.calculatedWithTaxPayouts.payouts.statePensions = this.calculatorService.formattedEconomyAmount(this.calculatedWithTaxPayouts.payouts.statePensions);
          this.calculatedWithTaxPayouts.payouts.education = this.calculatorService.formattedEconomyAmount(this.calculatedWithTaxPayouts.payouts.education);
          this.calculatedWithTaxPayouts.payouts.nationalDebtInterest = this.calculatorService.formattedEconomyAmount(this.calculatedWithTaxPayouts.payouts.nationalDebtInterest);
          this.calculatedWithTaxPayouts.payouts.defence = this.calculatorService.formattedEconomyAmount(this.calculatedWithTaxPayouts.payouts.defence);
          this.calculatedWithTaxPayouts.payouts.publicOrderSafety = this.calculatorService.formattedEconomyAmount(this.calculatedWithTaxPayouts.payouts.publicOrderSafety);
          this.calculatedWithTaxPayouts.payouts.transport = this.calculatorService.formattedEconomyAmount(this.calculatedWithTaxPayouts.payouts.transport);
          this.calculatedWithTaxPayouts.payouts.businessAndIndustry = this.calculatorService.formattedEconomyAmount(this.calculatedWithTaxPayouts.payouts.businessAndIndustry);
          this.calculatedWithTaxPayouts.payouts.govAdmin = this.calculatorService.formattedEconomyAmount(this.calculatedWithTaxPayouts.payouts.govAdmin);
          this.calculatedWithTaxPayouts.payouts.culture = this.calculatorService.formattedEconomyAmount(this.calculatedWithTaxPayouts.payouts.culture);
          this.calculatedWithTaxPayouts.payouts.environment = this.calculatorService.formattedEconomyAmount(this.calculatedWithTaxPayouts.payouts.environment);
          this.calculatedWithTaxPayouts.payouts.houseAndUtilities = this.calculatorService.formattedEconomyAmount(this.calculatedWithTaxPayouts.payouts.houseAndUtilities);
          this.calculatedWithTaxPayouts.payouts.culture = this.calculatorService.formattedEconomyAmount(this.calculatedWithTaxPayouts.payouts.overseasAid);
        })
      )
      .subscribe({
        next: () => {
          this.disabledCalculationButton = false;
        },
        error: (error) => {
          alert(`Error received, please contact help desk ${error}`);
        }
      });
  }
}
