using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace APIGateway.Models
{
    public class AuthToken
    {
        //public string refreshToken { get; set; }
        public DateTime ExpirationTime { get; set; }

        public string accessToken { get; set; }
        public string email { get; set; }
        public List<string> roles { get; set; }
        public string userName { get; set; }
    }
}
