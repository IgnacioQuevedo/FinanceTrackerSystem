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
        public string ClosingDate { get; set; }

        public CreditCardAccount() { }


        public void ValidateCreditCardAccount()
        {
            ValidateName();
            ValidateLast4Digits();

            if(AvailableCredit < 0)
            {
                throw new ExceptionValidateAccount("You do not have avaible credit...");
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
