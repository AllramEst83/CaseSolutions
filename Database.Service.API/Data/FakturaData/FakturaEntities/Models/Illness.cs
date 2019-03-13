using System;

namespace Database.Service.API.Data.FakturaData.FakturaEntities.Models
{
    public class Illness
    {
        public Guid Id { get; set; }
        public string IllnessTitle { get; set; }
        public IllnessSeverity IllnessSeverity { get; set; }
    }
}