using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.User
{

    public class ExceptionValidateUser : Exception
    {
        public ExceptionValidateUser(string message) : base(message) { }
    }

}
