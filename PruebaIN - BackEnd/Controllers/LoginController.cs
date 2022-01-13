using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

using PruebaIN.Services;

namespace PruebaIN.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class LoginController : ControllerBase
    {
        private readonly ITokenService _tokenService;
        
        public LoginController(ITokenService tokenService)
        {
            _tokenService = tokenService;
        }

        [HttpPost]
        public async Task<IActionResult> Post(User user)
        {
            HttpClient _client = new HttpClient();
            User[] validUsers = await SyncController.GetUsers(_client);

            if (validUsers.Length == 0)
            {
                return new BadRequestObjectResult("Error");
            }

            foreach (var item in validUsers)
            {
                if (item.UserName == user.UserName && item.Password == user.Password)
                {
                    Dictionary<string, dynamic> data = new Dictionary<string, dynamic>();
                    data.Add("user", item);
                    data.Add("jwt", _tokenService.GenerateToken(item));
                    return new OkObjectResult(data);
                }
            }
            return new UnauthorizedResult();
        }

    }
}
