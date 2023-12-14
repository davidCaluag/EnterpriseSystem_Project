using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EnterpriseSystem_Project.Models;

namespace Project_EnterpriseSystem.Models
{
    public class Song : IModelInterface
    {
        public Guid Id {get; set; } = Guid.NewGuid();

        public string Name {get; set; } = "";

        public Artist ArtistObject {get; set; } = new();
        public Genre Genre { get; set; } = new(){
            GenreName = ""
        };
    }
}