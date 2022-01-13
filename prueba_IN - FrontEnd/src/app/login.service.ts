import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable, of } from 'rxjs';
import { catchError, tap } from 'rxjs/operators';

import { User, LoginUserData } from './models/user';
import { configData } from './config/config_data';

@Injectable({
  providedIn: 'root'
})
export class LoginService {

  token: string | undefined;
  errorMessage: string | undefined;

  constructor(
    private http: HttpClient
  ) { }

  login(user: User): Observable<LoginUserData> {
    return this.http.post<LoginUserData>(configData.loginURL, user)
      .pipe(
        tap(userData => this.token = userData.jwt),
        catchError(this.handleError<LoginUserData>('login'))
      );
  }

  private handleError<T>(operation = 'operation', result?: T) {
    return (error: any): Observable<T> => {
      console.error(`Operación ${operation} falló`);
      console.error(error);
      this.errorMessage
      // Retornar resultado esperado para no bloquear aplicación
      return of(result as T);
    };
  }
}
