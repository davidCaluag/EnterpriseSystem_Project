using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Project_EnterpriseSystem.Models;

namespace EnterpriseSystem_Project.Models
{
    public class Genre : IModelInterface
    {
        public Guid Id {get; set; } = Guid.NewGuid();
        public required string GenreName { get; set; }
    }
}