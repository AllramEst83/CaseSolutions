using ResponseModels.ViewModels;
using System.Collections.Generic;

namespace Auth.API.ViewModels
{
    public class APIReturnListViewModel : APIMessageViewModel
    {
        public List<UsersResponse> ListOfUsers { get; set; }
    }
}
