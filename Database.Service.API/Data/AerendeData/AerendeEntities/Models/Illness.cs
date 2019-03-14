using Database.Service.API.Data.AerendeData.AerendeEntities.Models;
using System;

namespace Aerende.Service.API.Data
{
    public class Illness
    {
        public Guid Id { get; set; }
        public string IllnessTitle { get; set; }
        public IllnessSeverityWrapper IllnessSeverityWrapper { get; set; }

    }
}