using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Project_EnterpriseSystem.Models
{
    public class Song
    {
        public Guid Id {get; set; }

        public required string Name {get; set; }

        public required Artist Artist {get; set; }
    }
}