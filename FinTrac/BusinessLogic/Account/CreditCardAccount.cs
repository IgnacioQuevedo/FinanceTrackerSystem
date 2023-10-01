using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Account
{
    public class CreditCardAccount : Account
    {

        public string IssuingBank { get; set; }
        public string Last4Digits { get; set; }
        public int AvailableCredit { get; set; }
        public DateTime ClosingDate { get; set; }

        public CreditCardAccount() { }

        public CreditCardAccount(string name,CurrencyEnum currency,string issuingBank, string last4Digits, int availableCredit, DateTime closingDate) : base(name,currency)
        {
            IssuingBank = issuingBank;
            Last4Digits = last4Digits;
            AvailableCredit = availableCredit;
            ClosingDate = closingDate;

            ValidateName();
            ValidateLast4Digits();
            ValidateAvailableCredit();
            ValidateClosingDate();
        }

        public void ValidateCreditCardAccount()
        {
            ValidateName();
            ValidateLast4Digits();
            ValidateAvailableCredit();
            ValidateClosingDate();
        }

        private void ValidateAvailableCredit()
        {
            if (AvailableCredit < 0)
            {
                throw new ExceptionValidateAccount("You do not have avaible credit...");
            }
        }

        private void ValidateClosingDate()
        {
            if (DateTime.Compare(CreationDate, ClosingDate) >= 0)
            {
                throw new ExceptionValidateAccount("ERROR ON DATE");
            }
        }

        private void ValidateName()
        {
            if (string.IsNullOrEmpty(IssuingBank))
            {
                throw new ExceptionValidateAccount("ERROR ON ISSUING BANK NAME");
            }
        }

        private void ValidateLast4Digits()
        {
            int lengthOfLastDigits = Last4Digits.Length;

            if (string.IsNullOrEmpty(Last4Digits) || lengthOfLastDigits < 4 || !IsNumericChain())
            {
                throw new ExceptionValidateAccount("ERROR ON LAST 4 DIGITS");
            }
        }

        private bool IsNumericChain()
        {
            bool isCorrect = true;

            foreach (char caracter in Last4Digits)
            {
                if (!char.IsDigit(caracter))
                {
                    isCorrect = false;
                }
            }
            return isCorrect;
        }
    }
}
