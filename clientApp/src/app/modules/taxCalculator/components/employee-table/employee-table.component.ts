import { Component, ElementRef, inject, OnInit, ViewChild } from '@angular/core';
import { CalculatorService } from '../../../../services/taxCalculator/calculator.service';
import { EmployeeInterface } from '../../../../data/interfaces/employee.interface';
import { debounceTime, distinctUntilChanged, fromEvent, map, Subscription } from 'rxjs';
import { CommonModule } from '@angular/common';
import { DemoPageTwoComponent } from '../../demo-page-two/demo-page-two.component';
import { Router } from '@angular/router';

@Component({
  selector: 'app-employee-table',
  standalone: true,
  imports: [CommonModule, DemoPageTwoComponent],
  templateUrl: './employee-table.component.html',
  styleUrl: './employee-table.component.css'
})
export class EmployeeTableComponent implements OnInit {
  @ViewChild('searchInput') searchInput!: ElementRef<HTMLInputElement>;

  router = inject(Router);

  employeesList: EmployeeInterface[] = [];
  displayedEmployees: EmployeeInterface[] = [];
  filteredEmployees: EmployeeInterface[] = [];

  calculatorService = inject(CalculatorService);
  employeeSubscription: Subscription | undefined;

  // Pagination
  currentPage = 1;
  pageSize = 4; // Adjust the page size as needed

  selectedEmployee: EmployeeInterface | undefined; // Property to store the selected employee for the modal
  
  ngOnInit(): void {
    this.loadEmployeeList();
    this.setupSearchInput();
  }

  setupSearchInput(): void {
    fromEvent(this.searchInput.nativeElement, 'input')
      .pipe(
        map((event: any) => event.target.value),
        debounceTime(300),
        distinctUntilChanged()
      )
      .subscribe((searchTerm: string) => {
        this.filteredEmployees = this.employeesList.filter(employee =>
          employee.firstName.toLowerCase().includes(searchTerm.toLowerCase()) ||
          employee.lastName.toLowerCase().includes(searchTerm.toLowerCase())
        );
        
        if(this.currentPage !== 1) {
          this.currentPage = 1;
        }

        const startIndex = (this.currentPage - 1) * this.pageSize;
        const endIndex = startIndex + this.pageSize;
        this.displayedEmployees = this.filteredEmployees.slice(startIndex, endIndex);
      });
  }

  updateDisplayedEmployees(): void {
    const startIndex = (this.currentPage - 1) * this.pageSize;
    const endIndex = startIndex + this.pageSize;
    this.displayedEmployees = this.filteredEmployees.slice(startIndex, endIndex);
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
    const total = Math.ceil(this.filteredEmployees.length / this.pageSize);
    return Array.from({ length: total }, (_, index) => index + 1);
  }

  loadEmployeeList(): void {
    this.employeeSubscription = this.calculatorService.getEmployeeList().subscribe({
      next: (employeeList: EmployeeInterface[]) => {
        this.employeesList = employeeList;
        this.filteredEmployees = [...employeeList];
        this.updateDisplayedEmployees(); // Update displayed employees after loading the list
      },
      error: (error) => {
        alert(`Error fetching employee list: ${error}`);
      }
    });
  }

  // Display and close the modal
  selectEmployee(employee: EmployeeInterface): void {
    this.selectedEmployee = employee;
    this.router.navigateByUrl(`/demo/two/${this.selectedEmployee.id}`);
  }

  clearSearch(): void {
    this.searchInput.nativeElement.value = '';
    this.filteredEmployees = [...this.employeesList]; // Reset the filtered employees array
    this.updateDisplayedEmployees(); // Update displayed employees
  }
}
