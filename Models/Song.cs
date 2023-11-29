using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Project_EnterpriseSystem.Models
{
    public class Song
    {
        public Guid Id {get; set; } = new Guid();

        public  string Name {get; set; }

        public Artist Artist {get; set; }
    }
}