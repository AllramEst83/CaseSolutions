using Database.Service.API.Data.AerendeData.AerendeEntities.Models;
using System;

namespace Aerende.Service.API.Data
{
    public class Doctor
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public TypeOfDoctorWrapper TypeOfDoctorWrapper { get; set; }

    }
}