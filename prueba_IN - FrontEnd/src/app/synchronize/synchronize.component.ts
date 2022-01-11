import { Component, OnInit } from '@angular/core';

import { LibraryService } from '../library.service';
import { Book, Author } from '../models/library';

@Component({
  selector: 'app-synchronize',
  templateUrl: './synchronize.component.html',
  styleUrls: ['./synchronize.component.css']
})
export class SynchronizeComponent implements OnInit {

  catalogInfo: string | undefined;

  constructor(
    private libraryService: LibraryService
  ) { }

  ngOnInit(): void {
    this.setCatalogInfo();
  }

  syncDB(): void {
    this.libraryService.syncLibraryDB().subscribe(result => {
      //console.log(result);
      if (result === 'Sincronización realizada') {
        alert('Sincronización realizada');
      } else {
        alert('Falló sincronización. Intente de nuevo más tarde.');
      }
    });
  }

  getCatalog(): void {
    this.libraryService.getAllBooks().subscribe(books => {
      //console.log(books);
      if (books.length > 0) {
        this.libraryService.getAllAuthors().subscribe(authors => {
          //console.log(authors);
          if (books.length > 0) {
            alert('Se ha obtenido el catálogo de libros y autores.');
            this.setCatalogInfo();
          } else {
            alert('Falló carga de catálogo. Intente de nuevo más tarde.');
          }
        });
      } else {
        alert('Falló carga de catálogo. Intente de nuevo más tarde.');
      }
    });
  }

  private setCatalogInfo(): void {
    if (this.libraryService.books.length === 0) {
      this.catalogInfo = 'Su catálogo está vacío';
    } else {
      this.catalogInfo = `Su catálogo cuenta con ${this.libraryService.books.length} 
        libros y ${this.libraryService.authors.length} autores`;
    }
  }

}
