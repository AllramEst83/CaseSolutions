using System;

namespace Aerende.Service.API.Data
{
    public class InsuranceCompany
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public Adress Adress { get; set; }
    }
}