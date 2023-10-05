using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.ExchangeHistory_Components
{
    public class ExceptionExchangeHistoryManagement : Exception
    {
        public ExceptionExchangeHistoryManagement(string message) : base(message) { }
    }
}
