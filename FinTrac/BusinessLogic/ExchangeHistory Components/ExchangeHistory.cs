using BusinessLogic.Account_Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.ExchangeHistory_Components
{
    public class ExchangeHistory
    {
        public CurrencyEnum Currency { get; set; }





        public void ValidateExchange()
        {
            if(Currency != CurrencyEnum.USA)
            {
                throw new ExceptionExchangeHistory("Only dollar currency is allowed");
            }
        }


    }
}
