using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Project_EnterpriseSystem.Models
{
    public class UserPlaylist
    {
        private List<Song> songs;
        private string playlistTitle;
        private User user;

        public List<Song> Playlist {

            get=>songs; //Default getter;
            
            set{

            if(value == null || value.Count() <= 0)
                throw new ArgumentNullException();
            
            songs = value;
            } 
        }

        public void AddSong(Song newSong){
            if(newSong == null)
                throw new ArgumentNullException();

            if(songs.Contains(newSong))
                throw new Exception("Already in the playlist");

            songs.Add(newSong);
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