﻿using System;

namespace Aerende.Service.API.Data
{
    public class Illness
    {
        public Guid Id { get; set; }
        public string IllnessTitle { get; set; }
        public IllnessSeverity IllnessSeverity { get; set; }
    }
}