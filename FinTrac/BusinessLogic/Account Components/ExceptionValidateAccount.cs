using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Account_Components
{
    public class ExceptionValidateAccount : Exception
    {

        public ExceptionValidateAccount(string message) : base (message) { }
    }
}
