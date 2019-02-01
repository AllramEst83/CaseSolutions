using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Auth.API.Helpers
{
    public class Constants
    {
        public static class Strings
        {
            //Add type of claim here: 'role'
            public static class JwtClaimIdentifiers
            {
                public const string Rol = "rol", Id = "id";
            }

            //Add claim here: 'admin', 'user'
            public static class JwtClaims
            {
                public const string ApiAccess = "api_access";
            }

            public static class AppSettingStrings
            {
                public const string AppSettings = "AppSettings";
            }
        }
    }
}
