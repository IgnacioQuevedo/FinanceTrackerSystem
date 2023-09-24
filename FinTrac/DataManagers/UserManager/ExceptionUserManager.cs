using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataManagers.UserManager
{
    public class ExceptionUserManager : Exception
    {
        public ExceptionUserManager(string message) : base(message) { }
    }
}
