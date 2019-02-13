
namespace Auth.API.ViewModels
{
    public class JwtAuthResponse
    {
        public string Id { get; set; }
        public string Auth_Token { get; set; }
        public int Expires_In { get; set; }
        public int StatusCode { get; set; }
        public string Code { get; set; }
        public string Description { get; set; }
    }
}
