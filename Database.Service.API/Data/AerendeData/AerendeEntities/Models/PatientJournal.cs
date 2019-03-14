using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Aerende.Service.API.Data
{
    public class PatientJournal
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public Guid AnimalSSN { get; set; }
        public Insurance Insurance { get; set; }
        public Clinic Clinic { get; set; }
        public Guid InvoiceId { get; set; }
        public IEnumerable<MedicalService> MedicalServices { get; set; }
        public IEnumerable<Owner> Owners { get; set; }
    }
}
