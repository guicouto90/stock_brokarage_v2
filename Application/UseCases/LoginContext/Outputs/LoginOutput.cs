using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockBrokarageChallenge.Application.UseCases.LoginContext.Outputs
{
    public class LoginOutput
    {
        public string TokenJwt { get; set; }
        public DateTime Expiration { get; set; }

        public LoginOutput() { }
        public LoginOutput(string tokenJwt, DateTime expiration)
        {
            TokenJwt = tokenJwt;
            Expiration = expiration;
        }
    }
}
