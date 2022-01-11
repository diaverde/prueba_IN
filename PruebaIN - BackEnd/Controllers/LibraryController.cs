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
    public class LibraryController : ControllerBase
    {
        private readonly ILogger<BooksController> _logger;

        public LibraryController(ILogger<BooksController> logger)
        {
            _logger = logger;
        }

        [HttpPost]
        [Route("Synchronize")]
        public async Task<IActionResult> Post()
        {
            HttpClient _client = new HttpClient();
            Author[] authorsToSync = await SyncController.GetAuthors(_client);
            Book[] booksToSync = await SyncController.GetBooks(_client);

            if (DatabaseController.ReloadDatabase(authorsToSync, booksToSync))
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
