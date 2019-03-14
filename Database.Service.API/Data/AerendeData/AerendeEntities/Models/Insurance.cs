using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Aerende.Service.API.Data
{
    public class Insurance
    {
        public Guid Id { get; set; }
        public InsuranceCompany InsuranceCompany { get; set; }
        public TypeOfInsurance TypeOfInsurance { get; set; }

    }
}
