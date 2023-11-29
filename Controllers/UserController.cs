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

        [HttpGet]
        public async Task<IActionResult> GetAllUsers(){

            var userList = await database.Users.ToListAsync();
            return Ok(userList);    
        }

        [HttpPut("/userName/{userName}/password/{password}")]
        public async Task<IActionResult> AddUser(string userName, string password){
            
            if(userName == default || password == default){
                throw new ArgumentNullException("Default parameters for user...");
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

            if(person != default){
                throw new ArgumentOutOfRangeException("Username already in the database...");
            }

            await database.Users.AddAsync(person);
            await database.SaveChangesAsync();

            return Created("Created", person);
        }

        [HttpGet("/{userName}")]

        public async Task<IActionResult> FindUser(string userName){

            var person =  await database.Users.FindAsync(userName);

            if(person == default)
                throw new ArgumentOutOfRangeException("User not found...");
            
            var DTO = new{
                Id = person.Id,
                Name = person.Username,
                PlayListCount = person.ListOfPlaylists.Count
            };

            return Ok(DTO);
        }

        [HttpDelete("/{userName}")]
        public async Task<IActionResult> DeleteUser(string userName){
            var person =  await database.Users.FindAsync(userName);

            if(person == default)
                throw new ArgumentOutOfRangeException("User not found...");
            
            database.Users.Remove(person);

            return Ok($"Deleted {userName}");
            
        }

        [HttpGet("/random")]

        public async Task<IActionResult> GetRandomUser(){

            Random newRandom = new();
            newRandom.Next(0, database.Users.Count()-1);

            var person = await database.Users.Take(newRandom.Next(0, database.Users.Count()-1)).ToListAsync();

            return Ok(person);
        }
    }
}