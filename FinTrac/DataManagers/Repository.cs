using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessLogic.User_Components;

namespace DataManagers
{
    public class Repository
    {
        public List<User> Users { get; set; }

        public Repository()
        {
            Users = new List<User>();
            DefaultUser();
        }



        private void DefaultUser()
        {
            User userTest = new User("Ignacio", "Quevedo", "nachitoquevedo@gmail.com", "Nacho200304!", "");
            userTest.UserId = 0;
            Users.Add(userTest);
        }


    }
}
