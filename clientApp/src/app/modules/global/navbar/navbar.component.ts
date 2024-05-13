import { Component, inject, OnInit } from '@angular/core';
import { AuthService } from '../../../services/auth/auth.service';
import { CommonModule } from '@angular/common';
import { Router, RouterModule } from '@angular/router';
import { Subscription } from 'rxjs';

@Component({
  selector: 'app-navbar',
  standalone: true,
  imports: [CommonModule, RouterModule],
  templateUrl: './navbar.component.html',
  styleUrl: './navbar.component.css'
})
export class NavbarComponent implements OnInit {
  authService = inject(AuthService);
  router = inject(Router);

  userIsLoggedIn: Boolean = false; 
  usersName: string = '';

  loggedInUserName: string | undefined;

  loggedInUserSubscription: Subscription | undefined;

  
  constructor() {}

  ngOnInit(): void {
    if(localStorage.getItem('usersName') && localStorage.getItem('jwt')  ) {
      const usersNameFromLS = localStorage.getItem('usersName');
      this.usersName = usersNameFromLS ? usersNameFromLS : "";
    }
  }

  logout() {
    localStorage.clear();
    this.authService.loggedInUserSignal.set(null);
    this.router.navigateByUrl('/')
  }
}
