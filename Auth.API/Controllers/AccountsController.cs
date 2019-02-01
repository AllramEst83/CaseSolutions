using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Auth.API.Helpers;
using Auth.API.ViewModels;
using AutoMapper;
using Database.Service.API.Data.UserData.UserEntities.UserContext;
using Database.Service.API.Data.UserData.UserEntities.UserModel;
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
        public ActionResult<object> Get()
        {
            return new { message = "Ping" };
        }

        // POST api/accounts
        public async Task<IActionResult> Signup([FromBody]RegistrationViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var userIdentity = _mapper.Map<User>(model);

            var result = await _userManager.CreateAsync(userIdentity, model.Password);

            if (!result.Succeeded) return new BadRequestObjectResult(Errors.AddErrorsToModelState(result, ModelState));

            //await _context.Users.AddAsync(new User
            //{
            //    Id = userIdentity.Id
                
            //});
            await _context.SaveChangesAsync();

            return new OkObjectResult("Account created");
        }


    }
}
