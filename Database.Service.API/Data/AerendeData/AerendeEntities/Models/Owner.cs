using System;

namespace Aerende.Service.API.Data
{
    public class Owner
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Telephone { get; set; }
        public Adress Adress { get; set; }
        public int SSN { get; set; }
    }
}