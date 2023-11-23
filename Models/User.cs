using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Project_EnterpriseSystem.Models
{
    public class User
    {
        public Guid Id { get; set; }
        public List<Playlist>? PersonalPlaylist {get; set; }
    }
}