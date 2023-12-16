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
        //set Id
        public Guid Id { get; set; } = Guid.NewGuid();

        public User? user {get; set;} = default;
        public string Title { get; set; } = "";
        public int SongCount {get; set; } = 0;
        public List<Song> ListOfSongs {get; set; } = new();
        public Genre PlaylistGenre {get; set; }
        
        public Playlist(){
            //set genre title
            PlaylistGenre = new(){GenreName = "Mixed"};
        }

        //update song count
        public void UpdateSongCount(){
            //dynamically update song count with the list of songs count, not just ++ to ensure data integrity.
            SongCount = ListOfSongs.Count; //if the list of songs is empty, then 0.

        }

        public void AddSong(Song newSong){
            //if song is empty, return false.
            if(newSong == default)
                throw new ArgumentNullException("Empty song");

            //if song already exists, dont add it.
            if(ListOfSongs.Contains(newSong))
                throw new Exception("Already in the playlist");
            //add then
            ListOfSongs.Add(newSong);
            //update the count of songs
            UpdateSongCount();
        }

        public bool DeleteSong(string songName){
            //check if songname is null/default
            if(songName == default)
                //throw new ArgumentNullException();
                return false;
            //is the song in the playlist?
            var IsParsed = ListOfSongs.FirstOrDefault(x=>x.Name == songName);
            //if not
            if(IsParsed == default){
                //throw new Exception("Not in the playlist");
                return false;
                
            }
            //if found
            ListOfSongs.Remove(IsParsed);
            //update song count dynamically
            UpdateSongCount();
            //return
            return true;
        }

        public string PlayListTitle {
            //get title
            get => Title;
            //set title
            set{
            //if value is empty
            if(value == null || value.Count() <= 0)
                throw new ArgumentNullException();
            //title is value now.
            Title = value;
            }  
        }
    }
}