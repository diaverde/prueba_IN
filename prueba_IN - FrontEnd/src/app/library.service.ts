import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';

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

  getAllBooks() {
    return this.http.get<Book[]>(configData.getBooksURL);
  }

  getAllAuthors() {
    return this.http.get<Author[]>(configData.getAuthorsURL);
  }
}
