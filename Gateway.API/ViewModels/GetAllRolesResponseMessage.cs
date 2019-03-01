using APIResponseMessageWrapper.Model;
using System.Collections.Generic;

namespace Gateway.API.ViewModels
{
    public class GetAllRolesResponseMessage
    {
        public GetAllRolesResponseMessage(int statusCode)
        {
            StatusCode = statusCode;
        }
        public int StatusCode { get; set; }
        public List<GetAllRoles> ListOfAllRoles { get; set; }        
        public string Error { get; set; }
        public string Description { get; set; }
        public string Code { get; set; }
    }
}
