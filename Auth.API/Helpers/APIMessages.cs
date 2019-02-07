using Auth.API.ViewModels;
using Database.Service.API.Data.UserData.UserEntities.UserModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Auth.API.Helpers
{
    public static class APIMessages
    {
        public static APIMessageViewModel WrapAPIMessage(int statusCode, string message)
        {

            APIMessageViewModel errorModel = new APIMessageViewModel()
            {
                StatusCode = statusCode,
                Message = message
            };

            return errorModel;
        }

        public static APIReturnListViewModel WrapAPIList(int statusCode, string message, List<UsersViewModel> listOfUsers)
        {
            APIReturnListViewModel listViewModel = new APIReturnListViewModel()
            {
                StatusCode = statusCode,
                Message = message,
                ListOfUsers = listOfUsers
            };

            return listViewModel;
    }
    }
}
