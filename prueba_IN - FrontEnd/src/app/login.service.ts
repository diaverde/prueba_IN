import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable, of } from 'rxjs';
import { catchError } from 'rxjs/operators';

import { User } from './models/user';
import { configData } from './config/config_data';

@Injectable({
  providedIn: 'root'
})
export class LoginService {

  constructor(
    private http: HttpClient
  ) { }

  getUsers(): Observable<User[]> {
    return this.http.get<User[]>(configData.getUsersURL)
      .pipe(
        catchError(this.handleError<User[]>('getUsers', []))
      );
  }

  private handleError<T>(operation = 'operation', result?: T) {
    return (error: any): Observable<T> => {
      console.error(`Operación ${operation} falló`);
      console.error(error);
      // Retornar resultado esperado para no bloquear aplicación
      return of(result as T);
    };
  }
}
