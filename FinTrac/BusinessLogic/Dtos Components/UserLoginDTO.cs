using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Dtos_Components
{
    public class UserLoginDTO
    {
        public int UserId { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }

        public UserLoginDTO() { }

        public UserLoginDTO(int userId, string email, string password)
        {
            Email = email;
            Password = password;
            UserId = userId;
        }


    }
}
