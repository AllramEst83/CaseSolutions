using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Database.Service.API.Data.UserData.UserEntities.UserModel;
using APIResponseMessageWrapper.Model;

namespace Auth.API.ExceptionHandeling
{
    //public class TestException : CustomExceptionHandeling
    //{
    //    private readonly UserManager<User> _userManager;

    //    public TestException(UserManager<User> userManager)
    //    {
    //        _userManager = userManager;
    //    }
        
    //    public T Test<T>()
    //    {
    //        var test = TryCatch(() =>
    //        {

    //            var users = _userManager.Users.Select(x => new UsersViewModel
    //            {
    //                UserName = x.UserName,
    //                Id = x.Id

    //            })
    //                .ToList();


    //            return users;
    //        });
    //        return test;
    //    }
    //}
}
