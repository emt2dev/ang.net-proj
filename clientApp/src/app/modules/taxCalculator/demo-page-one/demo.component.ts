import { Component } from '@angular/core';
import { NavbarComponent } from '../../global/navbar/navbar.component';
import { EmployeeTableComponent } from '../components/employee-table/employee-table.component';

@Component({
  selector: 'app-demo',
  standalone: true,
  imports: [NavbarComponent, EmployeeTableComponent],
  templateUrl: './demo.component.html',
  styleUrl: './demo.component.css'
})
export class DemoPageOneComponent {

}
