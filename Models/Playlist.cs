using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Project_EnterpriseSystem.Models
{
    public class Playlist
    {
        public Guid Id { get; set; }
        public required string Title { get; set; }
        public int SongCount {get; set; }
        public List<Song> ListOfSongs {get; set; }
        
        public Playlist(){
            SongCount = 0; //initializing a playlist means it'll be 0 unless specified.
            ListOfSongs = new(); //initialize list early on so it's not null.
        }
    }
}