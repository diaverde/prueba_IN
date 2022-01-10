using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Net.Http;
using System.Text.Json;

namespace PruebaIN.Controllers
{
    public class SyncController
    {
        private static readonly string syncURL = "https://fakerestapi.azurewebsites.net/api/v1/";

        public static async Task<Book[]> GetBooks(HttpClient _client)
        {
            const string extraRoute = "Books";
            var url = syncURL + extraRoute;

            Book[] _books;

            try
            {
                var streamTask = _client.GetStreamAsync(url);
                JsonSerializerOptions options = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                };
                _books = await JsonSerializer.DeserializeAsync<Book[]>(await streamTask, options);

                if (_books.Length == 0)
                {
                    System.Diagnostics.Debug.WriteLine(String.Join("\r\n", new string[] {
                        "No hay libros a cargar",
                    }));
                }
                //System.Diagnostics.Debug.WriteLine(JsonSerializer.Serialize(_books));
                return _books;
            }
            catch (HttpRequestException e)
            {
                System.Diagnostics.Debug.WriteLine("\nExcepción:");
                System.Diagnostics.Debug.WriteLine($"Message :{e.Message} ");
            }
            return new Book[0];
        }

        public static async Task<Author[]> GetAuthors(HttpClient _client)
        {
            const string extraRoute = "Authors";
            var url = syncURL + extraRoute;

            Author[] _authors;

            try
            {
                var streamTask = _client.GetStreamAsync(url);
                JsonSerializerOptions options = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                };
                _authors = await JsonSerializer.DeserializeAsync<Author[]>(await streamTask, options);

                if (_authors.Length == 0)
                {
                    System.Diagnostics.Debug.WriteLine(String.Join("\r\n", new string[] {
                        "No hay autores a cargar",
                    }));
                }
                //System.Diagnostics.Debug.WriteLine(JsonSerializer.Serialize(_books));
                return _authors;
            }
            catch (HttpRequestException e)
            {
                System.Diagnostics.Debug.WriteLine("\nExcepción:");
                System.Diagnostics.Debug.WriteLine($"Message :{e.Message} ");
            }
            return new Author[0];
        }
    }
}
