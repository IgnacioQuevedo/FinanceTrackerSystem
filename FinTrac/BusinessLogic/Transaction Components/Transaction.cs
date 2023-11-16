using BusinessLogic.Category_Components;
using BusinessLogic.Enums;
using BusinessLogic.ExchangeHistory_Components;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Security.Authentication;
using System.Text;
using System.Threading.Tasks;
using BusinessLogic.Exceptions;
using BusinessLogic.Account_Components;

namespace BusinessLogic.Transaction_Components
{
    public class Transaction
    {
        #region Properties
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int TransactionId { get; set; }
        public string Title { get; set; }
        public DateTime CreationDate { get; set; } = DateTime.Now.Date;
        public decimal Amount { get; set; }
        public CurrencyEnum Currency { get; set; }
        public TypeEnum Type { get; set; }
        public Category TransactionCategory { get; set; }
        
        public int? CategoryId { get; set; } 
        public int? AccountId { get; set; }
        public Account TransactionAccount { get; set; }
            
        #endregion

        #region Constructors
        public Transaction()
        {
        }

        public Transaction(string title, decimal amount, DateTime date, CurrencyEnum currency, TypeEnum type, Category transactionCategory)
        {
            Title = title;
            Amount = amount;
            Currency = currency;
            Type = type;
            TransactionCategory = transactionCategory;
            CreationDate = date.Date;
            ValidateTransaction();
        }

        #endregion

        #region Validations
        public void ValidateTransaction()
        {
            ValidateCategory();
            ValidateTitle();
            ValidateAmount();
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

        #region Validate Category
        public void ValidateCategory()
        {
            NotSelectedACategory();

            if (IsDisabledCategory() || TransactionCategory.Type != Type)
            {
                throw new ExceptionValidateTransaction("Error: Seems like you have a disabled category or not available type");
            }
        }

        public void NotSelectedACategory()
        {
            if(TransactionCategory == null)
            {
                throw new ExceptionValidateTransaction("ERROR - must select a category");
            }

        }

        private bool IsDisabledCategory()
        {
            return TransactionCategory.Status == StatusEnum.Disabled;
        }

        #endregion

        #region Validate exchange exists for USA transaction date
        public static void CheckExistenceOfExchange(Transaction transactionToCheck, List<ExchangeHistory> exchangeHistories)
        {
            bool existsExchangeOnThatDate = false;

            if (transactionToCheck.Currency != CurrencyEnum.UY)
            {
                foreach (ExchangeHistory exchangeHistory in exchangeHistories)
                {
                    if (!existsExchangeOnThatDate && DateTime.Compare(exchangeHistory.ValueDate,
                                                      transactionToCheck.CreationDate) == 0
                                                  && transactionToCheck.Currency == exchangeHistory.Currency)

                    {
                        existsExchangeOnThatDate = true;
                        exchangeHistory.SetAppliedExchangeIntoTrue();
                    }
                }

                if (!existsExchangeOnThatDate)
                {
                    throw new ExceptionValidateTransaction("There is no register exchange for this date");
                }
            }

        }

        #endregion

    }
}
