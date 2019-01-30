using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Auth.API.Models
{
    public class AppSettnigs
    {
        public string UserConnection { get; set; }
        public string Secret { get; set; }
        public string Host { get; set; }
    }
}
