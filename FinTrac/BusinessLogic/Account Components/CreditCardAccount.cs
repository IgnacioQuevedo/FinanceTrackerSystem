using BusinessLogic.Enums;
using BusinessLogic.Transaction_Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessLogic.Exceptions;

namespace BusinessLogic.Account_Components
{
    public class CreditCardAccount : Account
    {
        private CurrencyEnum currency;
        #region Properties

        public string IssuingBank { get; set; }
        public string Last4Digits { get; set; }
        public decimal AvailableCredit { get; set; }
        public DateTime ClosingDate { get; set; }

        #endregion

        #region Constructor

        public CreditCardAccount()
        {
        }

        public CreditCardAccount(string name, CurrencyEnum currency, DateTime creationDate, string issuingBank,
            string last4Digits, decimal availableCredit, DateTime closingDate) : base(name, currency, creationDate)
        {
            IssuingBank = issuingBank;
            Last4Digits = last4Digits;
            AvailableCredit = availableCredit;
            ClosingDate = closingDate.Date;
            ValidateCreditCardAccount();
        }

        #endregion

        #region Validations of Credit Card

        public void ValidateCreditCardAccount()
        {
            ValidateIssuingNameAccount();
            ValidateLast4Digits();
            ValidateAvailableCredit();
            ValidateClosingDate();
        }

        #region Validation Of Credit

        private void ValidateAvailableCredit()
        {
            if (AvailableCredit < 0)
            {
                throw new ExceptionValidateAccount("You do not have avaible credit...");
            }
        }

        #endregion

        #region Validation Of ClosingDate

        private void ValidateClosingDate()
        {
            if (DateTime.Compare(CreationDate, ClosingDate) >= 0)
            {
                throw new ExceptionValidateAccount("Error on date, seems like your creation date is greater than the closing date");
            }
        }

        #endregion

        #region Validation Of IssuingName

        private void ValidateIssuingNameAccount()
        {
            if (string.IsNullOrEmpty(IssuingBank))
            {
                throw new ExceptionValidateAccount("ERROR ON ISSUING BANK NAME");
            }
        }

        #endregion

        #region Validation of Last 4 Digits

        private void ValidateLast4Digits()
        {
            int lengthOfLastDigits = Last4Digits.Length;

            if (string.IsNullOrEmpty(Last4Digits) || lengthOfLastDigits != 4 || !IsNumericChain())
            {
                throw new ExceptionValidateAccount("ERROR ON LAST 4 DIGITS");
            }
        }

        private bool IsNumericChain()
        {
            foreach (char caracter in Last4Digits)
            {
                if (!char.IsDigit(caracter))
                {
                    return false;
                }
            }

            return true;
        }

        public override void UpdateAccountMoneyAfterAdd(Transaction transactionToBeAdded)
        {
            AvailableCredit = AvailableCredit - transactionToBeAdded.Amount;
        }

        public override void UpdateAccountAfterModify(Transaction transactionToBeAdded, decimal oldAmountOfTransaction)
        {
            ModifyOutcomeAmount(transactionToBeAdded, oldAmountOfTransaction);
        }

        private static bool IsOutcome(Transaction transactionToBeAdded)
        {
            return transactionToBeAdded.Type == TypeEnum.Outcome;
        }

        private void ModifyOutcomeAmount(Transaction transactionToBeAdded, decimal oldAmountOfTransaction)
        {
            decimal resetCredit = AvailableCredit + oldAmountOfTransaction;
            AvailableCredit = resetCredit - transactionToBeAdded.Amount;
        }

        public override void UpdateAccountAfterDelete(Transaction transactionToBeDeleted)
        {
            AvailableCredit = AvailableCredit + transactionToBeDeleted.Amount;
        }

        #endregion

        #endregion
    }
}