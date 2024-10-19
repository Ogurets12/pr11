using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pract8
{
    public class User
    {
        public int Id { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string Role { get; set; }

        public User() { }

        public User(string login, string password, string email, string role)
        {
            Login = login;
            Password = password;
            Email = email;
            Role = role;
        }
    }
    
}
