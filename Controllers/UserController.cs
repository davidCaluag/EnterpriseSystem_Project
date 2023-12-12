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
    public class UserController : ControllerBase
    {
        private UserDatabase database = new();

        [HttpGet("allUser")]
        public async Task<IActionResult> GetAllUsers(){

            var userList = await database.Users.ToListAsync();
            if(userList.Count() <= 0)
                return NoContent();
                
            return Ok(userList);    
        }

        [HttpPut("username/{userName}/password/{password}")]
        public async Task<IActionResult> AddUser(string userName, string password){
            
            if(userName == default || password == default){
                throw new ArgumentNullException("Default parameters for user...");
            }

            var findUser = database.Users.Where(x=>x.Username == userName);

            if(findUser.Count() > 0){
                throw new ArgumentOutOfRangeException("Username already in the database...");
            }
            User person = new(){
                Username = userName,
                ListOfPlaylists = new(), //I already instructed the Model to instantiate this, but I just wanna make sure it does so there are no null.
                Id = Guid.NewGuid()
            };

            if(person.ValidatePassword(password))
                person.UserPasssword = password;
            else   
                throw new Exception("Password does not meet requirement...");
        

            await database.Users.AddAsync(person);
            await database.SaveChangesAsync();

            return Created("Created", person);
        }

        [HttpGet("{userName}")]

        public async Task<IActionResult> FindUser(string userName){

            var person = await database.Users.FirstOrDefaultAsync(x=>x.Username == userName);

            if(person == default)
                return BadRequest(person);
            
            var DTO = new{
                Id = person.Id,
                Name = person.Username,
                PlayListCount = person.ListOfPlaylists.Count
            };

            return Ok(DTO);
        }

        [HttpGet("access/{username}/{password}")]
        public async Task<IActionResult> AccessUser(string username, string password)
        {
            var person = ValidateUserAndPassword(username, password);

            if(person == default)
                return BadRequest("Input is wrong");

            return Ok("Granted");

        }
        [HttpDelete("{userName}/{password}")]
        public async Task<IActionResult> DeleteUser(string userName, string password){
            
            
            var person = ValidateUserAndPassword(userName, password);

            if(person == default)
                return BadRequest("Input is wrong");

            database.Users.Remove(person);

            await database.SaveChangesAsync();

            return Ok($"Deleted {userName}");
            
        }

        [HttpPut("newPlaylist/{playlistName}/user/{userName}")]
        public async Task<IActionResult> SetNewPlaylist(string username, string playlistName){
            
            var getUser = await database.Users.FindAsync(username);

            if(getUser == null || getUser == default)
                throw new ArgumentOutOfRangeException("User not found...");

            Playlist newPlaylist = new(){
                Title = playlistName
            }; 
            getUser.ListOfPlaylists.Add(newPlaylist);

            return Created($"{newPlaylist.Title} is added in {getUser.Username}'s list of playlists...",newPlaylist);
        }

        [HttpGet("random")]

        public async Task<IActionResult> GetRandomUser(){

            Random newRandom = new();
            newRandom.Next(0, database.Users.Count()-1);

            var person = await database.Users.Take(newRandom.Next(0, database.Users.Count()-1)).ToListAsync();

            return Ok(person);
        }

        public User ValidateUserAndPassword(string username, string password){

            var person = database.Users.FirstOrDefaultAsync(x=>x.Username == username).Result;

            if(person == default)
                return null;

             if(!person.UserPasssword.Equals(password))
                return null;

            return person;
        }
    }

    
}