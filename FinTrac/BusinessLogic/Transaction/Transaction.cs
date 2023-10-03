﻿using BusinessLogic.Account_Components;
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

        #region Validate Title
        public void ValidateTitle()
        {
            if (string.IsNullOrEmpty(Title))
            {
                throw new ExceptionValidateTransaction("ERROR ON TITLE");
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
            ValidateAllCategoriesAreEnabled();
            ValidateAllCategoriesBelongToTypeOfTransaction();

        }

        private void ValidateAllCategoriesBelongToTypeOfTransaction()
        {
            switch (Type)
            {
                case TypeEnum.Income:
                    {
                        bool allCategoriesAreIncome = MyCategories.All(c => c.Type == TypeEnum.Income);
                        if (!allCategoriesAreIncome)
                        {
                            throw new ExceptionValidateTransaction("ERROR ON LIST OF CATEGORIES");
                        }
                        break;
                    }
                case TypeEnum.Outcome:
                    {
                        bool allCategoriesAreOutcome = MyCategories.All(c => c.Type == TypeEnum.Outcome);
                        if (!allCategoriesAreOutcome)
                        {
                            throw new ExceptionValidateTransaction("ERROR ON LIST OF CATEGORIES");
                        }
                        break;
                    }
            }
        }

        private void ValidateAllCategoriesAreEnabled()
        {
            bool allCategoriesAreEnabled = MyCategories.All(c => c.Status == StatusEnum.Enabled);
            if (!allCategoriesAreEnabled)
            {
                throw new ExceptionValidateTransaction("ERROR ON LIST OF CATEGORIES");
            }
        }

        #endregion
    }
}
