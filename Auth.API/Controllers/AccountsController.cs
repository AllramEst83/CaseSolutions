using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using APIErrorHandling;
using Auth.API.Helpers;
using Auth.API.ViewModels;
using AutoMapper;
using Database.Service.API.Data.UserData.UserEntities.UserContext;
using Database.Service.API.Data.UserData.UserEntities.UserModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Auth.API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class AccountsController : ControllerBase
    {
        private readonly UserContext _context;
        private readonly UserManager<User> _userManager;
        private readonly IMapper _mapper;

        public AccountsController(UserManager<User> userManager, IMapper mapper, UserContext appDbContext)
        {
            _userManager = userManager;
            _mapper = mapper;
            _context = appDbContext;
        }

        // GET api/accounts/get
        [HttpGet]
        public ActionResult<object> Ping()
        {
            return new OkObjectResult(APIResponses.WrapAPIMessage(200, Constants.Strings.APIMessages.Ping));
        }

        // POST api/accounts/signup
        public async Task<IActionResult> Signup([FromBody]RegistrationViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var userIdentity = _mapper.Map<User>(model);

            var result = await _userManager.CreateAsync(userIdentity, model.Password);

            if (!result.Succeeded) return new BadRequestObjectResult(Errors.AddErrorsToModelState(result, ModelState));

            await _context.SaveChangesAsync();

            return new OkObjectResult(APIResponses.WrapAPIMessage(200, Constants.Strings.APIMessages.SuccessMessage));
        }

        //https://aryalnishan.com.np/asp-net-mvc/delete-user-related-data-in-asp-net-mvc-identity/
        //[Authorize(Policy = "Auth.API.Admin")]
        [HttpDelete]
        //Delete  /api/auth/deleteuser
        public async Task<IActionResult> DeleteUser([FromBody] DeleteUserViewModel model)
        {

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var user = await _userManager.FindByIdAsync(model.Id);

            if (user == null)
            {
                return NotFound(APIResponses
                    .WrapAPIMessage(404, String
                    .Format(Constants.Strings
                    .APIMessages.NotFoundMessage, model.Id)));
            }

            //var rolesForUser = await _userManager.GetRolesAsync(user);

            //var removeLoginsResult = await _userManager.RemoveLoginAsync(user, );
            //if (!removeLoginsResult.Succeeded) return new BadRequestObjectResult(Errors.AddErrorsToModelState(removeLoginsResult, ModelState));

            //var removeRolesResult = await _userManager.RemoveFromRolesAsync(user, rolesForUser);
            //if (!removeLoginsResult.Succeeded) return new BadRequestObjectResult(Errors.AddErrorsToModelState(removeRolesResult, ModelState));

            //var removeUserResult = await _userManager.DeleteAsync(user);
            //if (!removeLoginsResult.Succeeded) return new BadRequestObjectResult(Errors.AddErrorsToModelState(removeUserResult, ModelState));

            //await _context.SaveChangesAsync();

            return new OkObjectResult(new { });
        }

    }
}
