using System;
using System.Collections.Generic;
using System.IO.Pipelines;
using System.Linq;
using System.Threading.Tasks;
using EnterpriseSystem_Project.Models;
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
        
        private UserDatabase _database = new ();

        
        /*
        public Guid Id { get; set; } = Guid.NewGuid();

        public User? user {get; set;} = default;
        public string Title { get; set; } = "";
        public int SongCount {get; set; } = 0;
        public List<Song> ListOfSongs {get; set; } = new();
        public Genre PlaylistGenre {get; set; }
        */

        //GET SPECIFIC USER PLAYLIST
        [HttpGet("getplaylist/{userName}")]
        public async Task<IActionResult> GetAllPlaylist(string userName){

            //finds user
            User _selectedUser = await UserSet(userName);

            //if user doesnt exist, return bad request.
            if(_selectedUser == default)
                return BadRequest("Set user first before making requests!");
            
            List<Playlist> returnList = new();

            //list is the playlist list
            var list = _selectedUser.ListOfPlaylists.ToList().AsReadOnly();
            foreach(var i in list){
                i.user = default;
            }
            //return list
            return Ok(list);
        }

        [HttpGet("getplaylistbyplaylistname/{playlistName}/{userName}")]

        public async Task<IActionResult> GetSpecificPlaylistInfo(string playlistName, string userName){
            
            User _selectedUser = await UserSet(userName);

            //checking for null/default
            if(_selectedUser == default)
                return BadRequest("Set user first before making requests!");
            //find specific playlist
            var _playlist = _selectedUser.ListOfPlaylists.Where(x=>x.Title == playlistName).SingleOrDefault();

            if(_playlist == default)
                return BadRequest("Playlist name not found");
            //return playlist
            return Ok(_playlist);
        }

        [HttpPut("newplaylist/{playlistName}/{userName}/{genre}")]
        public async Task<IActionResult> AddPlaylist(string playlistName, string userName, string genre){
        
            //User _selectedUser = await UserSet(userName);
            //select user with playlist included
            var _selectedUser = await _database.Users
                                .Where(x=>x.Username==userName)
                                .Include(x=>x.ListOfPlaylists)
                                .FirstOrDefaultAsync();

            if(_selectedUser == default || _selectedUser == null)
                throw new ArgumentOutOfRangeException("User not set...");

            if(_selectedUser.ListOfPlaylists.Any(x=>x.PlayListTitle == playlistName) != default)
                return BadRequest("Already there");

            //int beforeAddingCount = _selectedUser.ListOfPlaylists.Count;
            
            //This doesn't work
            string _genre = genre==default?"Mixed":genre;

            //Make new playlist
            Playlist newPlaylist = new(){
                PlayListTitle = playlistName,
                user = _selectedUser
            };

            //This doesnt work
            newPlaylist.PlaylistGenre.GenreName = _genre;


            //THIS ISNT WORKING. NOT SURE WHY AND IM GETTING ABSOLUTELY LIVID LMAO

            // var _playlist = await __database.Users.Where(x=>x == _selectedUser).Include(x=>x.ListOfPlaylists).FirstAsync();

             _selectedUser.AddPlaylist(newPlaylist);
             var playlist = _database.Playlists.AddAsync(newPlaylist);
            //_database.Users.Update(_selectedUser);
            //_database.Playlists.Update(newPlaylist);


            //THIS DOESNT TRIGGER SO THE SELECTED USER HAS A NEW PLAYLIST. WHY WONT IT ADD?

            //await _database.SaveChangesAsync();
            

            //it works now!
            await _database.SaveChangesAsync();
            //_database.Users.Update(_selectedUser);

            return Ok($"{newPlaylist.PlayListTitle} is added in {_selectedUser.Username}'s list of playlists...");
        }

        [HttpPut("deleteplaylist/{playlistName}/{username}")]
        public async Task<IActionResult> DeletePlaylist(string playlistName, string username){

            //Cant find user
            var _selectedUser = await UserSet(username);
            

            if(_selectedUser == default || _selectedUser == null)
                throw new ArgumentNullException("User is null...");
            

            //new playlist
            var _playlist = await PlaylistSet(playlistName,_selectedUser);

            //Cant find playlist
            if(_playlist == default || _selectedUser == null){
                throw new ArgumentNullException("Playlist not found...");
            }
            
        
            //Delete the playlist
            _selectedUser.ListOfPlaylists.Remove(_playlist);
            _database.Playlists.Remove(_playlist);
            _database.Users.Update(_selectedUser);
            await _database.SaveChangesAsync();

            return Ok($"Deleted {playlistName}");
        }

        

        //Get playlist
        public async Task<Playlist> PlaylistSet(string playlist, User user){
         var setPlaylist = user.ListOfPlaylists.Where(x=>x.PlayListTitle == playlist).FirstOrDefault();
         return setPlaylist;
        }

        //Get user
        public async Task<User> UserSet(string username){
        var user = _database.Users.Where(x=>x.Username==username).Include(x=>x.ListOfPlaylists).FirstOrDefault();
            return user; //its ok if its default
        }

    }

    
}       /*
        Okay, so my idea is: I find the user first before I make HTTP Requests.
        I initialize _selectedUser as null so if the user isn't found // 
        Requests are made before setting selected user, throw an exception.

        When the user is found, it'll be easier to return the user's playlist this way.

        The only issue is that the whole controller isn't using async properly.

        I get warnings when I leave it as it is. 

        Ask the professor whether this is fine or not.

        2023-12-13 2:25 PM UPDATE: ENDED UP NOT WORKING; I HAD TO CHANGE THE LOGIC.
        */
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
            
        //     await _database.SaveChangesAsync();
        //     return Ok($"{newSong} added in {_playlist}...");
        // }

        // [HttpDelete("deletesong/{songName}")]
        // public async Task<IActionResult> DeleteSong(string songName){
            
        //     if(BothSet())
        //     if(_playlist.DeleteSong(songName)){
        //         await _database.SaveChangesAsync();
        //         return Ok("Deleted...");
        //     }

        //     throw new Exception("Cant find song...");
        // }
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
        //     var result = await _database.Users.Where(x=>x == _selectedUser).Include(x=>x.ListOfPlaylists).ToListAsync();
        //     return Ok(result);
        // }
        // [HttpGet("setuser/{userName}")]
            // public async Task<IActionResult> SetUser(string userName){
            //     var user = await _database.Users.FirstOrDefaultAsync(x=>x.Username == userName);//.ToListAsync();
                
            //     if(user == default)
            //         throw new ArgumentOutOfRangeException("User does not exist...");

            //     _selectedUser = user;

            //     return Ok("Found user");
            // }
            // [HttpPut("selectPlaylist/{playlistName}/{username}")]
        // public async Task<IActionResult> SetPlaylist(string playlistName, string username){
            
        //     var _selectedUser = UserSet(username);
            
        //     if(_selectedUser == default || _selectedUser == null)
        //         throw new ArgumentOutOfRangeException("User is not selected...");


        //     var i = _selectedUser.ListOfPlaylists.FirstOrDefault(x=>x.PlayListTitle == playlistName);
           
        //     if(i == default)
        //         throw new Exception("Playlist not found...");

        //     var _playlist = i;


        //     return Ok("Found playlist...");
        // }