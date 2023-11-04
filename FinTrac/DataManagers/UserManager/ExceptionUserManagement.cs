using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataManagers.UserManager
{
    public class ExceptionUserManagement : Exception
    {
        public ExceptionUserManagement(string message) : base(message)
        {
        }
    }
}