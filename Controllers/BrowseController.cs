using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EnterpriseSystem_Project.Models;
using Microsoft.AspNetCore.Mvc;
using Project_EnterpriseSystem.Services;

namespace EnterpriseSystem_Project.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BrowseController: ControllerBase
    {
        private UserDatabase database = new();

        //get all songs
        [HttpGet("allsongs")]
        public async Task<IActionResult> GetAllSongs(){
            //if no songs are in the database throw an exception
            if (database.Songs.Count() == 0){
                throw new Exception("No songs in database");
            }
            var songs = database.Songs;
            return Ok(songs);
        }



        //get songs with a specific genre
        [HttpGet("allgenres/{genre}")]
        public async Task<IActionResult> GetSongsByGenre(Genre genre){

            //scrape Songs in database where the Genre matches the argument given
            var searchGenre = database.Songs.Where(x => x.Genre == genre);
            return Ok(searchGenre);
        }
    }
}