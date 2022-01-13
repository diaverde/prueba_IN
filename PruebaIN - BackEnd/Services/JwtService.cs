using System;
using System.Collections.Generic;
using JWT.Builder;
using JWT.Algorithms;
using Newtonsoft.Json;
using JWT;
using JWT.Serializers;
using System.Configuration;

namespace PruebaIN.Services
{
    public class Jwt : IJwt
    {
        private readonly IJwtAlgorithm _algorithm;
        private readonly IJsonSerializer _serializer;

        private readonly IBase64UrlEncoder _base64Encoder;
        private readonly IJwtEncoder _jwtEncoder;

        private readonly IJwtDecoder _jwtdecoder;
        private readonly IJwtValidator _jwtValidator;

        private IDictionary<string, string> _dataConfig;
        public Jwt()
        {
            _algorithm = new HMACSHA256Algorithm();
            _serializer = new JsonNetSerializer();
            _base64Encoder = new JwtBase64UrlEncoder();
            _jwtEncoder = new JwtEncoder(_algorithm, _serializer, _base64Encoder);
            _serializer = new JsonNetSerializer();
            var provider = new UtcDateTimeProvider();
            _jwtValidator = new JwtValidator(_serializer, provider);
            _jwtdecoder = new JwtDecoder(_serializer, _jwtValidator, _base64Encoder, _algorithm);
        }

        public void setDataConfig(IDictionary<string, string> dataConfig)
        {
            _dataConfig = dataConfig;
        }

        public string Encode<T>(T data, string secret)
        {
            return _jwtEncoder.Encode(data, secret);
        }

        public T Decode<T>(string token, string secret)
        {
            IDictionary<string, object> payload = new JwtBuilder()
                    .WithAlgorithm(new HMACSHA256Algorithm())
                    .WithSecret(_dataConfig["secret"])
                    .MustVerifySignature()
                    .Decode<IDictionary<string, object>>(token);
            return JsonConvert.DeserializeObject<T>(JsonConvert.SerializeObject(payload));
        }

        public IDictionary<string, object> GetClaims(string token)
        {
            string secret = _dataConfig["secret"];

            if (token.Contains("Bearer"))
            {
                token = token.Substring(7);
            }

            IDictionary<string, object> claims = new Dictionary<string, object>();
            try
            {
                claims = new JwtBuilder()
                    .WithAlgorithm(new HMACSHA256Algorithm())
                    .WithSecret(secret)
                    .Decode<IDictionary<string, object>>(token);
            }
            catch (Exception e)
            {
                Console.WriteLine($"An error occurred while decoding: {e.Message}");
            }

            return claims;
        }

        public bool ValidateToken(string token, string isClaims)
        {
            IDictionary<string, object> claims = GetClaims(token);

            if (claims.Count <= 0)
            {
                Console.WriteLine("Error in obtaining claims");
                return false;
            }

            if (token.Equals("invalid"))
            {
                Console.WriteLine("It is an invalid token");
                return false;
            }

            if (DateTime.Compare(Convert.ToDateTime(claims[_dataConfig["exp"]]), DateTime.Now) <= 0)
            {
                Console.WriteLine(_dataConfig["exp"]);
                Console.WriteLine("The date is overdue");
                return false;
            }

            if (!claims.ContainsKey(isClaims))
            {
                Console.WriteLine("Does not contain the id");
                return false;
            }

            return true;
        }

        public string renew(string token)
        {
            IDictionary<string, object> claims = GetClaims(token);
            if (ValidateToken(token, _dataConfig["isClaims"]))
            {
                claims[_dataConfig["exp"]] = DateTime.Now.AddDays(1);
                return "Bearer-" + _jwtEncoder.Encode(claims, _dataConfig["secret"]);
            }
            else
            {
                return "invalid";
            }
        }

        public bool validateExpToken(string token)
        {
            IDictionary<string, object> claims = GetClaims(token);
            if (claims.ContainsKey(_dataConfig["exp"]))
            {
                if (DateTime.Compare(Convert.ToDateTime(claims[_dataConfig["exp"]]), DateTime.Now) <= 0)
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
            else
            {
                return false;
            }
        }
        public bool IsValidToken<T>(string token, string secret)
        {
            try
            {
                _jwtdecoder.DecodeToObject<T>(token, secret, verify: true);
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}