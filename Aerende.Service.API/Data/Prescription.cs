using System;

namespace Aerende.Service.API.Data
{
    public class Prescription
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }
}