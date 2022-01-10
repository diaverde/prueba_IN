using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace PruebaIN.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BooksController : ControllerBase
    {
        private readonly ILogger<BooksController> _logger;

        public BooksController(ILogger<BooksController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public IEnumerable<Book> Get()
        {
            return DatabaseController.GetAllBooks();
        }

        [HttpGet("{id}")]
        public Book Get(int id)
        {
            return DatabaseController.GetBook(id);
        }

        [HttpPost]
        public IActionResult Post(Book book)
        {
            if (DatabaseController.InsertBook(book))
            {
                return Ok(book);
            } else
            {
                return new BadRequestObjectResult("Error");
            }
        }

        [HttpPost]
        [Route("Synchronize")]
        public async Task<IActionResult> Post()
        {
            HttpClient _client = new HttpClient();
            Book[] booksToSync = await SyncController.GetBooks(_client);
            
            if (DatabaseController.InsertManyBooks(booksToSync))
            {
                return Ok("Sincronización realizada");
            }
            else
            {
                return new BadRequestObjectResult("Error");
            }
            
        }

    }
}
