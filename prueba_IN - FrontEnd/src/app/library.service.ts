import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable, of } from 'rxjs';
import { catchError, tap } from 'rxjs/operators';

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

  syncLibraryDB(): Observable<string> {
    let httpOptions = {
      headers: new HttpHeaders({ 'Content-Type': 'text/plain; charset=utf-8' })
    };
    let requestOptions: Object = {
      headers: httpOptions.headers,
      responseType: 'text'
    }
    return this.http.post<string>(configData.syncLibraryURL, '', requestOptions)
      //return of('Sincronización realizada')
      .pipe(
        catchError(this.handleError<string>('syncBooksDB', ''))
      );
  }

  getAllBooks(): Observable<Book[]> {
    return this.http.get<Book[]>(configData.getBooksURL)
      /*
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
      return of([b1, b2, b3])
        */
      .pipe(
        tap(books => this.books = books),
        catchError(this.handleError<Book[]>('getAllBooks', []))
      );
  }

  getAllAuthors(): Observable<Author[]> {
    return this.http.get<Author[]>(configData.getAuthorsURL)
      /*
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
      return of([a1, a2])
      */
      .pipe(
        tap(authors => this.authors = authors),
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
