using Microsoft.IdentityModel.Tokens;
using System.Security.Cryptography;
using Northwind.Model;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Collections.Generic;
using System.Security.Claims;

namespace WebApi.Authentication
{
    public class JwtProvider : ITokenProvider
    {
        private RsaSecurityKey _key;
        private string _algoritm;
        private string _issuer;
        private string _audience;

        //CustomerJwtProvider
        public JwtProvider(string issuer, string audience, string keyname)
        {
            var parmaters = new CspParameters() { KeyContainerName = keyname 
                //AZURE Flags = CspProviderFlags.UseMachineKeyStore 
            };
            var provider = new RSACryptoServiceProvider(2048, parmaters);
            _key = new RsaSecurityKey(provider);
            _algoritm = SecurityAlgorithms.RsaSha256Signature;
            _issuer = issuer;
            _audience = audience;
        }

        public string CreateToken(User user, DateTime expire)
        {
            JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();
            var identity = new ClaimsIdentity(new List<Claim>() 
                            {
                                new Claim(ClaimTypes.Name, $"{user.FirstName}{user.LastName}"),
                                new Claim(ClaimTypes.Role, user.Roles),
                                new Claim(ClaimTypes.PrimarySid, user.Id.ToString())
                            }, "Custom" );
            SecurityToken token = tokenHandler.CreateJwtSecurityToken(new SecurityTokenDescriptor
            {
                Audience = _audience,
                Issuer = _issuer,
                SigningCredentials = new SigningCredentials(_key, _algoritm),
                Expires = expire.ToUniversalTime(),
                Subject = identity
            });

            return tokenHandler.WriteToken(token);
        }

        public TokenValidationParameters GetValidationParameters()
        {
            return new TokenValidationParameters {
                IssuerSigningKey = _key,
                ValidAudience = _audience,
                ValidIssuer = _issuer,
                ValidateLifetime = true,
                ClockSkew = TimeSpan.FromSeconds(0)
            };
        }
    }
}