import { Component, inject, OnInit } from '@angular/core';
import { FormBuilder, ReactiveFormsModule, Validators } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { AuthService } from '../../services/auth/auth.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-login',
  standalone: true,
  imports: [ReactiveFormsModule, CommonModule],
  templateUrl: './login.component.html',
  styleUrl: './login.component.css'
})
export class LoginComponent implements OnInit {
  fb = inject(FormBuilder);
  authService = inject(AuthService);
  router = inject(Router);

  ngOnInit(): void {
    if(localStorage.getItem('jwt')) this.router.navigateByUrl('/demo/one');
  }

  loginForm = this.fb.nonNullable.group({
    email: ['', [Validators.required, Validators.pattern("^([a-zA-Z0-9-._]+)@([A-Za-z-]+)\.([a-z]{2,3}(.[a-z]{2,3})?)$")]],
    password: ['', [Validators.required, Validators.pattern("^(?=.*[^a-zA-Z0-9])(?=.*[a-z])(?=.*[A-Z])(?=.*\d).{8,}$")]]
  });

  submitForm(): void {
    this.authService.login(
      this.loginForm.getRawValue().email,
      this.loginForm.getRawValue().password
    ).subscribe(() => {
      if (!localStorage.getItem('usersName')) {
        alert("Invalid Email and Password.\n\nPlease use the following:\n\nacct@test.com\n\nP@ssword1");
      }
    });
  }
}
