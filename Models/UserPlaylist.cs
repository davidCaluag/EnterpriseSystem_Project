using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Project_EnterpriseSystem.Models
{
    public class UserPlaylist
    {
        private List<Playlist> playlists;
        private string playlistTitle;
        private User user;

        public List<Playlist> Playlists {

            get=>playlists; //Default getter;
            
            set{

            if(value == null || value.Count() <= 0)
                throw new ArgumentNullException();
            
            playlists = value;
            } 
        }

        public void AddSong(Song newSong, string playlist){
            if(newSong == default || playlist == default)
                throw new ArgumentNullException();

            var selected = playlists.SingleOrDefault(x=>x.Title == playlist);
            if(selected == default)
                throw new ArgumentOutOfRangeException();
            
            
            if(selected.ListOfSongs.Contains(newSong))
                throw new Exception("Already in the playlist");

            selected.ListOfSongs.Add(newSong);
        }

        public string PlayListTitle {

            get => playlistTitle;

            set{

            if(value == null || value.Count() <= 0)
                throw new ArgumentNullException();
            
            playlistTitle = value;
            }  
        }

        public User PlayListUser {

            get => user;

            set{

            if(value == null || value == default)
                throw new ArgumentNullException();
            
            user = value;
            }  
        }

    }
}