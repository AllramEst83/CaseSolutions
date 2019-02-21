using APIErrorHandling;
using APIErrorHandling.Models;
using APIResponseMessageWrapper;
using APIResponseMessageWrapper.Model;
using Auth.API.AuthFactory;
using Auth.API.Helpers;
using Auth.API.Models;
using Auth.API.ViewModels;
using CaseSolutionsTokenValidationParameters.Models;
using Database.Service.API.Data.UserData.UserEntities.UserModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System;
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


        [Authorize(Policy = TokenValidationConstants.Policies.AuthAPIAdmin)]
        [HttpGet]
        public IActionResult GetListOfUsers()
        {
            var users = _userManager.Users.Select(x => new UsersViewModel
            {
                UserName = x.UserName,

            })
            .ToList();

            return new OkObjectResult(Wrappyfier.WrapAPIList(200, Constants.APIMessages.ListOfUsers, users));
        }

        // POST api/auth/login
        [HttpPost]
        public async Task<IActionResult> Login([FromBody]CredentialsViewModel credentials)
        {
            if (!ModelState.IsValid)
            {
                return new JsonResult(Errors
                    .ErrorResponse(new JwtErrorHandlingModel()
                    {
                        Auth_Token = "",
                        Code = "modelState_invalid",
                        Id = "",
                        Description = "ModelState is not valid",
                        Expires_In = 0,
                        StatusCode = 400
                    }));
            }

            var identity = await GetClaimsIdentity(credentials.UserName, credentials.Password, credentials.Role);
            if (identity == null)
            {
                return new JsonResult(Errors
                    .ErrorResponse(new JwtErrorHandlingModel()
                    {
                        Auth_Token = "",
                        Code = "login_failure",
                        Id = "",
                        Description = "Invalid username or password.",
                        Expires_In = 0,
                        StatusCode = 400
                    }));
                
            }

            var jwtResponse = await Tokens.GenerateJwt(identity, _jwtFactory, credentials.UserName, _jwtOptions, new JsonSerializerSettings { Formatting = Formatting.Indented });

            return new OkObjectResult(jwtResponse);
        }


        private async Task<ClaimsIdentity> GetClaimsIdentity(string userName, string password, string role)
        {
            if (string.IsNullOrEmpty(userName) || string.IsNullOrEmpty(password))
                return await Task.FromResult<ClaimsIdentity>(null);

            // get the user to verifty
            var userToVerify = await _userManager.FindByNameAsync(userName);

            if (userToVerify == null) return await Task.FromResult<ClaimsIdentity>(null);

            // check the credentials
            if (await _userManager.CheckPasswordAsync(userToVerify, password))
            {
                return await Task.FromResult(_jwtFactory.GenerateClaimsIdentity(userName, userToVerify.Id, role));
            }

            // Credentials are invalid, or account doesn't exist
            return await Task.FromResult<ClaimsIdentity>(null);
        }

    }
}
