using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Project_EnterpriseSystem.Models
{
    /* 
    David

    Created a user class. It has the necessary properties like Id and 
    list of playlists.
    
    */
    public class User : IModelInterface
    {
        
        public Guid Id { get; set; } = Guid.NewGuid();
        public List<Playlist> ListOfPlaylists {get; set; } = new();
        public string userName = "";
        private string Password = "123456";

        public User(){
            Playlist newPlaylist = new(){
                PlayListTitle = "Generic Playlist"
            };
            ListOfPlaylists.Add(newPlaylist);
        }
        public string UserPasssword {
            get{
            return Password;    
            } 
            set{
                Password = value;
            }
        }
        public string Username { get => userName; set{
            if(value == default)
                throw new ArgumentNullException();

            userName = value;
            }
        }

        public void AddPlaylist(Playlist newPlaylist){
            ListOfPlaylists.Add(newPlaylist);
        }

        public bool ValidatePassword(string pw){
            if(pw.Length < 6)
                return false; //not long enough
            
            if(pw == pw.ToLower())
                return false;//no upper-case

            if(!pw.Any(char.IsDigit))
                return false;//no digit
                
            return true;
        }

        

        
    }
}