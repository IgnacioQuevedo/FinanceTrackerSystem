using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessLogic.Account_Components;
using BusinessLogic.Category_Components;
using BusinessLogic.User_Components;
using BusinessLogic.Transaction_Components;

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
            Category usertCategory = new Category("Food", StatusEnum.Enabled, TypeEnum.Outcome);
            Category usertCategory2 = new Category("Party", StatusEnum.Enabled, TypeEnum.Outcome);
            

        
        }


    }
}
