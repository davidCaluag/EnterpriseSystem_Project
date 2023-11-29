using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Project_EnterpriseSystem.Models
{
    public class Album
    {
        public Guid Id {get; } = new Guid();
        public required string Name {get; set; }
        public List<Song>? Songs { get; set; }

    }
}