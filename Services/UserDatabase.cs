using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Project_EnterpriseSystem.Models;

namespace Project_EnterpriseSystem.Services
{
    public class UserDatabase : DbContext
    {
        public DbSet<Artist> Artists {get; set; }

        public DbSet<Song> Songs {get; set; }

        public DbSet<User> Users {get; set;}

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("FileName=MusicDatabase.db");
            //base.OnConfiguring(optionsBuilder);
        }
    }
}