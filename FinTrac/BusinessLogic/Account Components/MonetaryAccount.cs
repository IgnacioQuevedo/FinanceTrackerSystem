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
        public decimal Ammount { get; set; }

        #endregion

        #region Constructor
        public MonetaryAccount() { }

        public MonetaryAccount(string accountName, decimal ammount, CurrencyEnum currencyType) : base(accountName, currencyType)
        {
            Ammount = ammount;
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
            if (Ammount < 0)
            {
                throw new ExceptionValidateAccount("ERROR ON AMMOUNT");
            }
        }

        public override void UpdateAccountMoney(Transaction transactionToBeAdded)
        {
            if (IsOutcome(transactionToBeAdded))
            {
                Ammount = Ammount - transactionToBeAdded.Amount;
            }
            else if (IsIncome(transactionToBeAdded))
            {
                Ammount = Ammount + transactionToBeAdded.Amount;
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
