using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessLogic.Category;
using BusinessLogic.User;

namespace DataManagers
{
    public class Repository
    {
        public List<User> Users { get; set; }

        public List<Category> Categories { get; set; }


        public Repository()
        {
            Users = new List<User>();
            Categories = new List<Category>();
        }



    }
}
