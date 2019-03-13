﻿using System;

namespace Database.Service.API.Data.FakturaData.FakturaEntities.Models
{
    public class Doctor
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public TypeOfDoctor TypeOfDoctor { get; set; }
    }
}