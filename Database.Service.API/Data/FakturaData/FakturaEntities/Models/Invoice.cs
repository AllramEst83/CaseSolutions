using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Database.Service.API.Data.FakturaData.FakturaEntities.Models
{
    public class Invoice
    {
        public Guid Id { get; set; }
        public Guid PatientJournalId { get; set; }
        public double TotalSum { get; set; }
        public double Discount { get; set; }
        public DateTime IssueDate { get; set; }
        public DateTime DueDate { get; set; }
        public IEnumerable<MedicalService> MedicalServices { get; set; }
    }
}
