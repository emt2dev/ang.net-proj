import { Component, EventEmitter, inject, Input, OnInit, Output } from '@angular/core';
import { EmployeeInterface } from '../../../../data/interfaces/employee.interface';
import { CommonModule } from '@angular/common';
import { CalculatorService } from '../../../../services/taxCalculator/calculator.service';
import { FormBuilder, Validators } from '@angular/forms';
import { BrowserModule } from '@angular/platform-browser';
import { FormsModule } from '@angular/forms';

@Component({
  selector: 'app-update-modal',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './update-modal.component.html',
  styleUrl: './update-modal.component.css'
})
export class UpdateModalComponent {

  fb = inject(FormBuilder);
  calculatorService = inject(CalculatorService);
  errors = "";

  @Input() employee: EmployeeInterface | undefined;
  @Output() close: EventEmitter<void> = new EventEmitter<void>();

  submitForm(grossAnnualSalary: number): void {
    if(this.employee !== null || this.employee !== undefined) {
      const EI: EmployeeInterface = {
        firstName: this.employee?.firstName ? this.employee.firstName : "",
        lastName: this.employee?.lastName ? this.employee.lastName : "",
        grossAnnualSalary: this.employee?.grossAnnualSalary ? this.employee.grossAnnualSalary : grossAnnualSalary,
        id: this.employee?.id ? this.employee.id : "",
        lastModifiedOnDate: this.employee?.lastModifiedOnDate ? this.employee.lastModifiedOnDate : "",
        lastModifiedByUser: this.employee?.lastModifiedByUser ? this.employee.lastModifiedByUser : "",
        currency: this.employee?.currency ? this.employee.currency : "",
      }

      this.calculatorService.updateEmployee(EI).subscribe(result => {
        alert(result);
      });
    }    
  }
}