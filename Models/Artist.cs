using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Project_EnterpriseSystem.Models
{
    public class Artist
    {
        public Guid Id {get;} = new Guid();
        public required string Name {get; set; }
    }
}