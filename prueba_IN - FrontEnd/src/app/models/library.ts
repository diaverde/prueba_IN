export interface Book {
    id: number;
    title: string;
    description: string;
    pageCount: number;
    excerpt: string;
    publishDate: string
}

export interface Author {
    id: number;
    idBook: number;
    firstName: string;
    lastName: string;
}

export interface BookWAuthor {
    id: number;
    title: string;
    description: string;
    pageCount: number;
    excerpt: string;
    publishDate: string;
    author: string[];
}