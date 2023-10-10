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
        #region Attributes
        private static decimal _initialAmount;
        #endregion

        #region Properties
        public decimal Amount { get; set; }

        #endregion

        #region Constructor
        public MonetaryAccount() { }

        public MonetaryAccount(string accountName, decimal amount, CurrencyEnum currencyType, DateTime creationDate) : base(accountName, currencyType, creationDate)
        {
            Amount = amount;
            _initialAmount = amount;
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
                throw new ExceptionValidateAccount("ERROR ON AMOUNT");
            }
        }

        public override void UpdateAccountMoneyAfterAdd(Transaction transactionToBeAdded)
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

        public override void UpdateAccountAfterModify(Transaction transactionToBeAdded, decimal oldAmountOfTransaction)
        {
            if (IsIncome(transactionToBeAdded))
            {
                ModifyIncomeAmount(transactionToBeAdded, oldAmountOfTransaction);
            }
            if (IsOutcome(transactionToBeAdded))
            {
                ModifyOutcomeAmount(transactionToBeAdded, oldAmountOfTransaction);
            }
        }

        public override void UpdateAccountAfterDelete(Transaction transactionToBeDeleted)
        {
            if (IsIncome(transactionToBeDeleted))
            {
                Amount = Amount - transactionToBeDeleted.Amount;
            }
            if (IsOutcome(transactionToBeDeleted))
            {
                Amount = Amount + transactionToBeDeleted.Amount;
            }

        }

        private void ModifyOutcomeAmount(Transaction transactionToBeAdded, decimal oldAmountOfTransaction)
        {
            Amount = Amount + oldAmountOfTransaction;
            Amount = Amount - transactionToBeAdded.Amount;
        }

        private void ModifyIncomeAmount(Transaction transactionToBeAdded, decimal oldAmountOfTransaction)
        {
            Amount = Amount - oldAmountOfTransaction;
            Amount = Amount + transactionToBeAdded.Amount;
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

        #region Method to Return Initial Amount

        public decimal ReturnInitialAmount()
        {
            return _initialAmount;
        }

        #endregion
    }
}
