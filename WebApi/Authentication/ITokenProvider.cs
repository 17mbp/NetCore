using Microsoft.IdentityModel.Tokens;
using Northwind.Model;
using System; 

namespace WebApi.Authentication
{
    public interface ITokenProvider
    {
        string CreateToken(User user, DateTime expire);
        TokenValidationParameters GetValidationParameters();
    }
}