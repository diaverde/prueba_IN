using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PruebaIN.Services
{
    public interface ITokenService
    {
        string GenerateToken(User user);
    }

    public class TokenService : ITokenService
    {
        private readonly IJwt _jwt;

        public TokenService(IJwt jwt)
        {
            _jwt = jwt;
        }

        public string GenerateToken(User user)
        {
            JwtPayload token = new JwtPayload
            {
                UserName = user.UserName,
                TokenExpiration = DateTime.Now.AddMinutes(JwtConfig.TokenTime),
            };
            return _jwt.Encode<JwtPayload>(token, JwtConfig.SecretKey);
        }
    }
}