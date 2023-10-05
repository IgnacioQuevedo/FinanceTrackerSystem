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
        public DateTime ValueDate { get; set; }
        public int ExchangeHistoryId { get; set; } = -1;

        #endregion

        #region Constructor
        public ExchangeHistory() { }

        public ExchangeHistory(CurrencyEnum currency, decimal value, DateTime valueDate) 
        { 
            Currency = currency;
            Value = value;
            ValueDate = valueDate;

            ValidateExchange();
        }

        #endregion

        #region Validate Exchange
        public void ValidateExchange()
        {
            ValidateCurrencyIsDollar();
            ValidateValueNumber();
        }
        #region Validate Exchange Auxiliaries
        private void ValidateValueNumber()
        {
            if (Value < 0)
            {
                throw new ExceptionExchangeHistory("Dollar value must be a positive number");
            }
        }

        private void ValidateCurrencyIsDollar()
        {
            if (Currency != CurrencyEnum.USA)
            {
                throw new ExceptionExchangeHistory("Only dollar currency is allowed");
            }
        }
        #endregion


        #endregion
    }
}
