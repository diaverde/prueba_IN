using System;

namespace PruebaIN
{
    public class JwtPayload
    {
        public string UserName { get; set; }
        public DateTime TokenExpiration { get; set; }
    }
}
