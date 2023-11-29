using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
    }
}