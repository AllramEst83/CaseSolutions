using Microsoft.AspNetCore.Identity;

namespace Auth.API.Data.UserData.UserEntities.UserModel
{
    public class User : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}
