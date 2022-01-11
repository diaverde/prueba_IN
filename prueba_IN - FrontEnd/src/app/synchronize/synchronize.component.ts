import { Component, OnInit } from '@angular/core';

import { LibraryService } from '../library.service';
import { Book, Author } from '../models/library';

@Component({
  selector: 'app-synchronize',
  templateUrl: './synchronize.component.html',
  styleUrls: ['./synchronize.component.css']
})
export class SynchronizeComponent implements OnInit {

  isSync: boolean | undefined;
  gotFullCatalog: boolean | undefined;

  constructor(
    private libraryService: LibraryService
  ) { }

  ngOnInit(): void {
  }

  syncDB(): void {
    this.libraryService.syncBooksDB().subscribe(result => {
      //console.log(result);
      if(result === 'Sincronización realizada') {
        this.libraryService.syncAuthorsDB().subscribe(result => {
          //console.log(result);
          if(result === 'Sincronización realizada') {
            this.isSync = true;
            alert('Sincronización realizada');
          } else {
            alert('Falló sincronización. Intente de nuevo más tarde.');
          }
        });
      } else {
        alert('Falló sincronización. Intente de nuevo más tarde.');
      }
    });
  }

  getCatalog(): void {
    this.libraryService.getAllBooks().subscribe(books => {
      //console.log(books);
      if(books.length > 0) {
        this.libraryService.getAllAuthors().subscribe(authors => {
          //console.log(authors);
          if(books.length > 0) {
            this.gotFullCatalog = true;
            alert('Se ha obtenido el catálogo de libros y autores.');
          } else {
            alert('Falló carga de catálogo. Intente de nuevo más tarde.');
          }
        });
      } else {
        alert('Falló carga de catálogo. Intente de nuevo más tarde.');
      }
    });
  }

}
