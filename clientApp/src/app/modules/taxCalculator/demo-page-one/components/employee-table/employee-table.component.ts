import { Component, ElementRef, inject, OnInit, ViewChild } from '@angular/core';
import { Subscription } from 'rxjs';
import { CommonModule } from '@angular/common';
import { EmployeeDetailsModalComponent } from '../employee-details-modal/employee-details-modal.component';
import { EmployeeInterface } from '../../../../../data/interfaces/employee.interface';
import { CalculatorService } from '../../../../../services/taxCalculator/calculator.service';

@Component({
  selector: 'app-employee-table',
  standalone: true,
  imports: [CommonModule, EmployeeDetailsModalComponent],
  templateUrl: './employee-table.component.html',
  styleUrl: './employee-table.component.css'
})
export class EmployeeTableComponent implements OnInit {
  employeesList: EmployeeInterface[] = [];
  displayedEmployees: EmployeeInterface[] = [];

  calculatorService = inject(CalculatorService);
  employeeSubscription: Subscription | undefined;

  // Pagination
  currentPage = 1;
  pageSize = 4; // Adjust the page size as needed

  selectedEmployee: EmployeeInterface | undefined; // Property to store the selected employee for the modal
  @ViewChild('employeeModal') employeeModal!: ElementRef;
  
  ngOnInit(): void {
    this.loadEmployeeList();
  }

  updateDisplayedEmployees(): void {
    
    const startIndex = (this.currentPage - 1) * this.pageSize;
    const endIndex = startIndex + this.pageSize;
    this.displayedEmployees = this.employeesList.slice(startIndex, endIndex);
  }

  nextPage(): void {
    if (this.currentPage < this.totalPages.length) {
      this.currentPage++;
      this.updateDisplayedEmployees();
    }
  }

  prevPage(): void {
    if (this.currentPage > 1) {
      this.currentPage--;
      this.updateDisplayedEmployees();
    }
  }

  goToPage(page: number): void {
    if (page >= 1 && page <= this.totalPages.length) {
      this.currentPage = page;
      this.updateDisplayedEmployees();
    }
  }

  get totalPages(): number[] {
    const total = Math.ceil(this.employeesList.length / this.pageSize);
    return Array.from({ length: total }, (_, index) => index + 1);
  }

  loadEmployeeList(): void {
    this.employeeSubscription = this.calculatorService.getEmployeeList().subscribe({
      next: (employeeList: EmployeeInterface[]) => {
        this.employeesList = employeeList;
        this.updateDisplayedEmployees(); // Update displayed employees after loading the list
      },
      error: (error) => {
        alert(`Error fetching employee list: ${error}`);
      }
    });
  }
  onPageChange(pageNumber: number): void {
    this.currentPage = pageNumber;
  }

  getDisplayedEmployees(): EmployeeInterface[] {
    const startIndex = (this.currentPage - 1) * this.pageSize;
    const endIndex = startIndex + this.pageSize;
    return this.employeesList.slice(startIndex, endIndex);
  }

  // Display and close the modal
  selectEmployee(employee: EmployeeInterface): void {
    this.selectedEmployee = employee;
  }

  closeDetailsModal(): void {
    this.selectedEmployee = undefined;
  }  
}
