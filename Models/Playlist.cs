using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Project_EnterpriseSystem.Models
{
    /*
    David

    Created a playlist class that is one-to-many with songs. It also other important
    properties like the title of the playlist and Id. It also Song Count just 
    to describe the playlist without entering it.
    */
    public class Playlist
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Title { get; set; } = "";
        public int SongCount {get; set; }
        public List<Song> ListOfSongs {get; set; }

        public Playlist(){
            SongCount = 0; //initializing a playlist means it'll be 0 unless specified.
            ListOfSongs = new(); //initialize list early on so it's not null.
        }

        public void UpdateSongCount(){
            SongCount = ListOfSongs.Count; //if the list of songs is empty, then 0.

        }

        public void AddSong(Song newSong){

            if(newSong == default)
                throw new ArgumentNullException("Empty song");

            
            if(ListOfSongs.Contains(newSong))
                throw new Exception("Already in the playlist");

            ListOfSongs.Add(newSong);
            UpdateSongCount();
        }

        public void DeleteSong(Song newSong){
            if(newSong == default)
                throw new ArgumentNullException();

            
            if(!ListOfSongs.Contains(newSong))
                throw new Exception("Not in the playlist");

            ListOfSongs.Remove(newSong);
            UpdateSongCount();
        }

        public string PlayListTitle {

            get => Title;

            set{

            if(value == null || value.Count() <= 0)
                throw new ArgumentNullException();
            
            Title = value;
            }  
        }
    }
}