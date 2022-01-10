using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PruebaIN.Controllers
{
    public class DatabaseController
    {
        public static SqlConnectionStringBuilder getConnectionString()
        {
            SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder();

            builder.DataSource = DatabaseConfig.DataSource;
            builder.UserID = DatabaseConfig.UserID;
            builder.Password = DatabaseConfig.Password;
            builder.InitialCatalog = DatabaseConfig.InitialCatalog;

            return builder;
        }

        // ---------------------Libros--------------------------------------------

        public static IEnumerable<Book> GetAllBooks()
        {
            SqlConnectionStringBuilder builder = DatabaseController.getConnectionString();
            List<Book> books = new List<Book>();

            try
            {
                using (SqlConnection connection = new SqlConnection(builder.ConnectionString))
                {
                    connection.Open();

                    String sql = "SELECT id, title, description, pageCount, excerpt, publishDate FROM books";

                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                Book book = new Book
                                {
                                    Id = reader.GetInt32(0),
                                    Title = reader.GetString(1),
                                    Description = reader.GetString(2),
                                    PageCount = reader.GetInt32(3),
                                    Excerpt = reader.GetString(4),
                                    PublishDate = reader.GetDateTime(5)
                                };
                                Console.WriteLine("{0} {1}", book.Id, book.Title);

                                books.Add(book);
                            }
                        }
                    }
                }
            }
            catch (SqlException e)
            {
                System.Diagnostics.Debug.WriteLine(e.ToString());
            }

            return books;
        }

        public static Book GetBook(int id)
        {
            SqlConnectionStringBuilder builder = DatabaseController.getConnectionString();

            try
            {
                using (SqlConnection connection = new SqlConnection(builder.ConnectionString))
                {
                    connection.Open();

                    String sql = $"SELECT id, title, description, pageCount, excerpt, publishDate FROM books WHERE id = @value";
                    SqlCommand command = new SqlCommand(sql, connection);
                    command.Parameters.AddWithValue("@value", id);

                    using (command)
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                Book book = new Book
                                {
                                    Id = reader.GetInt32(0),
                                    Title = reader.GetString(1),
                                    Description = reader.GetString(2),
                                    PageCount = reader.GetInt32(3),
                                    Excerpt = reader.GetString(4),
                                    PublishDate = reader.GetDateTime(5)
                                };
                                Console.WriteLine("{0} {1}", book.Id, book.Title);

                                return book;
                            }
                        }
                    }
                }
            }
            catch (SqlException e)
            {
                System.Diagnostics.Debug.WriteLine(e.ToString());
            }

            return null;
        }

        public static bool InsertBook(Book book)
        {
            SqlConnectionStringBuilder builder = DatabaseController.getConnectionString();

            try
            {
                using (SqlConnection connection = new SqlConnection(builder.ConnectionString))
                {
                    connection.Open();

                    String sql = "INSERT INTO books (title, description, pageCount, excerpt, publishDate) "
                        + $"VALUES (@val1, @val2, @val3, @val4, @val5)";

                    SqlCommand command = new SqlCommand(sql, connection);
                    command.Parameters.AddWithValue("@val1", book.Title);
                    command.Parameters.AddWithValue("@val2", book.Description);
                    command.Parameters.AddWithValue("@val3", book.PageCount);
                    command.Parameters.AddWithValue("@val4", book.Excerpt);
                    command.Parameters.AddWithValue("@val5", book.PublishDate);

                    using (command)
                    {
                        int res = command.ExecuteNonQuery();
                        if (res == 1)
                        {
                            return true;
                        }
                        else
                        {
                            return false;
                        }
                    }
                }
            }
            catch (SqlException e)
            {
                System.Diagnostics.Debug.WriteLine(e.ToString());
            }

            return false;
        }

        public static bool InsertManyBooks(Book[] books)
        {
            SqlConnectionStringBuilder builder = DatabaseController.getConnectionString();

            try
            {
                using (SqlConnection connection = new SqlConnection(builder.ConnectionString))
                {
                    connection.Open();

                    string[] values = new string[books.Length];
                    int cont = 0;
                    foreach (Book book in books)
                    {
                        string modifiedDate = book.PublishDate.ToString("yyyy-MM-dd HH:mm:ss.FFF");
                        //System.Diagnostics.Debug.WriteLine(modifiedDate);
                        string val = $"({book.Id}, '{book.Title}', '{book.Description}', {book.PageCount}, '{book.Excerpt}', '{modifiedDate}')";
                        values[cont] = val;
                        cont++;
                    }
                    
                    String sql = "SET IDENTITY_INSERT books ON;"
                        + "DELETE FROM books;"
                        + "INSERT INTO books (id, title, description, pageCount, excerpt, publishDate) "
                        + $"VALUES {String.Join(", ", values)}"
                        + "SET IDENTITY_INSERT books OFF";

                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        int res = command.ExecuteNonQuery();
                        if (res > 0)
                        {
                            return true;
                        }
                        else
                        {
                            return false;
                        }
                    }
                }
            }
            catch (SqlException e)
            {
                System.Diagnostics.Debug.WriteLine(e.ToString());
            }

            return false;
        }

        // ---------------------Autores--------------------------------------------


        public static IEnumerable<Author> GetAllAuthors()
        {
            SqlConnectionStringBuilder builder = DatabaseController.getConnectionString();
            List<Author> authors = new List<Author>();

            try
            {
                using (SqlConnection connection = new SqlConnection(builder.ConnectionString))
                {
                    connection.Open();

                    String sql = "SELECT id, idBook, firstName, lastName FROM authors";

                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                Author author = new Author
                                {
                                    Id = reader.GetInt32(0),
                                    IdBook = reader.GetInt32(1),
                                    FirstName = reader.GetString(2),
                                    LastName = reader.GetString(3)
                                };

                                authors.Add(author);
                            }
                        }
                    }
                }
            }
            catch (SqlException e)
            {
                System.Diagnostics.Debug.WriteLine(e.ToString());
            }

            return authors;
        }

        public static Author GetAuthor(int id)
        {
            SqlConnectionStringBuilder builder = DatabaseController.getConnectionString();

            try
            {
                using (SqlConnection connection = new SqlConnection(builder.ConnectionString))
                {
                    connection.Open();

                    String sql = $"SELECT id, idBook, firstName, lastName FROM authors WHERE id = @value";
                    SqlCommand command = new SqlCommand(sql, connection);
                    command.Parameters.AddWithValue("@value", id);

                    using (command)
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                Author author = new Author
                                {
                                    Id = reader.GetInt32(0),
                                    IdBook = reader.GetInt32(1),
                                    FirstName = reader.GetString(2),
                                    LastName = reader.GetString(3)
                                };

                                return author;
                            }
                        }
                    }
                }
            }
            catch (SqlException e)
            {
                System.Diagnostics.Debug.WriteLine(e.ToString());
            }

            return null;
        }

        public static bool InsertAuthor(Author author)
        {
            SqlConnectionStringBuilder builder = DatabaseController.getConnectionString();

            try
            {
                using (SqlConnection connection = new SqlConnection(builder.ConnectionString))
                {
                    connection.Open();
                    String sql = "INSERT INTO authors (idBook, firstName, lastName) "
                        + $"VALUES (@val1, @val2, @val3)";

                    SqlCommand command = new SqlCommand(sql, connection);
                    command.Parameters.AddWithValue("@val1", author.IdBook);
                    command.Parameters.AddWithValue("@val2", author.FirstName);
                    command.Parameters.AddWithValue("@val3", author.LastName);

                    using (command)
                    {
                        int res = command.ExecuteNonQuery();
                        if (res == 1)
                        {
                            return true;
                        }
                        else
                        {
                            return false;
                        }
                    }
                }
            }
            catch (SqlException e)
            {
                System.Diagnostics.Debug.WriteLine(e.ToString());
            }

            return false;
        }

        public static bool InsertManyAuthors(Author[] authors)
        {
            SqlConnectionStringBuilder builder = DatabaseController.getConnectionString();

            try
            {
                using (SqlConnection connection = new SqlConnection(builder.ConnectionString))
                {
                    connection.Open();

                    string[] values = new string[authors.Length];
                    int cont = 0;
                    foreach (Author author in authors)
                    {
                        string val = $"({author.Id}, {author.IdBook}, '{author.FirstName}', '{author.LastName}')";
                        values[cont] = val;
                        cont++;
                    }
                    
                    String sql = "SET IDENTITY_INSERT authors ON;"
                        + "DELETE FROM authors;"
                        + "INSERT INTO authors (id, idBook, firstName, lastName) "
                        + $"VALUES {String.Join(", ", values)}"
                        + "SET IDENTITY_INSERT authors OFF";

                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        int res = command.ExecuteNonQuery();
                        if (res >= authors.Length)
                        {
                            return true;
                        }
                        else
                        {
                            return false;
                        }
                    }
                }
            }
            catch (SqlException e)
            {
                System.Diagnostics.Debug.WriteLine(e.ToString());
            }

            return false;
        }

    }
}
