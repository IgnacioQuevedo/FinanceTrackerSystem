using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Transaction_Components
{
    public class ExceptionValidateTransaction : Exception
    {
        public ExceptionValidateTransaction(string message) : base(message) { }
    }
}
