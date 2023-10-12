using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessLogic.Transaction_Components;
using BusinessLogic.ExchangeHistory_Components;
using BusinessLogic.User_Components;


namespace BusinessLogic.Account_Components
{
    public abstract class Account
    {
        #region Properties
        public string Name { get; set; } = "";
        public CurrencyEnum Currency { get; set; }
        public DateTime CreationDate { get; set; } = DateTime.Now.Date;
        public int AccountId { get; set; } = -1;
        public List<Transaction> MyTransactions { get; set; }


        #endregion

        #region Constructor
        public Account() { }


        public Account(string name, CurrencyEnum currency, DateTime creationDate)
        {
            Name = name;
            Currency = currency;
            CreationDate = creationDate;

            if (ValidateAccount())
            {
                MyTransactions = new List<Transaction>();
            }

        }

        #endregion

        #region Mandatory Methods
        public abstract void UpdateAccountMoneyAfterAdd(Transaction transactionToBeAdded);

        public abstract void UpdateAccountAfterModify(Transaction transactionToBeAdded, decimal oldAmountOfTransaction);
        public abstract void UpdateAccountAfterDelete(Transaction transactionToBeRemoved);

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

        #region Transaction Management

        #region Add Transaction
        public void AddTransaction(Transaction transactionToBeAdded)
        {
            SetTransactionId(transactionToBeAdded);
            MyTransactions.Add(transactionToBeAdded);
        }

        private void SetTransactionId(Transaction transactionToBeAdded)
        {
            transactionToBeAdded.TransactionId = MyTransactions.Count;
        }

        #endregion

        #region Modify Transaction

        public void ModifyTransaction(Transaction transactionToUpdate)
        {
            bool flag = false;

            for (int i = 0; i < MyTransactions.Count && !flag; i++)
            {
                if (HaveSameId(transactionToUpdate, i))
                {
                    MyTransactions[i] = transactionToUpdate;
                    flag = true;
                }
            }
        }

        private bool HaveSameId(Transaction transactionToUpdate, int i)
        {
            return MyTransactions[i].TransactionId == transactionToUpdate.TransactionId;
        }

        #endregion

        #region Get All Transactions

        public List<Transaction> GetAllTransactions()
        {
            return MyTransactions;
        }

        #endregion

        #region Delete Transactions

        public void DeleteTransaction(Transaction transactionToDelete)
        {
            MyTransactions.Remove(transactionToDelete);
            MyTransactions.Insert(transactionToDelete.TransactionId,null);
            
        }

        #endregion


        #endregion

    }
}
