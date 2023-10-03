using BusinessLogic.Account_Components;
using BusinessLogic.Category_Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Transaction
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
            bool allCategoriesAreIncome = MyCategories.All(c => c.Type == TypeEnum.Income);
            bool allCategoriesAreOutcome = MyCategories.All(c => c.Type == TypeEnum.Outcome);

            return allCategoriesAreIncome || allCategoriesAreOutcome;

        }
        private bool ValidateAllCategoriesAreEnabled()
        {
            bool allCategoriesAreEnabled = MyCategories.All(c => c.Status == StatusEnum.Enabled);
            return allCategoriesAreEnabled;
        }

        #endregion
    }
}
