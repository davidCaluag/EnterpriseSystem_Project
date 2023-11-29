using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Storage;
using Project_EnterpriseSystem.Services;
using Project_EnterpriseSystem.Models;

namespace Project_EnterpriseSystem.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserPlaylist : ControllerBase
    {
        private UserDatabase database = new();

        

    }
}