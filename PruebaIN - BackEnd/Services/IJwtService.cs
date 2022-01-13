using System.Collections.Generic;

namespace PruebaIN.Services
{
    public interface IJwt
    {
        void setDataConfig(IDictionary<string, string> dataConfig);
        string Encode<T>(T data, string secret);
        T Decode<T>(string token, string secret = null);
        IDictionary<string, object> GetClaims(string token);
        bool ValidateToken(string token, string isClaims);
        string renew(string token);
        bool validateExpToken(string token);
        bool IsValidToken<T>(string token, string secret);
    }
}