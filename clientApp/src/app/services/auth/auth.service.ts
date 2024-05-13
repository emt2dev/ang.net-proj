import { Injectable, signal } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { tap } from 'rxjs/operators';
import { APIUserInterface } from '../../data/interfaces/apiUser.interface';
import { Router } from '@angular/router';
import { environment } from '../../../../environment';

@Injectable({
  providedIn: 'root'
})
export class AuthService {
  loggedInUserSignal = signal<APIUserInterface | undefined | null>(undefined);

  constructor(private http: HttpClient, private router: Router) { }

  login(email: string, password: string): Observable<{ token: string, refreshToken: string, user: APIUserInterface }> {
    return this.http.post<{ token: string, refreshToken: string, user: APIUserInterface }>(environment.loginURL, { email, password })
      .pipe(
        tap(response => {
          if (response) {
            this.loggedInUserSignal.set(response.user);
            localStorage.setItem('jwt', response.token);
            localStorage.setItem('usersName', response.user.name);

            this.router.navigateByUrl('/demo/one');
            return;
          } else {
            return environment.invalidLoginMessage;
          }
        })
      );
  }
}
