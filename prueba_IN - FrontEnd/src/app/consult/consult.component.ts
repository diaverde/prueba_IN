import { Component, OnInit } from '@angular/core';

import { LibraryService } from '../library.service';
import { Author, Book, BookWAuthor } from '../models/library';

@Component({
  selector: 'app-consult',
  templateUrl: './consult.component.html',
  styleUrls: ['./consult.component.css']
})
export class ConsultComponent implements OnInit {

  catalogInfo: string | undefined;
  showTable: boolean | undefined;
  totalBooks: number;
  authorNames: string[];
  booksToShow: BookWAuthor[];
  filteredBooksToShow: BookWAuthor[];

  constructor(
    private libraryService: LibraryService
  ) {
    this.totalBooks = libraryService.books.length;
    this.authorNames = [''];
    this.booksToShow = [];
    this.filteredBooksToShow = [];
  }

  ngOnInit(): void {
    this.setCatalogInfo();
    this.getAuthorsForBook();
    this.filteredBooksToShow = this.booksToShow;
  }

  filterData(author: string): void {
    if (author.length === 0) {
      this.filteredBooksToShow = this.booksToShow;
    } else {
      this.filteredBooksToShow = [];
      this.booksToShow.forEach(element => {
        if (element.author.includes(author)) {
          this.filteredBooksToShow.push(element)
        }
      });
    }
  }

  private setCatalogInfo(): void {
    if (this.totalBooks === 0) {
      this.catalogInfo = 'Su catálogo está vacío';
      this.showTable = false;
    } else {
      this.catalogInfo = '';
      this.showTable = true;
    }
  }

  private getAuthorsForBook(): void {
    for (let i = 0; i < this.totalBooks; i++) {
      let bookAuthors: string[] = []
      for (let j = 0; j < this.libraryService.authors.length; j++) {
        if (this.libraryService.books[i].id === this.libraryService.authors[j].idBook) {
          let authName = `${this.libraryService.authors[j].firstName} ${this.libraryService.authors[j].lastName}`;
          bookAuthors.push(authName);
          if (!this.authorNames.includes(authName)) {
            this.authorNames.push(authName);
          }
        }
      }
      let newBookWAuthor: BookWAuthor = { ...this.libraryService.books[i], author: bookAuthors };
      newBookWAuthor.publishDate = newBookWAuthor.publishDate.slice(0, 10);
      this.booksToShow?.push(newBookWAuthor);
    }
  }

}
