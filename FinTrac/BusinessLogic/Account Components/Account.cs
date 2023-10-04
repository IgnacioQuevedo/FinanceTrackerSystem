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
            if (transactionToBeAdded.Account is MonetaryAccount)
            {
                MonetaryAccount accountToBeUpdated = (MonetaryAccount)transactionToBeAdded.Account;
                if (transactionToBeAdded.Type is Category_Components.TypeEnum.Outcome)
                {
                    accountToBeUpdated.Ammount = accountToBeUpdated.Ammount - transactionToBeAdded.Amount;
                }
                if (transactionToBeAdded.Type is Category_Components.TypeEnum.Income)
                {
                    accountToBeUpdated.Ammount = accountToBeUpdated.Ammount + transactionToBeAdded.Amount;
                }
            }
            MyTransactions.Add(transactionToBeAdded);
        }

        #endregion

    }
}
