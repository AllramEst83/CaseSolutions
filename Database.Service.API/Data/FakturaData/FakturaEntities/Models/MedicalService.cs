using System;
using System.Collections.Generic;

namespace Database.Service.API.Data.FakturaData.FakturaEntities.Models
{
    public class MedicalService
    {
        public Guid Id { get; set; }
        public TypeOfExamination TypeOfExamination { get; set; }
        public Doctor Doctor { get; set; }
        public double HourlyCost { get; set; }
        public TimeSpan ExaminationDuration { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public KindOfIllness KindOfIllnes { get; set; } 
        public List<Prescription> Prescriptions { get; set; }
    }
}