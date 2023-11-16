using BusinessLogic.Enums;
using BusinessLogic.User_Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessLogic.Exceptions;

namespace BusinessLogic.ExchangeHistory_Components
{
    public class ExchangeHistory
    {
        #region Private Attributes
        private bool _isAppliedInATransaction = false;
        #endregion

        #region Properties
        public int ExchangeHistoryId { get; set; }
        public CurrencyEnum Currency { get; set; }
        public decimal Value { get; set; }
        public DateTime ValueDate { get; set; }
        
        public int? UserId{get; set; }
        public User ExchangeHistoryUser { get; set; }

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
            ValidateValueNumber();
        }
        #region Validate Exchange Auxiliaries
        private void ValidateValueNumber()
        {
            if (Value <= 0)
            {
                throw new ExceptionExchangeHistory($" {Currency} value must be a positive number");
            }
        }

        #endregion


        #endregion 

        #region Have Exchanges
        public static bool HaveExchanges(User loggedUser)
        {
            if (loggedUser.MyExchangesHistory.Count == 0)
            {
                return false;
            }
            return true;
        }
        #endregion


        #region Checking if Exchange is applied on a Transaction
        public void SetAppliedExchangeIntoTrue()
        {
            _isAppliedInATransaction = true;
        }

        public void ValidateApplianceExchangeOnTransaction()
        {
            if (_isAppliedInATransaction)
            {
                throw new ExceptionExchangeHistoryManagement("Imposible to modify an exchange that is being used on a transaction.");
            }
        }
        #endregion
    }
}
