using BusinessLogic.Account_Components;
using BusinessLogic.Category_Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Authentication;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Transaction_Components
{
    public class Transaction
    {
        #region Properties
        public string Title { get; set; }

        public DateTime CreationDate { get; set; } = DateTime.Now.Date;

        public decimal Amount { get; set; }

        public CurrencyEnum Currency { get; set; }

        public TypeEnum Type { get; set; }

        public Account Account { get; set; }

        public List<Category> MyCategories { get; set; }

        public int TransactionId { get; set; } = -1;

        #endregion

        #region Constructors
        public Transaction()
        {
        }

        public Transaction(string title, decimal amount, CurrencyEnum currency, TypeEnum type, Account account, List<Category> myCategories)
        {
            Title = title;
            Amount = amount;
            Currency = currency;
            Type = type;
            Account = account;
            MyCategories = myCategories;

            ValidateTitle();
            ValidateAccount();
            ValidateAmount();
            ValidateListOfCategories();
        }

        #endregion

        #region Validate Title
        public void ValidateTitle()
        {
            if (string.IsNullOrEmpty(Title))
            {
                throw new ExceptionValidateTransaction("ERROR ON TITLE");
            }
        }
        #endregion

        #region Validate Account

        public void ValidateAccount()
        {
            bool typeIsIncome = (Type == TypeEnum.Income);

            if (Account is CreditCardAccount && typeIsIncome)
            {
                throw new ExceptionValidateTransaction("Transaction of type income can't be associated to a credit account");
            }
        }

        #endregion

        #region Validate Amount
        public void ValidateAmount()
        {
            bool amountIsNoNegativeOrZero = Amount <= 0;

            if (amountIsNoNegativeOrZero)
            {
                throw new ExceptionValidateTransaction("ERROR ON AMOUNT");
            }
        }
        #endregion

        #region Validate List of Categories

        public void ValidateListOfCategories()
        {
            bool isNotValidList = !ValidateAllCategoriesAreEnabled() || !ValidateAllCategoriesBelongToTypeOfTransaction() || !ValidateEmptyList();

            if (isNotValidList)
            {
                throw new ExceptionValidateTransaction("Error");
            }

        }
        private bool ValidateEmptyList()
        {
            if (MyCategories.Count == 0)
            {
                return false;
            }
            return true;
        }

        private bool ValidateAllCategoriesBelongToTypeOfTransaction()
        {
            bool allCategoriesAreIncome = false;
            bool allCategoriesAreOutcome = false;

            if (Type is TypeEnum.Income)
            {
                allCategoriesAreIncome = MyCategories.All(c => c.Type == TypeEnum.Income);
                if (allCategoriesAreIncome)
                {
                    return true;
                }
            }
            else
            {
                allCategoriesAreOutcome = MyCategories.All(c => c.Type == TypeEnum.Outcome);
                if (allCategoriesAreOutcome)
                {
                    return true;
                }
            }
            return false;

        }
        private bool ValidateAllCategoriesAreEnabled()
        {
            bool allCategoriesAreEnabled = MyCategories.All(c => c.Status == StatusEnum.Enabled);
            return allCategoriesAreEnabled;
        }

        #endregion
    }
}
