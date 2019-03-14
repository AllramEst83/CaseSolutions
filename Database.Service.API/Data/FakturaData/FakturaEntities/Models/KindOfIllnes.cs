using System;
using System.Collections.Generic;

namespace Database.Service.API.Data.FakturaData.FakturaEntities.Models
{
    public class KindOfIllness
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public IllnessSev IllnessSeverity { get; set; }
    }
}