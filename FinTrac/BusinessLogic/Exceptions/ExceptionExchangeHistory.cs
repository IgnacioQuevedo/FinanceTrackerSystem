using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Exceptions
{
    public class ExceptionExchangeHistory : Exception
    {
        public ExceptionExchangeHistory(string message) : base(message) { }
    }
}
