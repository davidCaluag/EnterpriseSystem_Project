using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using EnterpriseSystem_Project.Models;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Project_EnterpriseSystem.Models
{
    /*
    David

    Created a playlist class that is one-to-many with songs. It also other important
    properties like the title of the playlist and Id. It also Song Count just 
    to describe the playlist without entering it.
    */
    public class Playlist : IModelInterface
    {
        public Guid Id { get; set; } = Guid.NewGuid();

        public User user {get; set;}
        private string Title { get; set; } = "";
        public int SongCount {get; set; } = 0;
        public List<Song> ListOfSongs {get; set; } = new();
        public Genre PlaylistGenre {get; set; }
        
        public Playlist(){
            PlaylistGenre = new(){GenreName = "Mixed"};
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

        public bool DeleteSong(string songName){
            if(songName == default)
                //throw new ArgumentNullException();
                return false;
            
            var IsParsed = ListOfSongs.FirstOrDefault(x=>x.Name == songName);
            
            if(IsParsed == default){
                //throw new Exception("Not in the playlist");
                return false;
                
            }

            ListOfSongs.Remove(IsParsed);
            UpdateSongCount();

            return true;
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