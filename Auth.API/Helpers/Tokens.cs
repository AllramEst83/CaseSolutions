using Auth.API.AuthFactory;
using Auth.API.Models;
using Auth.API.ViewModels;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Auth.API.Helpers
{
    public class Tokens
    {
        public static async Task<string> GenerateJwt(ClaimsIdentity identity, IJwtFactory jwtFactory, string userName, JwtIssuerOptions jwtOptions, JsonSerializerSettings serializerSettings)
        {
            //Add lines here to the token
            var response = new JwtAuthResponse
            {
                Id = identity.Claims.Single(c => c.Type == "id").Value,
                Auth_Token = await jwtFactory.GenerateEncodedToken(userName, identity),
                Expires_In = (int)jwtOptions.ValidFor.TotalSeconds,
                Code = "login_success",
                StatusCode = 200,
                Description = "user was authenticated",
            };

            return JsonConvert.SerializeObject(response, serializerSettings);
        }
    }
}
