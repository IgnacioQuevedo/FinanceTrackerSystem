using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessLogic.Transaction_Components;

namespace BusinessLogic.Account_Components
{
    public abstract class Account
    {
        #region Properties
        public string Name { get; set; } = "";
        public CurrencyEnum Currency { get; set; }
        public DateTime CreationDate { get; } = DateTime.Now.Date;
        public int AccountId { get; set; } = -1;
        public List<Transaction> MyTransactions { get; set; }


        #endregion

        #region Constructor
        public Account() { }


        public Account(string name, CurrencyEnum currency)
        {
            Name = name;
            Currency = currency;


            if (ValidateAccount())
            {
                MyTransactions = new List<Transaction>();
            }

        }

        #endregion

        #region Validate Account
        public bool ValidateAccount()
        {
            if (string.IsNullOrEmpty(Name))
            {
                throw new ExceptionValidateAccount("ERROR ON ACCOUNT NAME");
            }
            return true;
        }
        #endregion

        #region Transactions Management

        public void AddTransaction(Transaction transactionToBeAdded)
        {
            if (IsMonetaryAccount(transactionToBeAdded))
            {
                ManageAmount(transactionToBeAdded);
            }
            if (IsCreditAccount(transactionToBeAdded))
            {
                ManageCredit(transactionToBeAdded);
            }
            SetTransactionId(transactionToBeAdded);
            MyTransactions.Add(transactionToBeAdded);
        }

        private void SetTransactionId(Transaction transactionToBeAdded)
        {
            transactionToBeAdded.TransactionId = MyTransactions.Count;
        }

        private static void ManageCredit(Transaction transactionToBeAdded)
        {
            CreditCardAccount accountToBeUpdated = (CreditCardAccount)transactionToBeAdded.Account;

            accountToBeUpdated.AvailableCredit = accountToBeUpdated.AvailableCredit - transactionToBeAdded.Amount;
        }

        private static bool IsCreditAccount(Transaction transactionToBeAdded)
        {
            return transactionToBeAdded.Account is CreditCardAccount;
        }

        private static void ManageAmount(Transaction transactionToBeAdded)
        {
            MonetaryAccount accountToBeUpdated = (MonetaryAccount)transactionToBeAdded.Account;
            if (IsOutcome(transactionToBeAdded))
            {
                DecreaseAmount(transactionToBeAdded, accountToBeUpdated);
            }
            else if (IsIncome(transactionToBeAdded))
            {
                IncreaseAmount(transactionToBeAdded, accountToBeUpdated);
            }
        }

        private static bool IsMonetaryAccount(Transaction transactionToBeAdded)
        {
            return transactionToBeAdded.Account is MonetaryAccount;
        }

        private static bool IsIncome(Transaction transactionToBeAdded)
        {
            return transactionToBeAdded.Type is Category_Components.TypeEnum.Income;
        }

        private static bool IsOutcome(Transaction transactionToBeAdded)
        {
            return transactionToBeAdded.Type is Category_Components.TypeEnum.Outcome;
        }

        private static void DecreaseAmount(Transaction transactionToBeAdded, MonetaryAccount accountToBeUpdated)
        {
            accountToBeUpdated.Ammount = accountToBeUpdated.Ammount - transactionToBeAdded.Amount;
        }

        private static void IncreaseAmount(Transaction transactionToBeAdded, MonetaryAccount accountToBeUpdated)
        {
            accountToBeUpdated.Ammount = accountToBeUpdated.Ammount + transactionToBeAdded.Amount;
        }

        #endregion

    }
}
