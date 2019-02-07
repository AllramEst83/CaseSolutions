﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Auth.API.Helpers
{
    public class Constants
    {
        public static class Strings
        {

            public static class APIMessages
            {
                public const string Ping = "ZING!, Accounts is online";
                public const string SuccessMessage = "Account created";
                public const string NotFoundMessage = "user with id: {0} does not exists.";
                public const string ListOfUsers = "This list contains only usernames";
            }

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
