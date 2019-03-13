using System.ComponentModel.DataAnnotations;

namespace Aerende.Service.API.Data
{
    public class Adress
    {
        public int Id { get; set; }
        public string StreetAdress { get; set; }
        public string ZipCode { get; set; }
        public string Telephone { get; set; }
    }
}