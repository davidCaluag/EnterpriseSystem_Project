using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Project_EnterpriseSystem.Models
{
    public class Artist
    {
        [Key]
        public Guid Id {get; set; } = new Guid();
        public string Name {get; set; } = "";
    }
}