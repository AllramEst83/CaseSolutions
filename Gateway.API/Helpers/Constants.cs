using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Gateway.API.Helpers
{
    public class Constants
    {
        public const string ServerUrls = "ServerUrls";
        public const string Auth = "Auth";
        public const string ZING = "ZING!!!, Gateway open!";
        public const string AuthTestSuccess = "Auth on the Gateway server is workning.";


        public static class Policies
        {
            //Policies
            //public const string GatewayAPIAdmin = "Auth.API.Admin";
            //public const string GatewayAPICommonUser = "Auth.API.CommonUser";
        }


        public static class JwtIssuer
        {
            public const string JwtIssuerOptions = "JwtIssuerOptions";
        }
    }
}
