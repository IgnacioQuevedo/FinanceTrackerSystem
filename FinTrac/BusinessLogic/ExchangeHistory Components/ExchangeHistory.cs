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
        #region Properties
        public CurrencyEnum Currency { get; set; }

        public decimal Value { get; set; }



        #endregion

        #region Validate Exchange
        public void ValidateExchange()
        {
            ValidateCurrencyIsDollar();
        }

        private void ValidateCurrencyIsDollar()
        {
            if (Currency != CurrencyEnum.USA)
            {
                throw new ExceptionExchangeHistory("Only dollar currency is allowed");
            }
        }

        #endregion
    }
}
