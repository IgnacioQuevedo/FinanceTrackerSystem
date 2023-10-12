using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessLogic.Account_Components;
using BusinessLogic.Category_Components;
using BusinessLogic.User_Components;
using BusinessLogic.Transaction_Components;
using BusinessLogic.Enums;

namespace DataManagers
{
    public class Repository
    {
        public List<User> Users { get; set; }

        public Repository()
        {
            Users = new List<User>();

        }
    }
}
