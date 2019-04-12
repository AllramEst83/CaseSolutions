using System.Collections.Generic;
using Auth.API.Data.UserData.UserEntities.UserModel;

namespace Auth.API.Models
{
    public class UsersInRole
    {
        public UsersInRole()
        {

        }
        public UsersInRole(IList<User> _users)
        {
            User = _users;
        }

        public IList<User> User { get; set; }
        public bool IsNull
        {
            get
            {
                if (User == null)
                {
                    return true;
                }

                return false;
            }
        }
    }
}
