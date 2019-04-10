using APIErrorHandling;
using APIResponseMessageWrapper;
using Auth.API.AuthFactory;
using Auth.API.Data.UserData.UserEntities.UserModel;
using Auth.API.Helpers;
using Auth.API.Models;
using Auth.API.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using ResponseModels.ViewModels;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Auth.API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly UserManager<User> _userManager;
        private readonly IJwtFactory _jwtFactory;
        private readonly JwtIssuerOptions _jwtOptions;

        public AuthController(UserManager<User> userManager, IJwtFactory jwtFactory, IOptions<JwtIssuerOptions> jwtOptions)
        {
            _userManager = userManager;
            _jwtFactory = jwtFactory;
            _jwtOptions = jwtOptions.Value;
        }

        // GET api/auth/get
        [HttpGet]
        public ActionResult<object> Ping()
        {
            return new OkObjectResult(Wrappyfier.WrapResponse(200, Constants.APIMessages.Ping));
        }
               

        //DONE
        [HttpPost]
        // POST api/auth/login
        public async Task<IActionResult> Login([FromBody]CredentialsViewModel credentials)
        {
            if (!ModelState.IsValid)
            {
                return new JsonResult(await Errors
                    .GetGenericErrorResponse(new JwtResponse()
                    {
                        Id = "no_id",
                        Auth_Token = "no_token",
                        Expires_In = 0,
                        StatusCode = 400,
                        Error = "ModelState invalid",
                        Description = "ModelState is not valid",
                        Code = "modelState_invalid",


                    }));
            }

            var identity = await GetClaimsIdentity(credentials.UserName, credentials.Password);
            if (identity == null)
            {
                return new JsonResult(await Errors
                    .GetGenericErrorResponse(new JwtResponse()
                    {
                        Id = "no_token",
                        Auth_Token = "no_token",
                        Expires_In = 0,
                        StatusCode = 400,
                        Error = "Login failure",
                        Description = "Invalid username, password or role.",
                        Code = "login_failure"
                    }));

            }

            var jwtResponse = await Tokens.GenerateJwt(identity, _jwtFactory, credentials.UserName, _jwtOptions, new JsonSerializerSettings { Formatting = Formatting.Indented });

            return new OkObjectResult(jwtResponse);
        }

        //Help methods
        private async Task<ClaimsIdentity> GetClaimsIdentity(string userName, string password)
        {
            if (string.IsNullOrEmpty(userName) || string.IsNullOrEmpty(password))
                return await Task.FromResult<ClaimsIdentity>(null);

            // get the user to verifty
            var userToVerify = await _userManager.FindByNameAsync(userName);

            if (userToVerify == null) return await Task.FromResult<ClaimsIdentity>(null);

            IList<string> userRoles = await _userManager.GetRolesAsync(userToVerify);

            if (!userRoles.Any()) return await Task.FromResult<ClaimsIdentity>(null);

            // check the credentials
            if (await _userManager.CheckPasswordAsync(userToVerify, password))
            {
                return await Task.FromResult(_jwtFactory.GenerateClaimsIdentity(userName, userToVerify.Id, userRoles.ToList()));
            }

            // Credentials are invalid, or account doesn't exist
            return await Task.FromResult<ClaimsIdentity>(null);
        }
        //Help methods
    }
}
