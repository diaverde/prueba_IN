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
    public class AuthorsController : ControllerBase
    {
        private readonly ILogger<BooksController> _logger;

        public AuthorsController(ILogger<BooksController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public IEnumerable<Author> Get()
        {
            return DatabaseController.GetAllAuthors();
        }

        [HttpGet("{id}")]
        public Author Get(int id)
        {
            return DatabaseController.GetAuthor(id);
        }

        [HttpPost]
        public IActionResult Post(Author author)
        {
            if (DatabaseController.InsertAuthor(author))
            {
                return Ok(author);
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
            Author[] authorsToSync = await SyncController.GetAuthors(_client);
            
            if (DatabaseController.InsertManyAuthors(authorsToSync))
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
