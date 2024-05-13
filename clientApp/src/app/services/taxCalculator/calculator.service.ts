import { Injectable } from '@angular/core';
import { EmployeeInterface } from '../../data/interfaces/employee.interface';
import { HttpClient } from '@angular/common/http';
import { catchError, map, Observable, of, tap } from 'rxjs';
import { environment } from '../../../../environment';
import { CalculatedTax } from '../../data/interfaces/calculatedTax.interface';

@Injectable({
  providedIn: 'root'
})
export class CalculatorService {
  
  constructor(private http: HttpClient) { }

  getEmployeeList(): Observable<EmployeeInterface[]> {
    return this.http.get<EmployeeInterface[]>(environment.employeeListURL);
  }

  getOneEmployee(employeeId: number): Observable<EmployeeInterface> {
    return this.http.get<EmployeeInterface>(`${environment.singleEmployeeURL}/${employeeId}`);
  }

  getCalculatedTax(DTO : CalculatedTax): Observable<CalculatedTax> {
    return this.http.post<CalculatedTax>(environment.calculateTaxesURL, DTO);
  }

  formattedEconomyAmount(amount: number): number {
    return parseFloat(amount.toFixed(2));
  }

  updateEmployee(DTO: EmployeeInterface): Observable<boolean> {
    return this.http.post<any>(environment.updateEmployeeUrl, DTO)
      .pipe(
        map(response => {
          return response === true;
        }),
        catchError(error => {
          return of(false);
        })
      );
  }
}
