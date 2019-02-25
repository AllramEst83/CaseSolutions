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
        public const string SignUp = "SignUp";
        public const string AddRole = "AddRole";
        public const string AddUserToRole = "AddUserToRole";
        public const string ZING = "ZING!!!, Gateway open!";
        public const string CommonAuthTestSuccess = "Auth for CommonUser on the Gateway server is workning.";
        public const string AdminAuthTestSuccess = "Auth for Admin on the Gateway server is workning.";
        public const string RemoveUserFromRole = "RemoveUserFromRole";

        public static class AppSettingStrings
        {
            public const string AppSettings = "AppSettings";
            public const string Secret = "Secret";
        }

        public static class JwtIssuer
        {
            public const string JwtIssuerOptions = "JwtIssuerOptions";
        }
    }
}
