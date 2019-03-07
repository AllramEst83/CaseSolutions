using APIResponseMessageWrapper.Model;
using Database.Service.API.Data.UserData.UserEntities.UserModel;
using ResponseModels.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Auth.API.ViewModels
{
    public class APIReturnListViewModel : APIMessageViewModel
    {
        public List<UsersResponse> ListOfUsers { get; set; }
    }
}
