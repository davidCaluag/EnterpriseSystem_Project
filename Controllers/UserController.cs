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
        private UserDatabase _database = new();

        [HttpGet("alluser")]
        public async Task<IActionResult> GetAllUsers(){
            
            //list all users
            var userList = await _database.Users.ToListAsync();
            //return no content if empty
            if(userList.Count() <= 0)
                return NoContent();
            //ensure no password is given thru to have secure
            foreach(var user in userList){
                user.UserPasssword = "**********";
            }
            //return
            return Ok(userList);    
        }
        [HttpGet("alluserpluspassword")]
        public async Task<IActionResult> GetAllUsersPlusPassword(){

            //return a list of users
            var userList = await _database.Users.ToListAsync();
            //check for empty
            if(userList.Count() <= 0)
                return NoContent();
            
            // foreach(var user in userList){
            //     user.UserPasssword = "**********";
            // }
            //return
            return Ok(userList);    
        }

        [HttpPut("username/{userName}/password/{password}")]
        public async Task<IActionResult> AddUser(string userName, string password){
            
            //check for user and password input
            if(userName == default || password == default){
                throw new ArgumentNullException("Default parameters for user...");
            }

            var findUser = _database.Users.Where(x=>x.Username == userName);

            if(findUser.Count() > 0){
                throw new ArgumentOutOfRangeException("Username already in the _database...");
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
        

            await _database.Users.AddAsync(person);
            await _database.SaveChangesAsync();

            return Created("Created", person);
        }

        [HttpGet("{username}")]

        public async Task<IActionResult> FindUser(string userName){

            //find user
            var person = await _database.Users.FirstOrDefaultAsync(x=>x.Username == userName);
            //no user
            if(person == default)
                return BadRequest(person);
            //return A DTO for acylic return
            var DTO = new{
                Id = person.Id,
                Name = person.Username,
                PlayListCount = person.ListOfPlaylists.Count
            };
            //return :D
            return Ok(DTO);
        }

        [HttpGet("access/{username}/{password}")]
        public async Task<IActionResult> AccessUser(string username, string password)
        {
            //check for user and password
            var person = ValidateUserAndPassword(username, password);
            //if not right
            if(person == default)
                return BadRequest("Input is wrong");
            //give access
            return Ok("Granted");

        }
        
        [HttpDelete("deleteuser/{userName}/{password}/")]
        public async Task<IActionResult> DeleteUser(string userName, string password){
            
            //check for user and password
            var person = ValidateUserAndPassword(userName, password);
            //if wrong
            if(person == default)
                return BadRequest("Input is wrong");
            //remove
            _database.Users.Remove(person);
            //save
            await _database.SaveChangesAsync();
            //return
            return Ok($"Deleted {userName}");
            
        }


        [HttpGet("random")]

        public async Task<IActionResult> GetRandomUser(){
            

            //Get a random user
            Random newRandom = new();
            int randomNumber = newRandom.Next(0, _database.Users.Count()-1);
            //get random user with skipping thru certain numbers of user and returning the random user
            var person = _database.Users.Skip(randomNumber).Take(1);
            //return
            return Ok(person);
        }

        public User? ValidateUserAndPassword(string username, string password){

            //First find the person
            var person = _database.Users.FirstOrDefaultAsync(x=>x.Username == username).Result;
            //person is not found == error
            if(person == default)
                return null;
            //if the entered password is not user's password, return error
             if(!person.UserPasssword.Equals(password))
                return null;
            //both user and password connected
            return person;
        }
    }

    
}