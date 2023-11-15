using BusinessLogic.Transaction_Components;
using BusinessLogic.User_Components;
using BusinessLogic.Enums;
using BusinessLogic.Exceptions;
using System.ComponentModel.DataAnnotations.Schema;

namespace BusinessLogic.Account_Components
{
    public abstract class Account
    {
        #region Properties

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int AccountId { get; set; }
        public string Name { get; set; } = "";
        public CurrencyEnum Currency { get; set; }
        public DateTime CreationDate { get; set; } = DateTime.Now.Date;

        public int? UserId { get; set; }
        public User AccountUser { get; set; }
        public List<Transaction> MyTransactions { get; set; }

        #endregion

        #region Constructor

        public Account()
        {
        }


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
            MyTransactions.Add(transactionToBeAdded);
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
                    MyTransactions[i].Amount = transactionToUpdate.Amount;
                    MyTransactions[i].TransactionCategory = transactionToUpdate.TransactionCategory;
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
        }

        #endregion

        #endregion
    }
}