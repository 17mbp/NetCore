using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.Authentication
{
    public class JsonWebToken
    {
        public string Access_Token { get; set; }
        public string Token_Type { get; set; } = "bearer";            
        public int Expiries_in { get; set; }
        public string Refresh_Token { get; set; }
    }
}