using System;

namespace Database.Service.API.Data.FakturaData.FakturaEntities.Models
{
    public class Prescription
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }
}