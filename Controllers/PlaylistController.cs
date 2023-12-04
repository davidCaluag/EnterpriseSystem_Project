using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Project_EnterpriseSystem.Models;
using Project_EnterpriseSystem.Services;

namespace Project_EnterpriseSystem.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PlaylistController : ControllerBase
    {
        
        private UserDatabase database = new ();
        private static User _selectedUser = default;
        private static Playlist _playlist = default;


        /*
        Okay, so my idea is: I find the user first before I make HTTP Requests.
        I initialize _selectedUser as null so if the user isn't found // 
        Requests are made before setting selected user, throw an exception.

        When the user is found, it'll be easier to return the user's playlist this way.

        The only issue is that the whole controller isn't using async properly.

        I get warnings when I leave it as it is. 

        Ask the professor whether this is fine or not.
        */

        [HttpGet("/setuser/{userId}")]
        public async Task<IActionResult> SetUser(Guid userId){
            var user = await database.Users.FindAsync(userId);
            
            if(user == default)
                throw new ArgumentOutOfRangeException("User does not exist...");

            _selectedUser = user;
            return Ok("Found user");
        }

        [HttpGet("/selectPlaylist/{playlistName}")]
        public async Task<IActionResult> SetPlaylist(string playlistName){
            var user = await database.Users.FindAsync(_selectedUser);
            
            if(user == default)
                throw new ArgumentOutOfRangeException("User is not selected...");

            var i = user.ListOfPlaylists.Find(x=>x.PlayListTitle == playlistName);
           
            if(i == default)
                throw new Exception("Something happened idk tho");

            _playlist = i;
            if(_playlist == default)
                throw new ArgumentOutOfRangeException("Playlist not found...");

            return Ok("Found playlist...");
        }

        [HttpGet("/getplaylist")]
        public async Task<IActionResult> GetAllPlaylist(){
            //var allusers = new UserController(); 

            if(_selectedUser == default)
                throw new ArgumentOutOfRangeException("User does not exist...");

            return Ok(_selectedUser.ListOfPlaylists);
        }

        [HttpDelete("/{playlistName}")]
        public async Task<IActionResult> DeletePlaylist(string playlistName){

            //Cant find user
            if(_selectedUser == default)
                throw new ArgumentNullException("User is null...");

            //Cant find playlist
            if(_selectedUser.ListOfPlaylists.Find(x=>x.Title ==playlistName) == default){
                throw new ArgumentNullException("Playlist not found...");
            }
            //Not sure if this works
            var playlist = _selectedUser.ListOfPlaylists.First(x=>x.Title == playlistName);
            //Delete the playlist
            _selectedUser.ListOfPlaylists.Remove(playlist);

            await database.SaveChangesAsync();
            return Ok($"Deleted {playlistName}");
        }
        

        [HttpPut("/playlistName/{playlistName}")]

        public async Task<IActionResult> AddSong(Song newSong){

            if(newSong == default || _selectedUser == default || _playlist == default)
                throw new ArgumentNullException("Bro...");
            
            //This can't be true...
            if(_selectedUser.ListOfPlaylists == null)
                _selectedUser.ListOfPlaylists = new();
            
            _playlist.AddSong(newSong);
            
            await database.SaveChangesAsync();
            return Ok($"{newSong} added in {_playlist}...");
        }

        [HttpDelete("/{songName}")]
        public async Task<IActionResult> DeleteSong(Song song){

            if(_playlist.DeleteSong(song)){
                await database.SaveChangesAsync();
                return Ok("Deleted...");
            }

            throw new Exception("Cant find song...");
        }

        
    }
}