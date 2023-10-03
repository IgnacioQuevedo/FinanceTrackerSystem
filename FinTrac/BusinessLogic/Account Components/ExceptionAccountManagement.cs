using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Account_Components
{
    public class ExceptionAccountManagement : Exception
    {
        public ExceptionAccountManagement(string message) : base(message) { }
    }
}
