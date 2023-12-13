using System;
using System.Collections.Generic;
using System.IO.Pipelines;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Project_EnterpriseSystem.Models;
using Project_EnterpriseSystem.Services;

namespace Project_EnterpriseSystem.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PlaylistController : ControllerBase
    {
        
        private UserDatabase database = new ();

        /*
        Okay, so my idea is: I find the user first before I make HTTP Requests.
        I initialize _selectedUser as null so if the user isn't found // 
        Requests are made before setting selected user, throw an exception.

        When the user is found, it'll be easier to return the user's playlist this way.

        The only issue is that the whole controller isn't using async properly.

        I get warnings when I leave it as it is. 

        Ask the professor whether this is fine or not.
        */

        [HttpGet]
        public async Task<IActionResult> GetAllPlaylist(string username){

            var _selectedUser = UserSet(username);

            if(_selectedUser == default)
                return BadRequest("Set user first before making requests!");
           
            if(_selectedUser.ListOfPlaylists.Count <= 0)
                return NoContent();

            return Ok(_selectedUser.ListOfPlaylists);
            
            
        }

        [HttpPut("newPlaylist/{playlistName}/{username}")]
        public async Task<IActionResult> SetNewPlaylist(string playlistName, string username){
        
            var _selectedUser = UserSet(username);
            if(_selectedUser == default)
                throw new ArgumentOutOfRangeException("User not set...");

            Playlist newPlaylist = new(){
                Title = playlistName
            }; 
            _selectedUser.ListOfPlaylists.Add(newPlaylist);

            await database.SaveChangesAsync();
            return Created($"{newPlaylist.Title} is added in {_selectedUser.Username}'s list of playlists...",newPlaylist);
        }

        // [HttpGet("setuser/{userName}")]
        // public async Task<IActionResult> SetUser(string userName){
        //     var user = await database.Users.FirstOrDefaultAsync(x=>x.Username == userName);//.ToListAsync();
            
        //     if(user == default)
        //         throw new ArgumentOutOfRangeException("User does not exist...");

        //     _selectedUser = user;

        //     return Ok("Found user");
        // }

        [HttpPut("selectPlaylist/{playlistName}/{username}")]
        public async Task<IActionResult> SetPlaylist(string playlistName, string username){
            
            var _selectedUser = UserSet(username);
            var _playlist = PlaylistSet(playlistName,_selectedUser);
            if(_selectedUser == default)
                throw new ArgumentOutOfRangeException("User is not selected...");

            var i = _selectedUser.ListOfPlaylists.FirstOrDefault(x=>x.PlayListTitle == playlistName);
           
            if(i == default)
                throw new Exception("Something happened idk tho");

            _playlist = i;
            if(_playlist == default)
                throw new ArgumentOutOfRangeException("Playlist not found...");

            return Ok("Found playlist...");
        }

        [HttpDelete("playlist/{playlistName}/{username}")]
        public async Task<IActionResult> DeletePlaylist(string playlistName, string username){

            //Cant find user
            User _selectedUser = UserSet(username);
            Playlist _playlist = PlaylistSet(playlistName,_selectedUser);

            if(_selectedUser == default)
                throw new ArgumentNullException("User is null...");

            //Cant find playlist
            if(_playlist == default){
                throw new ArgumentNullException("Playlist not found...");
            }
            
        
            //Delete the playlist
            _selectedUser.ListOfPlaylists.Remove(_playlist);
            await database.SaveChangesAsync();

            return Ok($"Deleted {playlistName}");
        }
        
        
        //Mattie's Responsibility
        // [HttpPut("addsong")]

        // public async Task<IActionResult> AddSong(Song newSong){

        //     _selectedUser = UserSet()
        //     if(newSong.Name == default)
        //         throw new ArgumentNullException("Song name required.");

        //     // if(BothSet())
        //     //     throw new ArgumentNullException("Set the user/playlist first.");
            
        //     //This can't be true...
        //     if(_selectedUser.ListOfPlaylists == null)
        //         _selectedUser.ListOfPlaylists = new();
            
        //     _playlist.AddSong(newSong);
            
        //     await database.SaveChangesAsync();
        //     return Ok($"{newSong} added in {_playlist}...");
        // }

        // [HttpDelete("deletesong/{songName}")]
        // public async Task<IActionResult> DeleteSong(string songName){
            
        //     if(BothSet())
        //     if(_playlist.DeleteSong(songName)){
        //         await database.SaveChangesAsync();
        //         return Ok("Deleted...");
        //     }

        //     throw new Exception("Cant find song...");
        // }

        
        public Playlist PlaylistSet(string playlist, User user){
         var setPlaylist = user.ListOfPlaylists.FirstOrDefault(x=>x.Title == playlist);
         return setPlaylist;
        }
        public User UserSet(string username){
        var user = database.Users.FirstOrDefault(x=>x.Username == username);
            return user; //its ok if its default
        }

    }

    
}
        // public bool BothSet(){
        // if(_playlist == default)
        //     return false;
        // if(_selectedUser == default)
        //     return false;
        // return true;
        // }
              // [HttpGet("getplaylist")]
        // public async Task<IActionResult> GetAllPlaylist(){
        //     //var allusers = new UserController(); 

        //     if(_selectedUser == default)
        //         return BadRequest("User is empty.");
        //     var result = await database.Users.Where(x=>x == _selectedUser).Include(x=>x.ListOfPlaylists).ToListAsync();
        //     return Ok(result);
        // }
