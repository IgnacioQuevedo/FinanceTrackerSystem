using BusinessLogic.Category_Components;
using BusinessLogic.Transaction_Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Account_Components
{
    public class MonetaryAccount : Account
    {
        #region Properties
        public decimal Amount { get; set; }

        #endregion

        #region Constructor
        public MonetaryAccount() { }

        public MonetaryAccount(string accountName, decimal amount, CurrencyEnum currencyType, DateTime creationDate) : base(accountName, currencyType, creationDate)
        {
            Amount = amount;
            ValidateMonetaryAccount();
        }

        #endregion

        #region Validation of Monetary Account
        public void ValidateMonetaryAccount()
        {
            ValidateAmmount();
        }

        private void ValidateAmmount()
        {
            if (Amount < 0)
            {
                throw new ExceptionValidateAccount("ERROR ON AMMOUNT");
            }
        }

        public override void UpdateAccountMoney(Transaction transactionToBeAdded)
        {
            if (IsOutcome(transactionToBeAdded))
            {
                Amount = Amount - transactionToBeAdded.Amount;
            }
            else if (IsIncome(transactionToBeAdded))
            {
                Amount = Amount + transactionToBeAdded.Amount;
            }
        }

        protected static bool IsIncome(Transaction transactionToBeAdded)
        {
            return transactionToBeAdded.Type == TypeEnum.Income;
        }

        protected bool IsOutcome(Transaction transactionToBeAdded)
        {
            return transactionToBeAdded.Type == TypeEnum.Outcome;
        }



        #endregion
    }
}
