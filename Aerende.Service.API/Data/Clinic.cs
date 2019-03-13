using System;
using System.Collections.Generic;

namespace Aerende.Service.API.Data
{
    public class Clinic
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public Adress Adress { get; set; }
        public IEnumerable<Doctor> Doctors { get; set; }

    }
}