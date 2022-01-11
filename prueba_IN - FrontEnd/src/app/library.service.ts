import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable, of } from 'rxjs';
import { catchError } from 'rxjs/operators';

import { Book, Author } from './models/library';
import { configData } from './config/config_data';

@Injectable({
  providedIn: 'root'
})
export class LibraryService {

  books: Book[] = [];
  authors: Author[] = [];

  constructor(
    private http: HttpClient
  ) { }

  syncBooksDB(): Observable<string> {
    //return this.http.get<string>(configData.syncBooksURL)
    return of('Sincronización realizada')
    .pipe(
      catchError(this.handleError<string>('syncBooksDB', ''))
    );
  }

  syncAuthorsDB(): Observable<string> {
    //return this.http.get<string>(configData.syncAuthorsURL)
    return of('Sincronización realizada')
    .pipe(
      catchError(this.handleError<string>('syncAuthorsDB', ''))
    );
  }

  getAllBooks(): Observable<Book[]> {
    //return this.http.get<Book[]>(configData.getBooksURL)
    let b1: Book = {
      id: 1, title: "Damn",
      description: '',
      pageCount: 0,
      excerpt: '',
      publishDate: ''
    };
    let b2: Book = {
      id: 2, title: "Damn2",
      description: '',
      pageCount: 0,
      excerpt: '',
      publishDate: ''
    };
    let b3: Book = {
      id: 3, title: "Damn3",
      description: '',
      pageCount: 3,
      excerpt: '',
      publishDate: ''
    };
    return of([b1,b2,b3])
    .pipe(
      catchError(this.handleError<Book[]>('getAllBooks', []))
    );
  }

  getAllAuthors(): Observable<Author[]> {
    //return this.http.get<Author[]>(configData.getAuthorsURL)
    let a1: Author = {
      id: 1,
      idBook: 1,
      firstName: 'ad',
      lastName: 'ad'
    };
    let a2: Author = {
      id: 2,
      idBook: 2,
      firstName: 'af',
      lastName: 'sf'
    };
    return of([a1,a2])
    .pipe(
      catchError(this.handleError<Author[]>('getAllAuthors', []))
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
