using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Project_EnterpriseSystem.Models
{
    /* 
    David

    Created a user class. It has the necessary properties like Id and 
    list of playlists.
    
    */
    public class User
    {
        public Guid Id { get; } = new Guid();
        public List<Playlist> ListOfPlaylists {get; set; }
        public string userName;

        public string Username { get => userName; set{
            if(value == default)
                throw new ArgumentNullException();

            userName = value;
        } 
        }    
    }
}